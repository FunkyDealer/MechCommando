using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGateInv : MonoBehaviour
{
    Animator animator;

    bool open;

    [SerializeField]
    Color key;

    void Awake()
    {
        open = false;
        animator = transform.parent.gameObject.GetComponent<Animator>();

        animator.SetBool("Open", false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      


    }

    void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();
        if (p != null)
        {
            if (p.CheckKeys(key)) animator.SetBool("Open", true);
        }


    }

    void OnTriggerStay(Collider other)
    {


    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) animator.SetBool("Open", false);
    }


}
