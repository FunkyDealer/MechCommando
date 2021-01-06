using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Spawn spawn;
    [SerializeField]
    int limit;
    int count;
    [SerializeField]
    GameObject pos;
    [SerializeField]
    GameObject pos2;


    private void OnTriggerEnter(Collider other)
    {
        if (limit > count)
        {
            Debug.Log("Criado");
            
            spawn.Create(pos.transform.position);
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
