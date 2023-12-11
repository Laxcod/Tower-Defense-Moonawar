using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public Transform weaponHold;
    public Weapon startWeapon;
    Weapon equippedWeapon;

    void Start()
    {
        if (startWeapon !=null)
        {
            EquipWeapon (startWeapon);
        }
    }

    public void EquipWeapon(Weapon  weaponToEquip)
    {
        if (equippedWeapon != null)
        {
            Destroy (equippedWeapon.gameObject);
        }
        equippedWeapon = Instantiate (weaponToEquip, weaponHold.position, weaponHold.rotation) as Weapon;
        equippedWeapon.transform.parent = weaponHold;
    }

    public void Shoot ()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Shoot();
        }
    }
}
