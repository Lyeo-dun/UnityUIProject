using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 2000.0f, ForceMode.Impulse);
    }
}
