using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = ("ButtonBehaviour/Enable Object"))]
public class enableObject : ButtonBehaviour
{
    [SerializeField]
    GameObject Object2Enable;

    [SerializeField]
    string objectName;


    public override void Initialize()
    {
        base.Initialize();
        Object2Enable = GameObject.Find(objectName);
        Object2Enable.SetActive(false);
        Debug.Log(Object2Enable);
    }



    public override void Run()
    {


        Object2Enable.SetActive(true);



    }
}
