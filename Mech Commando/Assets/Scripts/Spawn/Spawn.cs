using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = ("Spawn/Entity"))]
public class Spawn : ScriptableObject
{
    [SerializeField]
    GameObject gameObject;
    void Start()
    {
    }

    void Update()
    {
    }

    public void Create(Vector3 POS)
    {
        Instantiate(gameObject,POS,Quaternion.identity);
    }
}

