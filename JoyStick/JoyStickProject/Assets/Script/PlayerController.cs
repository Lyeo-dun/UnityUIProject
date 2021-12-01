using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed;

    // ** ���� �÷��̾ Ÿ���� �����ϰ� �ִ����� �Ǵ��Ѵ�
    private bool PlayerTargetAimming;
    // ** �÷��̾ ��� �ִ� ������ �Ǵ��Ѵ�.
    private bool PlayerShooting;

    private void Start()
    {
        Speed = 10.0f;
        PlayerTargetAimming = false;
        PlayerShooting = false;
    }

    private void FixedUpdate()
    {
        Vector3 Dir = JoyStickManager.Instance.MoveDirection;
        Dir.z = Dir.y;
        Dir.y = 0;

        if(!PlayerTargetAimming && !PlayerShooting)
        {
            if(Dir.x != 0 || Dir.y != 0)
            {
                Quaternion rot = Quaternion.LookRotation(Dir);
                transform.rotation = rot;
            }
        }
        
        if(PlayerTargetAimming)
        {
            Skill1();
        }
        
        transform.position += Dir * JoyStickManager.Instance.MoveValue * Speed * Time.deltaTime;
    }

    private void Skill1()
    {

        Vector3 Dir = JoyStickManager.Instance.ShootingDirection;
        Dir.z = Dir.y;
        Dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(Dir);
        transform.rotation = rot;

        PlayerShooting = true;
        //...

        StartCoroutine("PlayerShootingFinish", 1.0f);

    }
    
    IEnumerator PlayerShootingFinish(float _time)
    {
        yield return new WaitForSeconds(_time);
        PlayerShooting = false;
    }
}
