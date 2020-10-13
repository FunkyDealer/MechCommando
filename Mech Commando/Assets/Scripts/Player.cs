﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingEntity
{
    int currentArmor;
    [SerializeField]
    int maxArmor;
    
    public int currentEnergy;
    [SerializeField]
    int maxEnergy;
    protected int healthPacksQt;
    public readonly int healthPacksQtMax = 3;
    bool alive;
    public bool inControl;

    public delegate void PlayerDeath();
    public static event PlayerDeath onDeath;

    public delegate void UpdateHealthEvent(int h, int maxH);
    public static event UpdateHealthEvent onHealthUpdate;

    public delegate void UpdateEnergyEvent(int e, int maxE);
    public static event UpdateEnergyEvent onEnergyUpdate;

    public delegate void UpdateArmorEvent(int a, int maxA);
    public static event UpdateArmorEvent onArmorUpdate;

    public delegate void PrimaryFire();
    public static event PrimaryFire onPrimaryFire;

    //Energy
    float EnergyRecoverTimer;
    [SerializeField]
    readonly float EnergyRecoverTime = 0.025f; //Time Between Energy recovering
    bool canRecoverEnergy;
    [SerializeField]
    float EnergyRecoverDelay = 0.3f; //Time before energy starts recovering again
    float EnergyRecoverDelayTimer;

    

    protected override void Awake()
    {
        alive = true;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        inControl = true;
        EnergyRecoverTimer = 0;
        canRecoverEnergy = true;
        EnergyRecoverTimer = 0;
        currentArmor = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

        onHealthUpdate(currentHealth, maxHealth);
        onEnergyUpdate(currentEnergy, maxEnergy);
        onArmorUpdate(currentArmor, maxArmor);

    }

    // Update is called once per frame
    void Update()
    {
        recoverEnergy();

    }

    public bool isAlive() => alive;
    public int CurrentArmor() => currentArmor;
    public int MaxArmor() => maxArmor;
  
    
    public void spendEnergy(int newEnergy)
    {
        currentEnergy = newEnergy;
        onEnergyUpdate(currentEnergy, maxEnergy);
        canRecoverEnergy = false;
        EnergyRecoverDelayTimer = 0;
    }


    private void recoverEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            if (canRecoverEnergy)
            {
                if (EnergyRecoverTimer < EnergyRecoverTime) EnergyRecoverTimer += Time.deltaTime;
                else
                {
                    currentEnergy++;
                    onEnergyUpdate(currentEnergy, maxEnergy);
                    EnergyRecoverTimer = 0;
                }
            } else
            {
                if (EnergyRecoverDelayTimer < EnergyRecoverDelay) EnergyRecoverDelayTimer += Time.deltaTime;
                else { canRecoverEnergy = true; }
            }
        }
    }

    public void updateHealth(int newHealth)
    {
        currentHealth = newHealth;
        onHealthUpdate(currentHealth, maxHealth);
    }




    public override void die()
    {

        Debug.Log($"Player Died");

    }

    public void increaseHpak() => healthPacksQt++;

    public void increaseArmor(int ammount)
    {
        if (currentArmor + ammount < maxArmor) currentArmor += ammount;
        else currentArmor = maxArmor;

        onArmorUpdate(currentArmor, maxArmor);
    }

}
