using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweeenShots = 100;
    //연사력, ms단위의 총을 쏘고 난 후 다음 총알을 얼마간의 텀을 두고 쏠 것인지
    public float muzzleVelocity = 35; //발사되는 순간의 총알 속도

    float nextShotTime = 0; //총을 쏠수 있는지 시간을 재는 변수


    public void Shoot()
    {
        if (Time.time > nextShotTime) //현재 시간 > 다음 총을 쏠 시간
        {
            nextShotTime = Time.time + msBetweeenShots / 1000; //현재 시간 + 연사력/1000(ms이므로)
            Projectile newProjectile =
                Instantiate<Projectile>(projectile, muzzle.position, muzzle.rotation);
        }
    }

}
