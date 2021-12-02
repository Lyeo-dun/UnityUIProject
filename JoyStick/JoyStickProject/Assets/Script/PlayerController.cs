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

        // ** ������� �ʴ� �Ѿ��� �ִٸ� ����ϴ� �Ѿ� ����Ʈ�� �ű�� �Ѿ��� ��ȯ�Ѵ�
        if(ObjectPool.Instance.GetDisable.ContainsKey(ObjectKey.Bullet) && 
            ObjectPool.Instance.GetDisable[ObjectKey.Bullet].Count > 0)
        {
            Bullet = AddEnableBullet(); 
            return Bullet;
        }

        //// ** ������� �ʴ� �Ѿ��� ���ٸ� ���ο� �Ѿ��� ����� �ϳ��� ����ϴ� �Ѿ� ����Ʈ�� �ű��
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
