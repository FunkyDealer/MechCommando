using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHead : MonoBehaviour
{
    [SerializeField]
    float thrust = 1.0f;
    Rigidbody rb;
    Vector3 direction;

    [SerializeField]
    GameObject DeathHud;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(DeathHud);
        Cursor.lockState = CursorLockMode.None;

        direction = Vector3.up;
        direction = Quaternion.AngleAxis(-25, Vector3.forward) * direction;
        direction = Quaternion.AngleAxis(25, Vector3.right) * direction;

        transform.up = direction;

        rb = GetComponent<Rigidbody>();


        rb.AddForce(direction * thrust);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
