using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void OnWeaponEquippedEvent(Weapon weapon);
    public static event OnWeaponEquippedEvent OnWeaponEquipped;

    public static void invokeOnWeaponEquipped(Weapon weapon)
    {
        OnWeaponEquipped?.Invoke(weapon);
    }
}
