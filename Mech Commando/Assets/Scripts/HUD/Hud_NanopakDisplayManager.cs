using UnityEngine;
using UnityEngine.UI;

public class Hud_NanopakDisplayManager : MonoBehaviour
{
    RawImage[] nanoPakDisplays;

    void Awake()
    {
        nanoPakDisplays = new RawImage[3];
        for (int i = 0; i < 3; i++)
        {
            nanoPakDisplays[i] = transform.Find($"Nanopak{i}").GetComponent<RawImage>();
            nanoPakDisplays[i].enabled = false;
        }
        Player.onNanopakUpdate += updateNanoPak;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateNanoPak(int n)
    {
        if (this != null)
        {
            for (int i = 0; i < 3; i++)
            {

                if (i <= n - 1) nanoPakDisplays[i].enabled = true;
                else nanoPakDisplays[i].enabled = false;
                //try
                //{
                //}
                //catch (MissingReferenceException)
                //{
                //    Execption(i);
                //}
            }
        }
    }

 
}
