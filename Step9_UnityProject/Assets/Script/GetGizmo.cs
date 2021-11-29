using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position, 0.2f);
    }
}
