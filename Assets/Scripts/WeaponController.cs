using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public Transform weaponHold;
    Weapon equippedWeapon;

    public void EquipWeapon(Weapon  weaponToEquip)
    {
        if (equippedWeapon != null)
        {
            Destroy (equippedWeapon.gameObject);
        }
        equippedWeapon = Instantiate (weaponToEquip);
    }
}
