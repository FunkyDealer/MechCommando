using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Option : MonoBehaviour
{
    bool selected;
    GameObject selection;
    [SerializeField]
    ButtonBehaviour behaviour;
    

    void Awake()
    {
        selected = false;
        selection = transform.Find("Selection").gameObject;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        selection.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        


    }

    void OnMouseOver()
    {
        // do mouse hover stuff
        selection.SetActive(true);
        selected = true;
    }

    void OnMouseExit()
    {
        // reset to normal
        selected = false;
        selection.SetActive(false);
    }

    void Activate()
    {
        behaviour.Run();
    }
}
