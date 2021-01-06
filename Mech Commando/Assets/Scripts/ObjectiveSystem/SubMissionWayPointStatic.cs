using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMissionWayPointStatic : MissionWayPointStatic
{

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
     

    }

    protected override void OnDestroy()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        try
        {
            if (distanceText != null) {
                updateDistance();
            }
            Vector2 pos = mainCamera.WorldToScreenPoint(target.transform.position);

            if (Vector3.Dot((target.transform.position - mainCamera.gameObject.transform.position), mainCamera.gameObject.transform.forward) < 0)
            {
                //target is behind the player
                img.enabled = false;
            }
            else img.enabled = true;

            //  pos.x = Mathf.Clamp(pos.x, minX, maxX);
            //  pos.y = Mathf.Clamp(pos.y, minY, maxY);

            img.transform.position = pos;
        }
        catch
        {
            Destroy(this.gameObject);
        }
    }
}
