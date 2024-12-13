using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModalPanel : MonoBehaviour
{
    [Header("모달 창 UI")]
    public GameObject modalPanel;          // 모달 창 전체 패널
    public GameObject hintGrid;           // 힌트 아이템이 배치될 그리드
    public GameObject descriptionPanel;   // 설명 텍스트가 들어갈 판넬
    public TMP_Text descriptionText;      // 설명 텍스트 (TMP)

    [Header("닫기 버튼")]
    public Button closeButton;

    private void Start()
    {
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

    // 모달 창 열기
    public void OpenModal()
    {
        if (hintGrid.transform.childCount > 0) // 힌트 아이템이 존재할 때만 열기
        {
            modalPanel.SetActive(true);

            // 첫 번째 아이템 선택
            SelectFirstHintItem();
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
    }

    // 첫 번째 힌트 아이템 선택
    private void SelectFirstHintItem()
    {
        if (hintGrid.transform.childCount > 0)
        {
            // 첫 번째 아이템 가져오기
            Transform firstHint = hintGrid.transform.GetChild(0);
            if (firstHint != null)
            {
                // 설명 텍스트를 기본값으로 설정
                ShowHintDescription("기본 설명");
            }
        }
    }

    // 힌트 수집 시 설명 업데이트
    public void AddHintToGrid(Sprite hintSprite, string hintDescription)
    {
        // 힌트 아이템(이미지) 생성
        GameObject newHint = new GameObject("HintItem", typeof(Image));
        Image hintImage = newHint.GetComponent<Image>();
        hintImage.sprite = hintSprite;

        // 새 힌트를 Grid에 추가
        newHint.transform.SetParent(hintGrid.transform, false);

        // 클릭 시 설명 텍스트 변경 이벤트 추가
        Button hintButton = newHint.AddComponent<Button>();
        hintButton.onClick.AddListener(() => OnHintItemClick(hintDescription));
    }

    // 힌트 클릭 시 동작
    public void OnHintItemClick(string hintDescription)
    {
        // 클릭된 힌트에 해당하는 설명 표시
        ShowHintDescription("hintDescription");
    }

    // 설명 텍스트 업데이트
    public void ShowHintDescription(string description)
    {
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }
    }
}