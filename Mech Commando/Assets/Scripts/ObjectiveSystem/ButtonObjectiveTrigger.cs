using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObjectiveTrigger : Interactible
{
    protected bool active;

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
        active = true;
        manager = objectiveManager.GetComponent<ObjectiveManager>();
    }
    
    // Start is called before the first frame update
    protected void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact(GameObject actor)
    {
        if (active)
        {
            base.Interact(actor);
            GameObject o = Instantiate(objectivePrefab, Vector3.zero, Quaternion.identity, objectiveManager.transform);
            MissionWayPoint m = o.GetComponentInChildren<MissionWayPoint>();
            m.mainCamera = actor.transform.Find("Main Camera").gameObject.GetComponent<Camera>();
            m.target = objectiveTarget;
            m.objectiveText = newObjectiveText;

            active = false;
        }
    }
}
