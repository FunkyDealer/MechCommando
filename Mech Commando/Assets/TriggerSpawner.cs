using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    [SerializeField]
    List<Spawner> spawners;

    [SerializeField]
    EnemyManager manager;

    bool activated;

    [SerializeField]
    bool timer;
    [SerializeField]
    float delay;

    void Awake()
    {
        activated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            if (!timer) ActivateSpawners();
            else ActivateSpawnersTimer();
            activated = true;
        }
    }


    void ActivateSpawners()
    {
        foreach (var s in spawners)
        {
            s.Spawn(manager);
        }
    }


    void ActivateSpawnersTimer()
    {
        
        float currentDelay = 0;
        foreach (var s in spawners)
        {
            StartCoroutine(ExecuteAfterTime(currentDelay, s));
            currentDelay += delay;
        }
    }

    IEnumerator ExecuteAfterTime(float time, Spawner s)
    {
        yield return new WaitForSeconds(time);
        s.Spawn(manager);
       // Debug.Log("Spawning");
    }

}
