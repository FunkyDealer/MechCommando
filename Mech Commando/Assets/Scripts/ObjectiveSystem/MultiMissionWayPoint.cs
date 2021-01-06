using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiMissionWayPoint : MissionWayPoint
{
    LocalObjectiveManager objectiveManager;
    
    List<MissionWayPoint> WayPoints;

    GameObject template;

    protected override void Awake()
    {

        WayPoints = new List<MissionWayPoint>();

       
    }

    // Start is called before the first frame update
    protected override void Start()
    {     
        template = GetComponentInChildren<SubMissionWayPointStatic>().gameObject;

        objectiveManager = target.GetComponent<LocalObjectiveManager>();

        foreach (var e in objectiveManager.Enemies)
        {
           // Debug.Log("creating 1 objective");
            GameObject a = Instantiate(template, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            SubMissionWayPointStatic m = a.GetComponent<SubMissionWayPointStatic>();
            m.target = e.gameObject;
            m.mainCamera = mainCamera;
            WayPoints.Add(m);
        }
        Destroy(template);

        manager = GetComponentInParent<ObjectiveManager>();
        manager.newObjective(this);
    }

    protected override void OnDestroy()
    {
        manager.endCurrentObjective(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        

     //   if (objectiveManager.Enemies.Count < 1) Destroy(this.gameObject);
      //  else {
     //   }


    }
}
