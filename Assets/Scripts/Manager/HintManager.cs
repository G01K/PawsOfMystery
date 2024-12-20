using System;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance { get; private set; }

    public List<Hint> hintData;

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

    void LoadHints()
    {

        // Resources 폴더에서 JSON 파일 읽기
        TextAsset jsonFile = Resources.Load<TextAsset>("Hint/HintData");
        if (jsonFile != null)
        {
            HintCollection hintCollection = JsonUtility.FromJson<HintCollection>(jsonFile.text);
            hintData = hintCollection.hints;
        }
        else
        {
            Debug.LogError("Hints.json 파일을 찾을 수 없습니다!");
        }
    }

    public void CreateHintObject(Hint hint)
    {
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

    public Hint GetHint(int id)
    {
        if (hintData != null)
        {
            return hintData.Find(h => h.id == id);
        }
        return null;
    }

    public List<Hint> GetHints(string trigger)
    {
        if (hintData != null && !string.IsNullOrEmpty(trigger))
        {
            return hintData.FindAll(h => h.trigger == trigger);
        }
        return new List<Hint>();
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
    public GameObject hintObject;

};

[System.Serializable]
public class HintCollection
{
    public List<Hint> hints;
};