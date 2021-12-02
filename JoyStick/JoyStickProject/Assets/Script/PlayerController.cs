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

        CreateBullet();
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

        GameObject Bullet = SettingBullet();
        if(Bullet)
        {
            Bullet.transform.rotation = transform.rotation;

            Bullet.SetActive(true);
        }

        StartCoroutine("PlayerShootingFinish", 1.0f);
    }
    
    public void SetPlayerAim(bool _value, Vector3 _dir)
    {
        PlayerTargetAimming = _value;
        ShootDir = _dir;
    }

    private GameObject SettingBullet()
    {
        GameObject Bullet = null;

        // ** 사용하지 않는 총알이 있다면 사용하는 총알 리스트로 옮기고 총알을 반환한다
        if(ObjectPool.Instance.GetDisable.ContainsKey(ObjectKey.Bullet) && 
            ObjectPool.Instance.GetDisable[ObjectKey.Bullet].Count > 0)
        {
            Bullet = AddEnableBullet(); 
            return Bullet;
        }

        //// ** 사용하지 않는 총알이 없다면 새로운 총알을 만들고 하나를 사용하는 총알 리스트로 옮긴다
        CreateBullet();
        Bullet = AddEnableBullet();

        return Bullet;
    }

    private GameObject AddEnableBullet()
    {
        GameObject _Bullet = ObjectPool.Instance.GetDisable[ObjectKey.Bullet][0];
        ObjectPool.Instance.GetDisable[ObjectKey.Bullet].RemoveAt(0);

        _Bullet.transform.parent = GameObject.Find("Objectpool/EnableList").transform;
        _Bullet.transform.position = transform.position + Vector3.up * 1.0f;

        ObjectPool.Instance.AddObject(true, ObjectKey.Bullet, _Bullet);

        return _Bullet;
    }
    private void CreateBullet()
    {
        PlayerBulletPrefab = Resources.Load("Prefab/Bullet") as GameObject;
        for (int i = 0; i < 5; i++)
        {
            GameObject Bullet = Instantiate<GameObject>(PlayerBulletPrefab, null);
            ObjectPool.Instance.CreateObject(ObjectKey.Bullet, Bullet);
        }
    }

    IEnumerator PlayerShootingFinish(float _time)
    {
        PlayerTargetAimming = false;
        yield return new WaitForSeconds(_time);
        PlayerShooting = false;
    }
}
