using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWayPoint : MonoBehaviour
{
    protected Image img;

    [SerializeField]
    public GameObject target;

    protected Vector2 max;
    protected Vector2 min;

    [SerializeField]
    public Camera mainCamera;

    protected ObjectiveManager manager;

    [SerializeField]
    public string objectiveText;

    protected Text distanceText;

    protected virtual void Awake()
    {
        distanceText = GetComponentInChildren<Text>();

        img = GetComponent<Image>();

        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        max = new Vector2(maxX, maxY);
        min = new Vector2(minX, minY);
    }
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        manager = GetComponentInParent<ObjectiveManager>();
        manager.newObjective(this);
    }

    protected virtual void OnDestroy()
    {
       manager.endCurrentObjective(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        updateDistance();

        Vector2 pos = mainCamera.WorldToScreenPoint(target.transform.position);

        if (Vector3.Dot((target.transform.position - mainCamera.gameObject.transform.position), mainCamera.gameObject.transform.forward) < 0)
        {
            //target is behind the player
            if (pos.x < Screen.width / 2)
            {
                pos.x = max.x;
            }
            else
            {
                pos.x = min.y;
            }
        } 

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        img.transform.position = pos;
    }

    protected void updateDistance()
    {
        float distance = Vector3.Distance(target.transform.position,mainCamera.gameObject.transform.position);

        int distanceInt = (int)distance;
        
        distanceText.text = $"{distanceInt}m";

    }
}
