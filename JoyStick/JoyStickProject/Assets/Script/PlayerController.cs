using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed;

    private void Start()
    {
        Speed = 10.0f;
    }

    private void Update()
    {
        Vector3 Dir = JoyStickManager.Instance.Direction;
        Dir.z = Dir.y;
        Dir.y = 0;

        transform.position += Dir * Speed * Time.deltaTime;
    }
}
