using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletControler : MonoBehaviour
{
    private Vector3 StartPos; // ** 총알이 발사된 위치
    private Vector3 Dir;
    private bool BulletMove;

    public float Dis;

    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        Initialize();
    }
    private void OnEnable()
    {
        Initialize();
    }
    private void Initialize()
    {
        Dis = 0;
        StartPos = transform.position;
        BulletMove = true;

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        GetComponent<Rigidbody>().AddForce(transform.forward * 50.0f, ForceMode.Impulse);
    }
    private void Update()
    {
        if (BulletMove)
        {
            Dis = Vector3.Distance(StartPos, transform.position);
            if (Dis >= 20.0f)
            {
                DestroySelf();
            }
        }
    }

    void DestroySelf()
    {
        gameObject.transform.parent = GameObject.Find("DisableList").transform;
        ObjectPool.Instance.AddObject(false, ObjectKey.Bullet, gameObject);

        BulletMove = false;
        gameObject.SetActive(false);
    }
}
