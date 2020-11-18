using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = ("ButtonBehaviour/Play Animation"))]
public class PlayAnimation : ButtonBehaviour
{
    [SerializeField]
    string location;

    public delegate void ButtonPressed(string location);
    public static event ButtonPressed onPress;

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

        onPress(location);



    }
}
