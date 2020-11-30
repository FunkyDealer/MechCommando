using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    public List<Enemy> Enemies;
    private Player thePlayer;


    public delegate void SubscriptionHandler(EnemyManager manager);
    public static event SubscriptionHandler SubcribeSlaves;


    void Awake()
    {
        Enemies = new List<Enemy>();
        thePlayer = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SubcribeSlaves(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player getPlayer() => thePlayer;
}
