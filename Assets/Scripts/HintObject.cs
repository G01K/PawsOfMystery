using UnityEngine;

public class HintObject : MonoBehaviour
{
    void OnEnable()
    {
        InputManager.OnRaycastHit += HandleRaycastHit;
    }

    void OnDisable()
    {
        InputManager.OnRaycastHit -= HandleRaycastHit;
    }

    void HandleRaycastHit(RaycastHit hit)
    {
        if (hit.collider.gameObject == gameObject) // 본인인지 확인
        {
            Debug.Log($"HintObject clicked: {gameObject.name}");
            OnHintClicked();
        }
    }

    public void OnHintClicked()
    {
        Debug.Log("Hint Clicked");
        gameObject.SetActive(false); // 오브젝트 비활성화
    }
}