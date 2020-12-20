using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTriggerStart : MonoBehaviour
{
    [SerializeField]
    GameObject objectiveManager;
    [SerializeField]
    GameObject objectivePrefab;
    [SerializeField]
    GameObject objectiveTarget;

    [SerializeField]
    string newObjectiveText;

    ObjectiveManager manager;


    void Awake()
    {
        manager = objectiveManager.GetComponent<ObjectiveManager>();
    }

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
        if (other.tag == "Player")
        {
            objectiveStart();

            Destroy(gameObject);
        }

    }

    public void objectiveStart()
    {
        GameObject o = Instantiate(objectivePrefab, Vector3.zero, Quaternion.identity, objectiveManager.transform);
        MissionWayPoint m = o.GetComponentInChildren<MissionWayPoint>();
        m.mainCamera = manager.playerCam;
        m.target = objectiveTarget;
        m.objectiveText = newObjectiveText;
        Destroy(this);
    }


}
