using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Shield : MonoBehaviour
{
    int Shield;
    public string text;
    Text textDisplay;


    void Awake()
    {
        textDisplay = GetComponent<Text>();
        Player.onArmorUpdate += getShield;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnDestroy()
    {
        Player.onEnergyUpdate -= getShield;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void getShield(int S, int maxShield)
    {
        Shield = S;
        textDisplay.text = $"{text}: {Shield}/{maxShield}";
    }
}
