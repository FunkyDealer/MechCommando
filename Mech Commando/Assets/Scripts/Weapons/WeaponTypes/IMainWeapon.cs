using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMainWeapon
{
    //For Pressing down Button
    void PrimaryFireStart(WeaponManager weaponManager);

    void PrimaryFireEnd();

    void SecondaryFireStart(WeaponManager weaponManager);

    void SecondaryFireEnd();


}
