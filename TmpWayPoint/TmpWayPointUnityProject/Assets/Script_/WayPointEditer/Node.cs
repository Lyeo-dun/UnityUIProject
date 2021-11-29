using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// ** 충돌체를 추가한다.
[RequireComponent(typeof(SphereCollider))]
public class Node : MonoBehaviour
{
    // ** 다음 노드를 가르키는 저장 공간.
    public Node NextNode = null;
    public int Index = 0;

    private void Start()
    {
        // ** 테그중 Node 테그를 찾아 셋팅한다.
        transform.tag = "Node";

        // ** 그리고 현재 스크립트를 포함한 오브젝트에 SphereCollider 를 포함한다.
        SphereCollider Coll = transform.GetComponent<SphereCollider>();

        // ** 반지름을 0.2f 롤 셋팅한다.
        Coll.radius = 0.2f;
    }
}

// ** Node 스크립트를 커스텀 한다.
[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    // ** 에디터 모드 (게임상에서는 보이지 않음.)
    private void OnSceneGUI()
    {
        // ** Node 를 갖는다.
        Node t = (Node)target;
        
        // ** 칼라값을 흰색으로 한다.
        Handles.color = Color.white;
        
        // ** 라인을 그린다.
        if(t.NextNode != null)
            Handles.DrawLine(t.transform.position, t.NextNode.transform.position);
    }
}


