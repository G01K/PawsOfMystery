using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }

    [Header("모달 창 UI")]
    public GameObject modalPanel;          // 모달 창 전체 패널
    public Transform hintGrid;            // 힌트 아이템이 배치될 그리드 (Grid Layout Group 필요)
    public TMP_Text descriptionText;      // 설명 텍스트 (TMP)

    [Header("닫기 버튼")]
    public Button closeButton;

    // 노트에 추가된 힌트를 저장
    public List<Hint> noteEntries;

    [SerializeField]
    private GameObject hintPrefab;

    private Hint clickedHint = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
            noteEntries = new List<Hint>();
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }

        // 닫기 버튼 초기화
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseModal);
        }

        // 초기 상태: 모달 창 비활성화
        if (modalPanel != null)
        {
            modalPanel.SetActive(false);
        }
    }

    // 노트에 힌트 추가
    public void AddHintToNote(Hint hint)
    {
        if (!noteEntries.Contains(hint))
        {
            //힌트 수집
            noteEntries.Add(hint);
            //잉크 동기화

            InkManager.Instance.AddItemToInventory(hint.code);

            // string Invent22ory = InkManager.Instance.GetVariable("Inventory");
            // Debug.Log("Inventory's Hand: " + Invent22ory);

            List<string> Inventory = InkManager.Instance.GetVariableAsList("Inventory");
            Debug.Log("Player's Hand: " + string.Join(", ", Inventory));

            Debug.Log($"노트에 추가됨: {hint.code}");
            UpdateNoteUI();
        }
        else
        {
            Debug.Log($"이미 노트에 추가된 힌트: {hint.code}");
        }
    }

    // 노트 UI를 갱신하는 함수
    public void UpdateNoteUI()
    {
        // 기존 UI 요소 제거
        foreach (Transform child in hintGrid.transform)
        {
            Destroy(child.gameObject);
        }

        // 노트의 모든 항목 추가
        foreach (Hint hint in noteEntries)
        {
            GameObject noteItem = Instantiate(hintPrefab, hintGrid.transform);

            // HintComponent 추가 및 데이터 설정
            HintComponent hintComponent = noteItem.GetComponent<HintComponent>();
            if (hintComponent != null)
            {
                hintComponent.hint = hint; // Hint 데이터 설정
            }
            else
            {
                Debug.LogError($"HintComponent not found on {noteItem.name}");
            }

            // HintImage 설정
            Image hintImage = noteItem.transform.Find("HintImage").GetComponent<Image>();
            if (hintImage != null)
            {
                Sprite loadedSprite = Resources.Load<Sprite>(hint.imagePath);
                if (loadedSprite != null)
                {
                    hintImage.sprite = loadedSprite;
                }
                else
                {
                    Debug.LogError($"Sprite not found for hint: {hint.name}");
                }
            }

            // 클릭 이벤트 추가
            Button hintButton = noteItem.GetComponent<Button>();
            if (hintButton != null)
            {
                hintButton.onClick.AddListener(() => OnHintItemClick(hint));
            }
        }

        Debug.Log("노트 UI 업데이트 완료");
    }


    // 모달 창 열기
    public void OpenModal(Hint hint = null)
    {
        if (noteEntries.Count > 0)
        {
            modalPanel.SetActive(true);

            //힌트 획득과 동시에 열렸다면
            if (hint != null)
            {
                clickedHint = hint;
                Debug.Log("noteEntries.Count : " + noteEntries.Count);
                Debug.Log("방금 수집한 힌트 : " + noteEntries[noteEntries.Count - 1].name);
                OnHintItemClick(clickedHint);
            }
            else
            {
                SelectFirstHintItem();
            }
        }
        else
        {
            Debug.LogWarning("힌트 아이템이 없습니다.");
        }
    }

    // 모달 창 닫기
    public void CloseModal()
    {
        if (modalPanel != null)
        {
            modalPanel.SetActive(false);
        }
        if (clickedHint != null)
        {
            HintManager.Instance.closeNoteModal(clickedHint);
        }
    }

    // 첫 번째 힌트 아이템 선택
    private void SelectFirstHintItem()
    {
        if (noteEntries.Count > 0)
        {
            OnHintItemClick(noteEntries[0]);
        }
    }

    // 노트에서 증거 클릭 시 동작
    public void OnHintItemClick(Hint selectedHint)
    {
        // 1. 설명 텍스트 업데이트
        if (descriptionText != null)
        {
            descriptionText.text = selectedHint.description;
        }

        // 2. 모든 힌트를 순회하며 색상 업데이트
        foreach (Transform child in hintGrid.transform)
        {
            // 부모 Panel의 Image 컴포넌트를 가져옴
            Image panelBackground = child.GetComponent<Image>();

            // 자식 HintImage를 가져옴
            Image hintImage = child.Find("HintImage")?.GetComponent<Image>();

            if (panelBackground != null)
            {
                HintComponent hintComponent = child.GetComponent<HintComponent>();
                if (hintComponent != null && hintComponent.hint.id == selectedHint.id)
                {
                    // 선택된 Panel: 밝은 색상 유지
                    panelBackground.color = new Color(0.9f, 0.9f, 0.9f, 1f); // 연한 회색, 불투명
                    if (hintImage != null)
                    {
                        hintImage.color = Color.white; // White (기본 색상)
                    }
                }
                else
                {
                    // 선택되지 않은 Panel: 약간 어둡게
                    panelBackground.color = new Color(0.6f, 0.6f, 0.6f, 0.9f); // 회색, 약간 어두움
                    if (hintImage != null)
                    {
                        hintImage.color = new Color(0.6f, 0.6f, 0.6f, 0.9f); // HintImage도 약간 어둡게
                    }
                }
            }
        }

        Canvas.ForceUpdateCanvases(); // UI 업데이트 강제 적용
    }
}