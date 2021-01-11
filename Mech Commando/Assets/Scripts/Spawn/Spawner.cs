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
    [SerializeField]
    List<GameObject> positions;
    EnemyManager enemy;
    private void Start()
    {
        enemy = FindObjectOfType<EnemyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (limit > count)
        {
            Debug.Log("Criado");
            for (int n = 0; n<positions.Count;n++)
            {
                if (n <= limit)
                {
                    spawn.Create(positions[n].transform.position, enemy);
                    count++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
