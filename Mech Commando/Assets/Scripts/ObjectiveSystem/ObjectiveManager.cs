using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    List<MissionWayPoint> objectiveMarkers;

    [SerializeField]
    MissionWayPoint currentObjective;

    Text objectiveText;

    public Camera playerCam;

    void Awake()
    {
        GameObject t = transform.Find("ObjectiveText").gameObject;
        objectiveText = t.GetComponent<Text>();
        objectiveText.text = null;
        playerCam = FindObjectOfType<Player>().gameObject.transform.Find("Main Camera").gameObject.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player.onDeath += OnDeath;
    }

    void OnDestroy()
    {
        Player.onDeath -= OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addObjective()
    {

    }


    public void DeleteObjective()
    {
        Destroy(currentObjective.gameObject);
        currentObjective = null;       


    }

    public void newObjective(MissionWayPoint newObj)
    {
        if (currentObjective != null)
        {
            MissionWayPoint oldObj = currentObjective;
            Destroy(oldObj.gameObject);            
        }

        currentObjective = newObj;
        changeObjectiveText(newObj.objectiveText);
    }


    public void changeObjectiveText(string newText)
    {
        objectiveText.text = newText;
    }

    public void endCurrentObjective(MissionWayPoint m)
    {
        if (m == currentObjective) objectiveText.text = null;

    }

    void OnDeath()
    {
        Destroy(gameObject);
    }
}
