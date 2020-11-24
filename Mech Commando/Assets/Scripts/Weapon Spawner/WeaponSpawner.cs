using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    Player player;
    bool activated;
    Transform DropPodPosition;
    [SerializeField]
    GameObject test;
    [SerializeField]
    GameObject dropPod;

    void Awake()
    {
        player = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        DropPodPosition = transform.Find("DropPodSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            player = other.GetComponent<Player>();

            SpawnDropPod();
            activated = true;
        }
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {

    }

    void SpawnDropPod()
    {
        Debug.Log("Spawning dropPod");

        GameObject a = Instantiate(dropPod, DropPodPosition.position, Quaternion.identity);
        DropPod d = a.GetComponent<DropPod>();
        d.weapon = test;
        // g = a.GetComponent<>();


    }

}
