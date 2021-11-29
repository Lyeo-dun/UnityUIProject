using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ViewAngle))]
public class ViewAngleEditor : Editor
{
    private void OnSceneGUI()
    {
        ViewAngle va = (ViewAngle)target;
        
        Handles.DrawWireArc(va.transform.position, Vector3.up, Vector3.forward, 360.0f, va.Radius);

        // ** 좌측 시야각 최대치.
        Vector3 LeftViewAngle = va.LocalViewAngle(-va.Angle / 2.0f);

        // ** 우측 시야각 최대치.
        Vector3 RightViewAngle = va.LocalViewAngle(va.Angle / 2.0f);

        Handles.DrawLine(va.transform.position, va.transform.position + LeftViewAngle * va.Radius);

        Handles.DrawLine(va.transform.position, va.transform.position + RightViewAngle * va.Radius);

        Handles.color = Color.green;
        for (int i = 0; i < va.TargetList.Count; ++i)
        {
            // ** DrawLine(Vector3 p1, Vector3 p2);
            Handles.DrawLine(va.transform.position, va.TargetList[i].position);
        }
    }
}
