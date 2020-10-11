using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Energy : MonoBehaviour
{
    int Energy;
    public string text;
    Text textDisplay;


    void Awake()
    {
        textDisplay = GetComponent<Text>();
        Player.onEnergyUpdate += getEnergy;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDestroy()
    {
        Player.onEnergyUpdate -= getEnergy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void getEnergy(int E, int maxEnergy)
    {
        Energy = E;
        textDisplay.text = $"{text}: {Energy}/{maxEnergy}";
    }

}
