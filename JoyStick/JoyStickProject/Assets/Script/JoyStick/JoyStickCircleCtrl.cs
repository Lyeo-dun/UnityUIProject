using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickCircleCtrl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float MaxDistance; // ** �߾ӿ��� ��Ʈ�ѷ������� �Ÿ� �ִ�
    
    private Color StandardColor; // ** �⺻ ��
    [SerializeField] private Color OnDragColor; // ** �巡�� �� ����� ��

    private void Start()
    {
        // ** MaxDisTance�� ���� �θ� ��ü�� ũ���� ���� �Ǿ�� ���� ���� ������ ������ �ʴ´�.
        MaxDistance = transform.parent.GetComponent<RectTransform>().sizeDelta.x / 2;

        StandardColor = GetComponent<Image>().color; // ** ���� �⺻ �÷��� ����
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().color = OnDragColor; // ** �巡�װ� ������ �� ���� �����Ѵ�
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 CirclePos = Vector2.zero; // ** ���̽�ƽ�� ���� ��Ʈ�ѷ��� �־�� �� ��ġ
        float CircleDistance = Vector2.Distance(transform.parent.position, eventData.position); 
        // ** �߾ӿ��� ���̽�ƽ ��Ʈ�ѷ����� �Ÿ�

        // ** ���� ����(���̽�ƽ �߽�)���� Ÿ��(���콺 �巡�� ��ġ)�� ������ ���Ѵ�
        Vector2 vecNomalize = eventData.position - (Vector2)transform.parent.position;
        vecNomalize.Normalize();

        // ** �߾� ���̽�ƽ ���� �ִ� �Ÿ����� �Ÿ��� �ִٸ�
        if (Mathf.Sqrt(CircleDistance) > Mathf.Sqrt(MaxDistance))
        {
            CircleDistance = MaxDistance; // ** �ִ� �Ÿ��� �����ش�
        }

        // ** ���̽�ƽ ������(0 ~ 1�� ��)�� ����
        transform.parent.GetComponent<JoyStickCtrl>().value = CircleDistance / MaxDistance;
        transform.parent.GetComponent<JoyStickCtrl>().Direction = vecNomalize;

        // ** ��Ʈ�ѷ��� ��ġ�� ����
        CirclePos = vecNomalize * CircleDistance; // ** (���� * �Ÿ� => �� ������ �Ÿ���ŭ�� ��ġ)
        transform.position = CirclePos + (Vector2)transform.parent.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent.GetComponent<JoyStickCtrl>().Mode == JoyStickMode.Rotate)
        {
            // ** ���� ��� ���̽�ƽ�̶�� ���ػ��¸� ������ �����ϰ� ���̽�ƽ�� ���Ҵ� ������ ������ �����Ѵ�.
            JoyStickManager.Instance.PlayerAimming(true, transform.parent.GetComponent<JoyStickCtrl>().Direction);
        }

        // ** ��Ʈ�ѷ��� ���� ���� �߾� ��ġ�� ���� �����̱� �� �� ���·� �ʱ�ȭ�Ѵ�.
        transform.position = (Vector2)transform.parent.position;
        transform.parent.GetComponent<JoyStickCtrl>().value = 0;

        // ** ����� ���Ⱚ�� �ʱ�ȭ ��Ų��
        transform.parent.GetComponent<JoyStickCtrl>().Direction = Vector2.zero;

        // ** ���� �ʱ���·� �����Ѵ�.
        GetComponent<Image>().color = StandardColor;
    }
}
