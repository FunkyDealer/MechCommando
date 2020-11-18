using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField]
    string location;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Open", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayAnimation.onPress += PlayAnim;
    }

    void OnDestroy()
    {
        PlayAnimation.onPress -= PlayAnim;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAnim(string l)
    {
        anim.SetBool("Open", true);

    }
}
