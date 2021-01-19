using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Player.onDeath += OnDeath;
    }

    void OnDestroy()
    {
        Player.onDeath -= OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
