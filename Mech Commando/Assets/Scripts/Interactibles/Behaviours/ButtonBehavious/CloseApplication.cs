using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = ("ButtonBehaviour/CloseApplication"))]
public class CloseApplication : ButtonBehaviour
{
    [SerializeField]
    string sceneToChange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void Run()
    {
        Application.Quit();



    }
}