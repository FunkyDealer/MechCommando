﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactible
{
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(GameObject actor)
    {
        if (active)
        {
            base.Interact(actor);

            Debug.Log("Button Pressed");
            active = false;
        }
    }
}
