using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitingArea : MonoBehaviour
{
    Player p;

    // Start is called before the first frame update
    void Start()
    {
        p = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (p == null) p = other.gameObject.GetComponent<Player>();

            p.inPlayableArea = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            p.inPlayableArea = true;
        }
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            p.inPlayableArea = false;
        }
    }


    }
