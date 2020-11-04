using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenuAttribute(menuName = ("MenuButtonBehaviour/ChangeScreen"))]
public class ChangeScreen : ButtonBehaviour
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
        SceneManager.LoadScene(sceneToChange);



    }
}
