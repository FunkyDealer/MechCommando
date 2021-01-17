using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField]
    string nextScene;

    [SerializeField]
    GameObject completeCanvas;
    [SerializeField]
    ButtonBehaviour behaviour;

    [SerializeField]
    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player p = other.gameObject.GetComponent<Player>();
            p.inControl = false;

            RemoveCanvas();

            GameObject c = Instantiate(completeCanvas, Vector3.zero, Quaternion.identity);
            ActionTimer a = c.GetComponent<ActionTimer>();
            a.behaviour = behaviour;

            enemyManager.DestroyAllEnemies();
        }
    }

    //Removes all canvas fom game
    void RemoveCanvas()
    {
        Canvas[] a = FindObjectsOfType<Canvas>();

        foreach (var c in a)
        {
            Destroy(c.gameObject);
        }

    }


}
