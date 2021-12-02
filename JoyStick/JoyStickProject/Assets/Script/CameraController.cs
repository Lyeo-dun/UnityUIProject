using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Player;

    private Vector3 Offset;
    private float Angle;
    private void Awake()
    {
        Player = GameObject.Find("Player");
    }
    void Start()
    {
        Offset = new Vector3(0, 15.0f, -15.0f);
        Angle = 41.3f;

        transform.position = Player.transform.position + Offset;
        transform.rotation = Quaternion.Euler(Angle, transform.rotation.y, transform.rotation.z);
    }

    void Update()
    {
        transform.position = Player.transform.position + Offset;
    }
}
