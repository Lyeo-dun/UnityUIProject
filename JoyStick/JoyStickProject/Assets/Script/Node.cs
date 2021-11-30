using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(SphereCollider))]
public class Node : MonoBehaviour
{
    public Node NextNode;

    private void Start()
    {
        transform.tag = "Node";
        SphereCollider Coll = transform.GetComponent<SphereCollider>();

        Coll.radius = 0.2f;

    }
}

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    private void OnSceneGUI()
    {
        Node t = (Node)target;
        
        Handles.color = Color.white;
        Handles.DrawLine(t.transform.position, t.NextNode.transform.position);
    }
}


