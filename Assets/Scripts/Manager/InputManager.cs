using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static event Action OnMouseClick;
    public static event Action<RaycastHit> OnRaycastHit; // Raycast 결과 이벤트

    void Update()
    {

        // Debug.Log($"Update 호출 - Frame {Time.frameCount}");

        // 클릭했을 때만 UI 클릭 여부 확인
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            if (EventSystem.current.IsPointerOverGameObject()) // 클릭이 UI 위에서 발생
            {
                //  Debug.Log("UI 클릭으로 인해 무시됨");
                return; // UI 클릭 시 다른 처리는 하지 않음
            }

            // Debug.Log("Mouse Click Detected");

            // Raycast 처리
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Raycast Hit: {hit.collider.name}");
                OnRaycastHit?.Invoke(hit); // Raycast 이벤트 호출
            }
            else
            {
                OnMouseClick?.Invoke(); // 일반 클릭 이벤트 호출
            }
        }
    }
}