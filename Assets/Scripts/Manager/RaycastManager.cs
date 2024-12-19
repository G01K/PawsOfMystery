// using UnityEngine;

// public class RaycastManager : MonoBehaviour
// {
//     void Update()
//     {
//         // 마우스 왼쪽 버튼 또는 터치 입력
//         if (Input.GetMouseButtonDown(0))
//         {
//             // 카메라에서 클릭된 위치로 Ray를 발사
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//             if (Physics.Raycast(ray, out RaycastHit hit))
//             {
//                 // 클릭된 오브젝트에서 HintObject 컴포넌트를 가져옵니다.
//                 HintObject hintObject = hit.collider.GetComponent<HintObject>();
//                 if (hintObject != null)
//                 {
//                     hintObject.OnHintClicked(); // 클릭 이벤트 실행
//                 }
//                 else
//                 {
//                     Debug.Log($"Clicked on: {hit.collider.name} (No HintObject script attached)");
//                 }
//             }
//         }
//     }
// }