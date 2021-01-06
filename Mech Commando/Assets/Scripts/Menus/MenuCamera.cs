using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Target Hit: " + hit.collider.gameObject.name);
        }
        else
        {
            //Debug.Log("Nothing is being hit");
        }
       // Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));
    }
}
