using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ink.Runtime;
using UnityEngine;
using Newtonsoft.Json;
public class HintManager : MonoBehaviour
{
    public static HintManager Instance { get; private set; }

    public Dictionary<String, List<Hint>> hintsByKey;

    public List<Hint> hintData;


    public List<Hint> hintsCreated = new List<Hint>();

    [SerializeField]
    private GameObject hintPrefab;


    [SerializeField]
    private GameObject hintContainer; // OK: 인스펙터에 보임


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadHints();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void removeAllHint()
    {
        if (hintsCreated.Count > 0)
        {
            foreach (Hint hint in hintsCreated)
            {
                Destroy(hint.hintObject);
                hintsCreated = new List<Hint>();
            }

        }
    }

    void LoadHints()
    {

        // Resources 폴더에서 JSON 파일 읽기
        TextAsset jsonFile = Resources.Load<TextAsset>("Hint/HintData");
        if (jsonFile != null)
        {

            HintCollection hintCollection = JsonUtility.FromJson<HintCollection>(jsonFile.text);
            hintData = hintCollection.hints;

            HintCollection hintCollection2 = JsonConvert.DeserializeObject<HintCollection>(jsonFile.text);
            hintsByKey = hintCollection2.hintsByKey;


        }
        else
        {
            Debug.LogError("Hints.json 파일을 찾을 수 없습니다!");
        }
    }

    public void CreateHintObject(Hint hint)
    {
        // 선행힌트가 없다면 만들지 않음
        if (hint.preConditionCode != null)
        {
            foreach (Hint savedHint in NoteManager.Instance.noteEntries)
            {
                if (!hint.preConditionCode.Equals(savedHint.code))
                {
                    Debug.Log($"선행힌트 {hint.preConditionCode}가 없어서 {hint.name}은 만들어지지 않음");
                    return;
                }

            }
        }
        hintsCreated.Add(hint);

        Transform parentTransform = hintContainer.transform;
        // 프리팹 인스턴스 생성
        GameObject hintObject = Instantiate(hintPrefab, parentTransform);

        // RectTransform 설정
        RectTransform rectTransform = hintObject.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = hint.position;
        }

        // 힌트 이미지 설정
        UnityEngine.UI.Image image = hintObject.GetComponent<UnityEngine.UI.Image>();
        if (image != null)
        {
            Sprite loadedSprite = Resources.Load<Sprite>(hint.imagePath);
            if (loadedSprite != null)
            {
                image.sprite = loadedSprite;
            }
            else
            {
                Debug.LogError($"Sprite not found at path: {hint.imagePath}");
            }
        }

        // 클릭 이벤트 연결
        UnityEngine.UI.Button button = hintObject.GetComponent<UnityEngine.UI.Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnHintClicked(hint));
        }

        hint.hintObject = hintObject;


    }

    // 힌트 클릭 이벤트
    void OnHintClicked(Hint hint, Action<Hint> onClickCallback = null)
    {
        Debug.Log($"Hint clicked: {hint.description}");

        // 클릭 시 처리할 로직 추가

        /*** To Do 
        * 1. 노트에 추가(잉크변수 동기화, 노트 매니저)
        * 2. 관련 대화 삽입
        * 3. 
        ***/

        // 노트에 추가 후 열기
        NoteManager.Instance.AddHintToNote(hint);

        NoteManager.Instance.OpenModal(hint);
        // 필드에서 수집한 힌트 지우기
        Destroy(hint.hintObject);

        //콜백(있으면)
        if (onClickCallback != null)
        {
            onClickCallback.Invoke(hint); // 콜백 함수 실행
        }

    }

    public Hint GetHint(string pTrigger, string code)
    {
        if (hintsByKey != null)
        {
            if (hintsByKey[pTrigger] != null)
            {
                return hintsByKey[pTrigger].Find(h => h.code == code);
            }
        }
        return null;
        /*
                if (hintData != null)
                {
                    return hintData.Find(h => h.id == id);
                }
                return null;
                */
    }

    public List<Hint> GetHints(string pTrigger)
    {

        if (hintsByKey != null && !string.IsNullOrEmpty(pTrigger))
        {
            List<Hint> noteEntries = NoteManager.Instance.noteEntries;

            string[] drawedHintsIds = hintsCreated.Select(hc => hc.code).ToArray();
            string[] collectedHintIds = noteEntries.Select(hc => hc.code).ToArray();
            if (hintsByKey[pTrigger] != null)
            {
                return hintsByKey[pTrigger].Where(h =>
                                    !drawedHintsIds.Contains(h.code)
                                    && !collectedHintIds.Contains(h.code))
                                    .ToList();
            }
        }
        /*
                if (hintData != null && !string.IsNullOrEmpty(pTrigger))
                {
                    List<Hint> noteEntries = NoteManager.Instance.noteEntries;

                    int[] drawedHintsIds = hintsCreated.Select(hc => hc.id).ToArray();
                    int[] collectedHintIds = noteEntries.Select(hc => hc.id).ToArray();
                    // 기존에 힌트가 있는 경우라면
                    if (drawedHintsIds.Length > 0 || collectedHintIds.Length > 0)
                    {
                        return hintData.Where(h => h.trigger.Equals(pTrigger)
                                            && !drawedHintsIds.Contains(h.id)
                                            && !collectedHintIds.Contains(h.id))
                                        .ToList();
                    }
                    return hintData.FindAll(h => h.trigger.Equals(pTrigger));
                }
                */
        return new List<Hint>();
    }

    public void DisplayHint(string scene)
    {
        Debug.Log(scene);

        // 이전 힌트들 있으면 다 지우기
        HintManager.Instance.removeAllHint();

        if ("EnterForest".Equals(scene)
            || "sniff_around".Equals(scene)
                || "goto_forest".Equals(scene)
                    || "start".Equals(scene)
                        || "choose_home".Equals(scene)
        )
        {
            List<Hint> hints = HintManager.Instance.GetHints("EnterForest");
            Debug.Log($"EnterForest hint count is : {hints.Count}");
            if (hints != null)
            {
                foreach (Hint hint in hints)
                {
                    hint.trigger = "EnterForest";
                    HintManager.Instance.CreateHintObject(hint);
                }
            }

            // 다이얼로그박스 숨기기
            //PauseStory();
        }
        else if ("goto_foxRoom".Equals(scene)
        || "FoxRoom".Equals(scene))
        {
            List<Hint> hints = HintManager.Instance.GetHints("FoxRoom");
            Debug.Log($"FoxRoom hint count is : {hints.Count}");

            if (hints != null)
            {
                foreach (Hint hint in hints)
                {
                    hint.trigger = "FoxRoom";
                    HintManager.Instance.CreateHintObject(hint);
                }
            }
        }
        else if ("33".Equals(scene))
        {


        }
        else if ("44".Equals(scene))
        {


        }
        else
        {
            List<Hint> hints = HintManager.Instance.GetHints("EnterForest");
            Debug.Log($"EnterForest hint count is : {hints.Count}");
            if (hints != null)
            {
                foreach (Hint hint in hints)
                {
                    hint.trigger = "EnterForest";
                    HintManager.Instance.CreateHintObject(hint);
                }
            }

        }
    }


    public void closeNoteModal(Hint clickedHint)
    {
        if ("EnterForest".Equals(clickedHint.trigger))
        {
            //최초 힌트 획득으로 열린 노트라면 닫으면서 스토리 진행
            InkManager.Instance.ChoosePathString("goto_forest.sniff_around");
        }
        else if ("FoxRoom".Equals(clickedHint.trigger))
        {
            switch (clickedHint.code)
            {
                case "apple":
                    InkManager.Instance.ChoosePathString("goto_foxRoom.get_hintApple");
                    break;
                case "giftBox":
                    InkManager.Instance.ChoosePathString("goto_foxRoom.get_hintCandle");
                    break;
                case "letter":
                    InkManager.Instance.ChoosePathString("goto_foxRoom.get_hintNeedHelp");
                    break;
            }
        }
        DialogueManager.Instance.ResumeStory();
    }

};

[System.Serializable]
public class Hint
{
    public int id;
    public string name;

    public string code;
    public string description;
    public string imagePath;
    public Vector2 position;
    public String trigger;
    public string preConditionCode;
    public GameObject hintObject;


};

[System.Serializable]
public class HintCollection
{
    public List<Hint> hints;

    public Dictionary<String, List<Hint>> hintsByKey;
};