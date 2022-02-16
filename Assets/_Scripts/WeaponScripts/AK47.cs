using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Weapon
{
    protected override void FireWeapon()
    {
        Vector3 hitLocation;

        if (weaponStats.bulletsInMag > 0 && !isReloading)
        {
            base.FireWeapon();

            if (firingEffect)
            {
                firingEffect.Play();
            }

            Ray screenRay = mainCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;
                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);
            }
        }
        else if (weaponStats.bulletsInMag <= 0)
        {
            //trigger a reload when no bullets left
            weaponHolder.StartReloading();
        }

    }
}
