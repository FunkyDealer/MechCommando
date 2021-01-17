using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDropper : MonoBehaviour
{
    [SerializeField]
    GameObject Object2Drop;

    Transform pos2Spawn;

    // Start is called before the first frame update
    void Start()
    {
        pos2Spawn = transform.Find("Center").transform;
    }

    void OnDestroy()
    {
        Instantiate(Object2Drop, pos2Spawn.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
