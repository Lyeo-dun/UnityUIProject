using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun StartingGun;
    Gun equippedGun;

    void Start()
    {
        if(StartingGun != null)
        {
            EquipGun(StartingGun);
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if(equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate<Gun>(gunToEquip, weaponHold.position, weaponHold.rotation);
        equippedGun.transform.parent = weaponHold;
    }
    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
