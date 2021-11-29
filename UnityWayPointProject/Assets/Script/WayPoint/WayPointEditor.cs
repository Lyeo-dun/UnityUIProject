using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointEditor : EditorWindow
{
    // ** 매뉴바에 "Tools"이라는 새로운 매뉴를 추가한다.
    // ** 그리고 "Tools"매뉴 아래로 "WayPoint Editor"라는 매뉴를 포함 시킨다.
    [MenuItem("Tools/WayPoint Editor")]

    //** 매뉴가 실행되면 다음과 같이 윈도우를 생성하여 보이게 한다.
    static public void Initialize()
    {
        WayPointEditor Window = GetWindow<WayPointEditor>();
        Window.Show();
    }

    [Tooltip("부모 노드")]
    public GameObject ParentNode = null;
    
    private void OnGUI()
    {
        // ** Serialized된 오브젝트를 같는다.
        SerializedObject Obj = new SerializedObject(this);

        // ** 현재 스크립트에서 갖고있는 Property 중에 "ParentNode"라는 이름의 Property를 찾는다.
        EditorGUILayout.PropertyField(
            Obj.FindProperty("ParentNode"));

        // ** 만약 ParentNode에 아무것도 없다면...
        if (ParentNode == null)
        {
            // ** 아무것도 없다는 메시지를 띄운다.
            EditorGUILayout.HelpBox("root node 없음", MessageType.Warning);
        }
        else
        {
            // ** GUILayout.Width(); GUILayout.Height();
            // ** GUILayout.MinWidth(); GUILayout.MinHeight();
            // ** GUILayout.MaxWidth(); GUILayout.MaxHeight();

            // ** 생성된 윈도우 창의 레이아웃을 위 옵션으로 변경할 수 있다.
            // ** 옵션 추가는 마음대로할 수 있다.
            EditorGUILayout.BeginVertical();

            // ** 버튼을 생성한다. 그리고 버튼이 눌렸는지 확인다.
            // ** 버튼이 눌렸다면 CreateNode() 함수를 실행한다.
            if (GUILayout.Button("Create Node"))
                CreateNode();

            // ** 레이아웃 편집을 종료한다.
            EditorGUILayout.EndVertical();
        }

        // ** 현재 위 코드 내용을 적용시킴.
        Obj.ApplyModifiedProperties();
    }

    // ** 새로운 노드를 갖는다.
    public void CreateNode()
    {
        // ** 오브젝트를 생성한다. 그리고 오브젝트의 이름을 "Node 0", "Node 1", "Node 2", "Node 3" 등등.... 으로 설정해준다.
        GameObject NodeObj = new GameObject("Node " + ParentNode.transform.childCount);

        // ** 생성된 오브젝트의 부모를 ParentNode로 설정해준다.
        NodeObj.transform.SetParent(ParentNode.transform);

        // ** 그리고 생선된 오브젝트에 컴퍼넌트를 추가한다.
        // ** GetGizmo = 위치확인용 
        NodeObj.AddComponent<GetGizmo>();

        // ** Node = 다음 노드를 가르키는 용으로 사용된다.
        // ** 생성된 오브젝트에 노드 컴퍼넌트를 받아온다.
        Node CurrentNode = NodeObj.AddComponent<Node>();

        // ** 노드의 인덱스를 생성한다.
        CurrentNode.Index = ParentNode.transform.childCount - 1;


        while (true)
        {
            // ** 생성된 노드의 위치를 랜덤하게 셋팅한다.
            // ** 현재는 (-5 ~ +5) 사이값으로 생성한다.
            // ** 이때, Y좌표에 값은 변경 시키지 않는다. 
            NodeObj.transform.position = new Vector3(
                Random.Range(-25.0f, 25.0f), 0.0f, Random.Range(-25.0f, 25.0f));

            float Distance = 1000.0f;

            // ** 만약 ParentNode의 하위에 자식노드의 개수가 2개 이상이라면...
            if (ParentNode.transform.childCount > 1)
            {
                // ** 이 전 노드를 찾는다.
                Node PreviousNode = ParentNode.transform.GetChild(ParentNode.transform.childCount - 2).GetComponent<Node>();

                // ** 찾은 이전 노드에 현재 노드를 넣는다.
                PreviousNode.NextNode = ParentNode.transform.GetChild(ParentNode.transform.childCount - 1).GetComponent<Node>();

                // ** 그리고 현재 노드에 첫번째 노드를 넣어 반복될 수 있도록 한다.
                CurrentNode.NextNode = ParentNode.transform.GetChild(0).GetComponent<Node>();

                Distance = Vector3.Distance(PreviousNode.transform.position, CurrentNode.transform.position);
            }

            if (Distance > 1.5f)
                break;
        }
    }
}
