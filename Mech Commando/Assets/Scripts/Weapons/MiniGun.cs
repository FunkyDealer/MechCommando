using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : MainWeapon, IMainWeapon
{
    bool primaryPressed;
    bool secondaryPressed;
    
    enum FiringState
    {
        Idle,
        StartSpin,
        Spinning,
        Firing,
        EndSpining
    }
    [SerializeField]
    FiringState state;

    [SerializeField]
    float spinTime;
    [SerializeField]
    float spinTimer;

    void Awake()
    {
        spinTimer = 0;        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        state = FiringState.Idle;
        primaryPressed = false;
        secondaryPressed = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


        switch (state)
        {
            case FiringState.Idle:
                break;
            case FiringState.StartSpin:
                if (spinTimer < spinTime) spinTimer += Time.deltaTime;
                else {
                    if (primaryPressed) state = FiringState.Firing;
                    else state = FiringState.Spinning;
                 spinTimer = 0;
                }
                break;
            case FiringState.Spinning:

                break;
            case FiringState.Firing:
                 PrimaryFire();
                break;
            case FiringState.EndSpining:
                if (spinTimer < spinTime) spinTimer += Time.deltaTime;
                else { state = FiringState.Idle; spinTimer = 0; }
                break;
            default:
                break;
        }    
    }

    public override void PrimaryFireStart(WeaponManager weaponManager)
    {
        manager = weaponManager;
        primaryPressed = true;

        switch (state)
        {
            case FiringState.Idle:
                state = FiringState.StartSpin;
                break;
            case FiringState.StartSpin:
                
                break;
            case FiringState.Spinning:
                PrimaryFire();
                state = FiringState.Firing;
                break;
            case FiringState.Firing:                
                state = FiringState.Firing;
                PrimaryFire();
                break;
            case FiringState.EndSpining:
                state = FiringState.StartSpin;
                break;
            default:
                break;
        }

    }

    public override void PrimaryFireEnd()
    {
        primaryPressed = false;
        switch (state)
        {
            case FiringState.Idle:

                break;
            case FiringState.StartSpin:
                state = FiringState.EndSpining;
                break;
            case FiringState.Spinning:
                if (secondaryPressed) state = FiringState.Spinning;
                else state = FiringState.EndSpining;
                break;
            case FiringState.Firing:
                if (secondaryPressed) state = FiringState.Spinning;
                else state = FiringState.EndSpining;
                break;
            case FiringState.EndSpining:

                break;
            default:
                break;
        }
    }

    public override void SecondaryFireStart(WeaponManager weaponManager)
    {
        secondaryPressed = true;
        switch (state)
        {
            case FiringState.Idle:
                if (!primaryPressed) state = FiringState.StartSpin;
                break;
            case FiringState.StartSpin:
                //Stays as it is
                break;
            case FiringState.Spinning:
                //Stays as it is
                break;
            case FiringState.Firing:
                //Stays as it is
                break;
            case FiringState.EndSpining:
                if (!primaryPressed) state = FiringState.StartSpin;
                break;
            default:
                break;
        }
    }

    public override void SecondaryFireEnd()
    {
        secondaryPressed = false;
        switch (state)
        {
            case FiringState.Idle:
                //Stays as it is
                break;
            case FiringState.StartSpin:
                if (!primaryPressed) state = FiringState.EndSpining;
                break;
            case FiringState.Spinning:
                if (!primaryPressed) state = FiringState.EndSpining;
                break;
            case FiringState.Firing:
                //Stays as it is
                //if (!primaryPressed) state = FiringState.EndSpining;
                break;
            case FiringState.EndSpining:
                //Stays as it is
                break;
            default:
                break;
        }
    }


    private void PrimaryFire()
    {
        if (!overHeated && canFirePrimary && manager.currentPrimaryAmmo > 0)
        {
            heatUp();
            // Debug.Log($"Primary Weapon Firing: Primary Fire");
            fireDelayTimer = 0;
            canFirePrimary = false;
            manager.currentPrimaryAmmo--;
            manager.updateAmmo();

            foreach (Transform s in ShootPlaces)
            {
                GameObject a = Instantiate(projectile, s.position, Quaternion.identity);
                Bullet b = a.GetComponent<Bullet>();
                b.direction = transform.forward;
                b.damage = baseDamage;
            }
        }
    }
}
