using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyComponent : MonoBehaviour
{
    [SerializeField]
    Color key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();
        if (p != null)
        {
            p.addKey(key);
            Destroy(gameObject);
        }


    }
}
