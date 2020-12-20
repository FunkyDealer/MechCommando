using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBombHolo : Button
{
    [SerializeField]
    GameObject Bomb;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(GameObject actor)
    {
        if (active)
        {
            foreach (var b in behaviours)
            {
                b.Run();
            }
       //     Debug.Log("bomb planted");
            spawnBomb();
            active = false;
            Destroy(gameObject);
        }
    }


    void spawnBomb()
    {
        Instantiate(Bomb, transform.position, transform.rotation);
    }

}
