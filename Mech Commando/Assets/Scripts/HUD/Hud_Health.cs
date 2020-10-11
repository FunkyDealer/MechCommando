using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Health : MonoBehaviour
{
    int Health;
    public string text;
    Text textDisplay;


    void Awake()
    {
        textDisplay = GetComponent<Text>();
        Player.onHealthUpdate += getHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDestroy()
    {
        Player.onHealthUpdate -= getHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void getHealth(int H, int maxHealth)
    {
        Health = H;
        textDisplay.text = $"{text}: {Health}/{maxHealth}";
    }
}
