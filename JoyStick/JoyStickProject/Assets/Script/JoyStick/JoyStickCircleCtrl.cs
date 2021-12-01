using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickCircleCtrl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float MaxDistance; // ** 중앙에서 컨트롤러까지의 거리 최댓값
    
    private Color StandardColor; // ** 기본 색
    [SerializeField] private Color OnDragColor; // ** 드래그 시 변경될 색

    private void Start()
    {
        // ** MaxDisTance의 값은 부모 객체의 크기의 반이 되어야 원이 라인 밖으로 나가지 않는다.
        MaxDistance = transform.parent.GetComponent<RectTransform>().sizeDelta.x / 2;

        StandardColor = GetComponent<Image>().color; // ** 현재 기본 컬러를 저장
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().color = OnDragColor; // ** 드래그가 시작할 때 색을 변경한다
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 CirclePos = Vector2.zero; // ** 조이스틱의 원형 컨트롤러가 있어야 할 위치
        float CircleDistance = Vector2.Distance(transform.parent.position, eventData.position); 
        // ** 중앙에서 조이스틱 컨트롤러와의 거리

        // ** 현재 원점(조이스틱 중심)에서 타겟(마우스 드래그 위치)의 방향을 구한다
        Vector2 vecNomalize = eventData.position - (Vector2)transform.parent.position;
        vecNomalize.Normalize();

        // ** 중앙 조이스틱 원이 최대 거리보다 거리가 멀다면
        if (Mathf.Sqrt(CircleDistance) > Mathf.Sqrt(MaxDistance))
        {
            CircleDistance = MaxDistance; // ** 최대 거리로 맞춰준다
        }

        // ** 조이스틱 벨류값(0 ~ 1의 값)을 세팅
        transform.parent.GetComponent<JoyStickCtrl>().value = CircleDistance / MaxDistance;
        transform.parent.GetComponent<JoyStickCtrl>().Direction = vecNomalize;

        // ** 컨트롤러의 위치를 세팅
        CirclePos = vecNomalize * CircleDistance; // ** (방향 * 거리 => 그 방향의 거리만큼의 위치)
        transform.position = CirclePos + (Vector2)transform.parent.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent.GetComponent<JoyStickCtrl>().Mode == JoyStickMode.Rotate)
        {
            // ** 조준 모드 조이스틱이라면 조준상태를 참으로 변경하고 조이스틱을 놓았던 마지막 방향을 리턴한다.
            JoyStickManager.Instance.PlayerAimming(true, transform.parent.GetComponent<JoyStickCtrl>().Direction);
        }

        // ** 컨트롤러를 놓는 순간 중앙 위치와 값을 움직이기 전 원 상태로 초기화한다.
        transform.position = (Vector2)transform.parent.position;
        transform.parent.GetComponent<JoyStickCtrl>().value = 0;

        // ** 저장된 방향값을 초기화 시킨다
        transform.parent.GetComponent<JoyStickCtrl>().Direction = Vector2.zero;

        // ** 색을 초기상태로 변경한다.
        GetComponent<Image>().color = StandardColor;
    }
}
