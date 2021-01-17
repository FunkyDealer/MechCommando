using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    float sensivity = 10f;


    [SerializeField]
    float Volume = 1;


    private static SettingsManager _instance;
    public static SettingsManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Sensivity() => sensivity;

    public void NewSensivity(float sensivity)
    {
        this.sensivity = sensivity;
    }
}
