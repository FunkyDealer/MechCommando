using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalObjectiveManager : MonoBehaviour
{
    [SerializeField]
    public List<StaticEntity> Enemies;
    public string location;
    private bool isEmpty;

    [SerializeField]
    ButtonBehaviour behaviour;

    public delegate void SubscriptionHandler(LocalObjectiveManager manager, string location);
    public static event SubscriptionHandler SubcribeSlaves;
           
    void Awake()
    {
        Enemies = new List<StaticEntity>();
        
    }


    // Start is called before the first frame update
    void Start()
    {
        SubcribeSlaves(this, location);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies.Count < 1)
        {
            behaviour.Run();
        }
    }
}
