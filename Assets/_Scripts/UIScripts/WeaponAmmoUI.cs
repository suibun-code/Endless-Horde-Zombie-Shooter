using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponNameText;
    [SerializeField] TextMeshProUGUI currentBulletCountText;
    [SerializeField] TextMeshProUGUI totalBulletCountText;

    [SerializeField] Weapon weapon;

    /// <summary>
    /// set up events for onweaponequipped to handle the weapon component we grab
    /// </summary>

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    void OnWeaponEquipped(Weapon _weapon)
    {
        weapon = _weapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weapon)
            return;

        weaponNameText.text = weapon.weaponStats.weaponName;
        currentBulletCountText.text = weapon.weaponStats.bulletsInMag.ToString();
        totalBulletCountText.text = weapon.weaponStats.totalBullets.ToString();
    }
}
