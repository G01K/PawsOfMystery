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
    [SerializeField] private GameObject heroContainer;
    [SerializeField] private GameObject npcContainer;

    [Header("프리팹 및 컨테이너")]
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;
    private List<GameObject> activeChoices = new List<GameObject>();
    private Queue<GameObject> choicePool = new Queue<GameObject>();

    [Header("타이핑 애니메이션")]
    public float typingSpeed = 0.03f;
    private bool isTyping = false;
    private Coroutine typingCoroutine;


    void Start()
    {

        init();
        initBindFnc();
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

    void initBindFnc()
    {
        // External Function 연결
        story.BindExternalFunction("DisplayHint", (string scene) =>
        {
            Debug.Log("DisplayHintDisplayHintDisplayHintDisplayHint");

            DisplayHint(scene);
        });
    }

    void DisplayHint(string scene)
    {
        Debug.Log(scene);

        if (scene == "sniff_around")
        {
            List<Hint> hints = HintManager.Instance.GetHints("EnterForest");
            if (hints != null)
            {
                foreach (Hint hint in hints)
                {
                    HintManager.Instance.CreateHintObject(hint);
                }
            }

            // dlf
            //PauseStory();

            // ToDo : 다이얼로그박스 숨기기
        }
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
        isPaused = true;
    }

    public void ResumeStory()
    {
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
        if (isPaused) return;

        if (story.canContinue)
        {
            string text = story.Continue().Trim();
            HandleDialogue(text, speakerName);
        }
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }

    void HandleDialogue(string text, string speakerName = "")
    {
        string speaker = (speakerName == "주인공") ? speakerName : story.variablesState["currentSpeaker"]?.ToString();

        HideAllContainers();

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
        container.SetActive(true);

        TMP_Text dialogueTextComponent = container.transform.Find("DialogueText")?.GetComponent<TMP_Text>();
        if (dialogueTextComponent != null && dialogueTextComponent.text != text)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(dialogueTextComponent, text));
        }

        TMP_Text nameTextComponent = container.transform.Find("NameText")?.GetComponent<TMP_Text>();
        if (nameTextComponent != null && nameTextComponent.text != speakerName)
        {
            nameTextComponent.text = speakerName;
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
        textComponent.text = "";

        for (int i = 0; i < text.Length; i += 2) // 2글자씩 출력
        {
            textComponent.text = text.Substring(0, i + 1);
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

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
            CreateChoice(choice.text, () => OnChoiceSelected(choice));
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
}