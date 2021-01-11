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

    public void Create(Vector3 POS , EnemyManager enemy)
    {
        GameObject game;
        Enemy e;
        game = Instantiate(gameObject,POS,Quaternion.identity);
        e = game.GetComponent<Enemy>();
        e.SubcribeToManager(enemy);
    }
}

