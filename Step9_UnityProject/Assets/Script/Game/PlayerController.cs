using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody myRigidbody;
    Vector3 velocity;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();    
    }
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void  LookAt(Vector3 _lookpoint)
    {
        Vector3 heightcorrectedPoint = new Vector3(_lookpoint.x, transform.position.y, _lookpoint.z);
        transform.LookAt(heightcorrectedPoint);
    }
    void FixedUpdate()//정기적으로 반복하여 실행해야하기 때문에 사용한다
        //이동속도 유지 목적
    {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime); 
        //현재 위치 + 속력 * 한 프레임이 돌았을 때의 현재 시간(FixedUpdate 메소드가 호출된 시간)
    }
}
