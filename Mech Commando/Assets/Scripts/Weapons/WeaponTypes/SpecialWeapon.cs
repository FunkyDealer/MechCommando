using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeapon : Weapon
{
    protected bool isLocked;
    [SerializeField]
    Transform camera;
    AutoAim mira;
    GameObject projectile;
    void Start()
    {
        mira = new AutoAim();
        mira.nos = camera;
    }

    // Update is called once per frame
    void Update()
    {
        mira.Update();
    }

    void Disparo()
    {
        GameObject a = Instantiate(projectile, this.transform.position, Quaternion.identity);
        AntiTankMissile tankMissile = a.GetComponent<AntiTankMissile>();
        tankMissile.go = mira.GetCloser();

    }
}
