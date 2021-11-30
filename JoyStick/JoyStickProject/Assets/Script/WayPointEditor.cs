using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointEditor : EditorWindow
{
    [MenuItem("Tools/WayPoint Editor")]

    static public void Initialize()
    {
        WayPointEditor Window = GetWindow<WayPointEditor>();
        Window.Show();
    }

    //[Tooltip("")]
    public GameObject ParentNode = null;

    private void OnGUI()
    {
        SerializedObject Obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(
            Obj.FindProperty("ParentNode"));

        if(ParentNode == null)
        {
            EditorGUILayout.HelpBox("root node 없음", MessageType.Warning);
        }
        else
        {
            // ** GUILayout.Width(); GUILayout.Height();
            // ** GUILayout.MinWidth(); GUILayout.MinHeight();
            // ** GUILayout.MaxWidth(); GUILayout.MaxHeight();

            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Create Node"))
                CreateNode();

            EditorGUILayout.EndVertical();
        }

        // ** 현재 위 코드 내용을 적용시킴.
        Obj.ApplyModifiedProperties();
    }


    public void CreateNode()
    {
        GameObject NodeObj = new GameObject("Node " + ParentNode.transform.childCount);
        NodeObj.transform.SetParent( ParentNode.transform );

        NodeObj.AddComponent<GetGizmo>();
        NodeObj.AddComponent<Node>();

        NodeObj.transform.position = new Vector3(
            Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));


        Node node = NodeObj.GetComponent<Node>();

        if (ParentNode.transform.childCount > 1)
        {
            node.NextNode = ParentNode.transform.GetChild(ParentNode.transform.childCount - 2).GetComponent<Node>();
            
            GameObject FirstObj = GameObject.Find("Node " + 0);
            Node FirstNode = FirstObj.GetComponent<Node>();
            FirstNode.NextNode = node;
        }
    }
}
