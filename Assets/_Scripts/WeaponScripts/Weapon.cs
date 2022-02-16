using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Pistol,
    Machinegun
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst, PumpAction
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public WeaponFiringPattern weaponFiringPattern;
    public string weaponName;
    public float damage;
    public int bulletsInMag;
    public int magSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public int totalBullets;
}

public class Weapon : MonoBehaviour
{
    public Transform gripLocation;
    public WeaponStats weaponStats;
    protected WeaponHolder weaponHolder;

    [SerializeField]
    protected ParticleSystem firingEffect;

    public bool isFiring;
    public bool isReloading;

    protected Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Initialize(WeaponHolder _weaponHolder)
    {
        weaponHolder = _weaponHolder;
    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;

        if (weaponStats.repeating)
        {
            CancelInvoke(nameof(FireWeapon));
            //fire weapon
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
        
        if(firingEffect && firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
    }

    protected virtual void FireWeapon()
    {
        print("Firing weapon!");
        weaponStats.bulletsInMag--;
        print(weaponStats.bulletsInMag);
    }

    public virtual void StartReloading()
    {
        isReloading = true;
        ReloadWeapon();
    }

    public virtual void StopReloading()
    {
        isReloading = false;
    }

    protected virtual void ReloadWeapon()
    {
        if (firingEffect && firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }

        int bulletsToReload = weaponStats.magSize - weaponStats.totalBullets;

        if (bulletsToReload < 0)
        {
            weaponStats.bulletsInMag = weaponStats.magSize;
            weaponStats.totalBullets -= weaponStats.magSize;
        }
        else
        {
            weaponStats.bulletsInMag = weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }
}
