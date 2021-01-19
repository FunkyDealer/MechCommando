using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base_Hud : MonoBehaviour
{
    public string text;
    protected Text textDisplay;

    protected virtual void Awake()
    {
        textDisplay = GetComponent<Text>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
