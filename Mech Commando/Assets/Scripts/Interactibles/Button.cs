using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactible
{
    protected bool active;
    [SerializeField]
    protected List<ButtonBehaviour> behaviours;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        active = true;
        foreach (var b in behaviours)
        {
            b.Initialize();
        }
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

            foreach (var b in behaviours)
            {                
                b.Run();
            }

            //Debug.Log("Button Pressed");
            active = false;            
        }
    }

}
