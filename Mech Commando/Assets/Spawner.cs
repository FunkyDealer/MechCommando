using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject Effect;

    AudioSource audioClip;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        audioClip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(EnemyManager manager)
    {
        audioClip.Play();
        Instantiate(Effect, transform.position, transform.rotation);

       GameObject o = Instantiate(enemy, transform.position, transform.rotation);
        Enemy e = o.GetComponent<Enemy>();
        e.SubcribeToManager(manager);
    }
}
