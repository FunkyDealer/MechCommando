using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField]
    float seconds;
    float timer;

    void Awake()
    {
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < seconds) timer += Time.deltaTime;
        else Destroy(gameObject);
    }
}
