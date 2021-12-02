using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed;

    // ** 현재 플레이어가 타겟을 조준하고 있는지를 판단한다
    private bool PlayerTargetAimming;
    // ** 플레이어가 쏘고 있는 중인지 판단한다.
    private bool PlayerShooting;

    private Vector3 ShootDir;

    private GameObject PlayerBulletPrefab;

    private void Start()
    {
        JoyStickManager.Instance.Player = this.gameObject;

        Speed = 10.0f;
        PlayerTargetAimming = false;
        PlayerShooting = false;

        PlayerBulletPrefab = Resources.Load("Prefab/Bullet") as GameObject;
        for (int i = 0; i < 5; i++)
        {
            GameObject Bullet = Instantiate<GameObject>(PlayerBulletPrefab, null);
            ObjectPool.Instance.CreateObject(ObjectKey.Bullet, Bullet);
        }
    }

    private void FixedUpdate()
    {
        Vector3 Dir = JoyStickManager.Instance.MoveDirection;
        float RotationValue = Mathf.Atan2(Dir.x, Dir.y) * Mathf.Rad2Deg;

        if(!PlayerTargetAimming && !PlayerShooting)
        {
            if(Dir.x != 0 || Dir.y != 0)
            {
                Quaternion rot = Quaternion.Euler(Vector3.up * RotationValue);
                transform.rotation = rot;
            }
        }
        
        if(PlayerTargetAimming)
        {
            ShootBullet();
        }

        Dir.z = Dir.y;
        Dir.y = 0;

        transform.position += Dir * JoyStickManager.Instance.MoveValue * Speed * Time.deltaTime;
    }

    private void ShootBullet()
    {
        Vector3 Dir = ShootDir;
        float RotationValue = Mathf.Atan2(Dir.x, Dir.y) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(Vector3.up * RotationValue);
        transform.rotation = rot;

        PlayerShooting = true;
        Debug.Log("Skill");

        StartCoroutine("PlayerShootingFinish", 1.0f);

        PlayerTargetAimming = false;
    }
    
    public void SetPlayerAim(bool _value, Vector3 _dir)
    {
        PlayerTargetAimming = _value;
        ShootDir = _dir;
    }

    private void SettingBullet()
    {
        if(ObjectPool.Instance.GetDisable.ContainsKey(ObjectKey.Bullet) && 
            ObjectPool.Instance.GetDisable[ObjectKey.Bullet].Count > 0)
        {
            GameObject Bullet = ObjectPool.Instance.GetDisable[ObjectKey.Bullet][0];
            ObjectPool.Instance.GetDisable[ObjectKey.Bullet].RemoveAt(0);

            Bullet.transform.parent = GameObject.Find("Objectpool/EnableList").transform;
            
            Debug.Log("총알 발사");
        }
        else
        {
            Debug.Log("총알 충전 후 발사");
        }
    }

    IEnumerator PlayerShootingFinish(float _time)
    {
        yield return new WaitForSeconds(_time);
        PlayerShooting = false;
    }
}
