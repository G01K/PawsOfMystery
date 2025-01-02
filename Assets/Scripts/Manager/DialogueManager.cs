using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Ink 동기화")]
    public TextAsset inkJSONAsset;
    private Story story;
    private bool isPaused = false;

    [Header("화자 컨테이너 매핑")]
    public Dictionary<string, GameObject> speakerContainers;

    [Header("화자 컨테이너")]
    [SerializeField] private GameObject dialogBox;

    [SerializeField] private GameObject heroContainer;
    [SerializeField] private GameObject npcContainer;

    [Header("프리팹 및 컨테이너")]
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;
    private List<GameObject> activeChoices = new List<GameObject>();
    private Queue<GameObject> choicePool = new Queue<GameObject>();

    [Header("타이핑 애니메이션")]
    public float typingSpeed = 0.01f;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private string knotName = "";


    void Start()
    {
        init();
        InkManager.Instance.initBindFnc();
        DisplayNextLine();
    }

    void init()
    {
        /**        Debug.Log("dd");

        List<Hint> hints = hintManager.GetHints("EnterForest");
        foreach (Hint hint in hints)
        {
            Debug.Log($"Hint: ID = {hint.id}, Name = {hint.name}, Description = {hint.description} , imagePath = {hint.imagePath} , position = {hint.position}");
        }
        */
        story = InkManager.Instance.InitializeStory(inkJSONAsset.text);
        InitializeSpeakerContainers();
    }


    void OnEnable()
    {
        InputManager.OnMouseClick += HandleMouseClick;
    }

    void OnDisable()
    {
        InputManager.OnMouseClick -= HandleMouseClick;
    }

    public void PauseStory()
    {
        dialogBox.SetActive(false);
        isPaused = true;
    }

    public void ResumeStory()
    {
        dialogBox.SetActive(true);
        isPaused = false;
        DisplayNextLine();
    }

    void HandleMouseClick()
    {
        if (isTyping)
        {
            SkipTyping();
        }
        else if (!isTyping && story.currentChoices.Count == 0)
        {
            DisplayNextLine();
        }
    }

    void InitializeSpeakerContainers()
    {
        speakerContainers = new Dictionary<string, GameObject>
        {
            { "주인공", heroContainer },
            { "여우", npcContainer },
            { "염소", npcContainer },
            { "공작", npcContainer },
            { "족제비", npcContainer },
            { "해설", npcContainer }
        };
    }

    public void DisplayNextLine(string speakerName = "")
    {
        // 스토리가 일시정지된 경우 아무것도 하지 않음
        if (isPaused) return;

        // 대화 상자가 비활성화되어 있으면 활성화
        if (!dialogBox.activeSelf)
        {
            dialogBox.SetActive(true);
        }

        // 이전 선택지가 있는경우 비활성화
        ClearChoices();

        // 스토리가 계속될 수 있는 경우
        if (story.canContinue)
        {
            string text = story.Continue().Trim(); // 다음 텍스트 가져오기
            HandleDialogue(text, speakerName);    // 대화 처리
        }
        // 스토리가 더 이상 진행할 수 없지만 선택지가 있는 경우
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices(); // 선택지 표시
        }
        else
        {
            Debug.Log("스토리가 종료되었거나 진행할 내용이 없습니다.");
        }

        // 현재 노드 이름을 가져오기
        string knotName = InkManager.Instance.GetKnotName();

        if (!this.knotName.Equals(knotName))
        {
            this.knotName = knotName;
            // 노드가 바뀔때 마다 힌트 지우기(화면 전환?)
            HintManager.Instance.removeAllHint();

            Debug.Log("knotName가 변경됨 : " + knotName);
        }
    }

    void HandleDialogue(string text, string speakerName = "")
    {
        string speaker = (speakerName == "주인공") ? speakerName : story.variablesState["currentSpeaker"]?.ToString();

        HideAllContainers();
        Debug.Log($"인물 : {speaker} /n 대사 : {text}");

        if (speakerContainers.TryGetValue(speaker, out GameObject targetContainer))
        {
            ShowContainer(targetContainer, text, speaker);
        }
        else
        {
            ShowContainer(npcContainer, text, speaker);
        }
    }
    void ShowContainer(GameObject container, string text, string speakerName = "")
    {
        // NameText 설정
        TMP_Text nameTextComponent = container.transform.Find("NameText")?.GetComponent<TMP_Text>();
        if (nameTextComponent != null && nameTextComponent.text != speakerName)
        {
            nameTextComponent.text = speakerName;
        }

        // DialogueText 설정
        TMP_Text dialogueTextComponent = container.transform.Find("DialogueText")?.GetComponent<TMP_Text>();
        if (dialogueTextComponent != null && dialogueTextComponent.text != text)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            // DialogBox 활성화 및 코루틴 실행
            StartCoroutine(ActivateAndStartCoroutine(container, dialogueTextComponent, text));
        }
    }

    void HideAllContainers()
    {
        foreach (var container in speakerContainers.Values)
        {
            container.SetActive(false);

        }
    }

    IEnumerator TypeText(TMP_Text textComponent, string text)
    {
        isTyping = true;
        textComponent.text = ""; // 초기화
        for (int i = 0; i <= text.Length; i++) // `i <= text.Length`로 수정
        {
            int lengthToShow = Mathf.Min(i, text.Length); // 길이 초과 방지
            textComponent.text = text.Substring(0, lengthToShow); // 현재까지 출력할 텍스트

            yield return new WaitForSecondsRealtime(typingSpeed); // 대기
        }

        // 최종 텍스트 보장
        textComponent.text = text; // 전체 텍스트 설정

        isTyping = false;

        if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }

    }

    void OnChoiceSelected(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index); // 선택된 인덱스를 Ink 스토리에 전달
        ClearChoices(); // 기존 선택지를 모두 제거
        DisplayNextLine("주인공"); // 선택 후 다음 대사를 표시
    }

    void SkipTyping()
    {
        if (isTyping && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;

            TMP_Text dialogueTextComponent = GetCurrentDialogueTextComponent();
            if (dialogueTextComponent != null)
            {
                dialogueTextComponent.text = story.currentText?.Trim();
            }

            if (story.currentChoices.Count > 0)
            {
                DisplayChoices();
            }
        }
    }

    TMP_Text GetCurrentDialogueTextComponent()
    {
        foreach (var container in speakerContainers.Values)
        {
            if (container.activeSelf)
            {
                return container.transform.Find("DialogueText")?.GetComponent<TMP_Text>();
            }
        }
        return null;
    }

    void DisplayChoices()
    {
        ClearChoices();

        foreach (Choice choice in story.currentChoices)
        {
            //테스트코드 start
            if (!string.IsNullOrEmpty(choice.text))
            {
                Debug.Log($"choice.text is {choice.text}");

                string[] firstWord = choice.text.Split(" ");
                if (!"G1".Equals(firstWord[0]))
                {
                    CreateChoice(choice.text, () => OnChoiceSelected(choice));
                }
            }

            //if (!"G1".Equals(choice.text)) CreateChoice(choice.text, () => OnChoiceSelected(choice));
            //테스트코드 end 

            //CreateChoice(choice.text, () => OnChoiceSelected(choice));
        }
    }

    public void CreateChoice(string choiceText, System.Action onClickAction)
    {
        GameObject choiceButton;

        if (choicePool.Count > 0)
        {
            choiceButton = choicePool.Dequeue();
            choiceButton.SetActive(true);
        }
        else
        {
            choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        }

        TMP_Text choiceTMP = choiceButton.GetComponentInChildren<TMP_Text>();
        choiceTMP.text = choiceText;

        Button button = choiceButton.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClickAction());

        activeChoices.Add(choiceButton);
    }

    public void ClearChoices()
    {
        foreach (var choice in activeChoices)
        {
            choice.SetActive(false);
            choicePool.Enqueue(choice);
        }
        activeChoices.Clear();
    }
    IEnumerator ActivateAndStartCoroutine(GameObject container, TMP_Text dialogueTextComponent, string text)
    {
        // DialogBox를 활성화
        container.SetActive(true);

        // 한 프레임 대기 (DialogBox가 완전히 활성화될 때까지)
        yield return null;
        //  Debug.Log($"Before ShowContainer - DialogBox activeSelf: {dialogBox.activeSelf}, activeInHierarchy: {dialogBox.activeInHierarchy}");
        // 텍스트 출력 코루틴 실행
        typingCoroutine = StartCoroutine(TypeText(dialogueTextComponent, text));
    }
}