using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeatHud : MonoBehaviour
{
    [HideInInspector]
    public MainWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon == null || !weapon.overHeated)
        {
            Destroy(this.gameObject);
        }
    }

    
}
