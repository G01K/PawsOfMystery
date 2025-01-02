using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public Button sniffButton;
    public Button noteButton;
    public static UIManager Instance { get; private set; }

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
    // 냄새킁킁
    private int freeSniffUseCnt = 100; // 일 무료 횟수
    private int addSniffUseCnt = 0; // 일 광고 가능 횟수
    private int MaxSniffUses => freeSniffUseCnt + addSniffUseCnt; // 일 총 킁킁가능 횟수
    private int sniffUsedCnt = 0; // 일 사용된 냄새킁킁 횟수


    private bool isCooldown = false;
    private DateTime lastResetTime;

    public GameObject modalPanel;


    [SerializeField]
    public RectTransform buttonTransform;

    [SerializeField]
    public float pulseSpeed = 2.0f;

    [SerializeField]
    public float maxScale = 2.0f;
    [SerializeField]
    public float minScale = 1.0f;

    private bool pulsingUp = true;

    // 냄새킁킁을 할 시기에 버튼 강조 애니메이션
    public void StartButtonAnimation()
    {
        StartCoroutine(ButtonPulseCoroutine(3.0f)); // 3초 동안 애니메이션 실행
    }

    private IEnumerator ButtonPulseCoroutine(float duration)
    {
        float elapsedTime = 0f; // 애니메이션 실행 시간 누적
        pulsingUp = true; // 초기 방향 설정

        while (elapsedTime < duration)
        {
            if (pulsingUp)
            {
                buttonTransform.localScale = Vector3.MoveTowards(
                    buttonTransform.localScale,
                    Vector3.one * maxScale,
                    Time.deltaTime * pulseSpeed
                );

                if (Mathf.Abs(buttonTransform.localScale.x - maxScale) <= 0.01f)
                {
                    pulsingUp = false;
                }
            }
            else
            {
                buttonTransform.localScale = Vector3.MoveTowards(
                    buttonTransform.localScale,
                    Vector3.one * minScale,
                    Time.deltaTime * pulseSpeed
                );

                if (Mathf.Abs(buttonTransform.localScale.x - minScale) <= 0.01f)
                {
                    pulsingUp = true;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 애니메이션 종료 후 원래 크기로 복귀
        buttonTransform.localScale = Vector3.one * minScale;
    }
    void Start()
    {
        lastResetTime = DateTime.Now;
        if (IsNewDay())
        {
            ResetSniffUses();
        }
        UpdateSniffButton();


    }
    public void OnSniffButtonClick()
    {
        if (isCooldown)
        {
            Debug.Log("냄새 킁킁 버튼은 쿨다운 중입니다!");
            return;
        }

        if (sniffUsedCnt < MaxSniffUses)
        {
            if (sniffUsedCnt < freeSniffUseCnt)
            {// 일 무료횟수가 남아있다면
                sniffUsedCnt++;
                Debug.Log("냄새 킁킁 사용: " + sniffUsedCnt + "/" + MaxSniffUses);
                PerformSniffAction();
                UpdateSniffButton();

            }
            else
            { //광고횟수 사용
              // 광고컨펌? 혹은 자동진행? 
                Boolean isConfirm = true;
                if (isConfirm)
                {
                    // 광고로직 추가
                    sniffUsedCnt++;
                    Debug.Log(" 광고 냄새 킁킁 사용: " + sniffUsedCnt + "/" + MaxSniffUses);
                    PerformSniffAction();
                    UpdateSniffButton();
                }
            }

        }
        else
        {
            Debug.Log("냄새 킁킁 사용 불가. 오늘의 횟수를 모두 소진했습니다.");
        }
    }

    public void OnNoteButtonClick()
    {
        Debug.Log("노트 버튼 클릭!");
        modalPanel.SetActive(true);
        modalPanel.GetComponent<NoteManager>().OpenModal();
    }

    void UpdateSniffButton()
    {
        sniffButton.interactable = sniffUsedCnt < MaxSniffUses && !isCooldown;
    }

    private async void PerformSniffAction()
    {
        string knotName = InkManager.Instance.GetKnotName();
        HintManager.Instance.DisplayHint(knotName);

        Debug.Log("냄새 킁킁 행동 실행!");
        isCooldown = true;
        UpdateSniffButton();

        await Task.Delay(TimeSpan.FromSeconds(5)); // 비동기 대기
        isCooldown = false;
        UpdateSniffButton();
    }
    private IEnumerator SniffCooldownCoroutine(float cooldownTime)
    {
        isCooldown = true;
        UpdateSniffButton();
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
        UpdateSniffButton();
    }

    private bool IsNewDay()
    {
        DateTime now = DateTime.Now;
        if (now.Date > lastResetTime.Date)
        {
            lastResetTime = now;
            return true;
        }
        return false;
    }

    private void ResetSniffUses()
    {
        sniffUsedCnt = 0;
        UpdateSniffButton();
        Debug.Log("새로운 하루 시작! 냄새 킁킁 버튼 초기화.");
    }

    public void ChangeRoom(string roomName)
    {
        // 배경화면 바꾸기
        // 힌트 초기화 및 불러오기
        switch (roomName)
        {
            case "EnterForest":
                break;
            case "FoxRoom":
                break;
            case "PeacockRoom":
                break;
            case "GoatRoom":
                break;

        }
    }
}