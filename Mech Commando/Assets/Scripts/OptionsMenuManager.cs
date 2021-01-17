using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [HideInInspector]
    public PauseMenu pauseMenu;

    Slider slider;

    [SerializeField]
    Text sensivityText;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = SettingsManager.Instance.Sensivity();
        sensivityText.text = slider.value.ToString("N2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSensivity(System.Single s)
    {
        SettingsManager.Instance.NewSensivity(s);
        ChangeSensivityText(s);
    }

    void ChangeSensivityText(float s)
    {
        sensivityText.text = s.ToString("N2");
    }
}
