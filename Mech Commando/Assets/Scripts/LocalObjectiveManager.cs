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
    List<ButtonBehaviour> behaviours;

    public delegate void SubscriptionHandler(LocalObjectiveManager manager, string location);
    public static event SubscriptionHandler SubcribeSlaves;

    ObjectiveTriggerStart objectiveTrigger;

    void Awake()
    {
        Enemies = new List<StaticEntity>();
        objectiveTrigger = GetComponent<ObjectiveTriggerStart>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SubcribeSlaves(this, location);

        foreach (var b in behaviours)
        {
            b.Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies.Count < 1)
        {
            foreach (var b in behaviours)
            {
                b.Run();
            }

            if (objectiveTrigger != null) objectiveTrigger.objectiveStart();
        }
    }
}
