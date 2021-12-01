using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** Rigidbody Component 를추가한다.
[RequireComponent(typeof(Rigidbody))]

// ** BoxCollider Component 를추가한다.
//[RequireComponent(typeof(BoxCollider))]

// ** CapsuleCollider Component 를추가한다.
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyController : MonoBehaviour
{
    // ** WayPoint Node의 부모
    [SerializeField] private GameObject ParentObj;

    // ** 목표 Node
    [SerializeField] private Node TargetNode = null;

    // ** 움직일지 확인
    private bool Moving = false;

    // ** 돌아서 갈지 확인
    private bool isTurn = false;

    private void Awake()
    {
        // ** 노드의 부모를 받아온다.
        ParentObj = GameObject.Find("Parent");
    }

    void Start()
    {
        // ** 현재 스크립트를 포함한 오브젝트의 Rigidbody 를 받아온다.
        Rigidbody Rigid = transform.GetComponent<Rigidbody>();

        // ** 중력를 끔
        Rigid.useGravity = false;

        // ** 현재 스크립트를 포함한 오브젝트의 BoxCollider 를 받아온다.
        CapsuleCollider Coll = transform.GetComponent<CapsuleCollider>();

        // ** 콜라이더의 중앙 위치를 변경
        Coll.center = new Vector3(0.0f, 1.0f, 0.0f);
        Coll.height = 2.0f;

        // ** 충돌 형식을 충돌했는지의 판단여부만 확인할 수 있도록 한다.
        Coll.isTrigger = true;

        // ** 목표로 설정할 Node가 있는지 확인. (2개 이상 되어야 움직임)
        StartCoroutine("NodeChecking");
    }
    
    void Update()
    {
        // ** 움직일 준비가 되었다면...
        if(Moving)
        {
            // ** 목표지점 으로 향하는 방향벡터를 구한다.
            Vector3 Direction = (TargetNode.transform.position - transform.position).normalized;

            // ** 방향 으로 이동한다.
            transform.position += Direction * 1.5f * Time.deltaTime;

            // ** 목표지점을 바라보게 한다.
            transform.LookAt(TargetNode.transform);

            // ** 현재 위치와 목표지점간의 라인을 그린다. (확인용)
            Debug.DrawLine(this.transform.position, TargetNode.transform.position, Color.red);
        }
    }

    // ** 충돌 했다면 
    private void OnTriggerEnter(Collider other)
    {
        // ** 충돌한 목표지점의 이름을 확인한다. 맞다면 다음 노드를 가르킨다.
        if (TargetNode && other.transform.name == ("Node " + TargetNode.Index))
            TargetNode = TargetNode.NextNode;
    }

    IEnumerator NodeChecking()
    {
        // ** 계속 확인한다.
        while (true)
        {
            // ** 0.5초 간격으로
            yield return new WaitForSeconds(0.5f);

            // ** root node에 node가 있는지 확인한다. (2개 이상이어야 함.)
            if (ParentObj.transform.childCount > 1)
            {
                // ** 첫번째 노드를 찾는다.
                TargetNode = ParentObj.transform.GetChild(0).GetComponent<Node>();

                // ** 움직일 준비가 되었다는것을 알림.
                Moving = true;

                // ** 이 루프를 종료한다.
                break;
            }
        }
    }
}
