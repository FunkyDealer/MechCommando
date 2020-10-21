using System.Collections.Generic;
using UnityEngine;

public class EspecialAim : MonoBehaviour
{
    public GameObject ourGameObj;
    public Vector3 position;
    public List<GameObject> Inimigos;
    public float range;
    void Start()
    {
        position = ourGameObj.transform.position;
        Inimigos = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (Inimigos.Contains(other.gameObject) == false)
            {
                Inimigos.Add(other.gameObject);
            }
        }
    }

    void Update()
    {
        position = ourGameObj.transform.position;
        GameObject go = null;
        float distancia = range;

        foreach (GameObject g in Inimigos)
        {
            Vector3 p = g.transform.position;
            float diX = (position.x - p.x) * (position.x - p.x);
            float diY = (position.y - p.y) * (position.y - p.y);
            float diZ = (position.z - p.z) * (position.z - p.z);

            float di = Mathf.Sqrt(diX + diY + diZ);

            if (di < distancia)
            {
                distancia = di;
                go = g;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Disparar Bala
            Debug.Log("Piu - " + go.transform.position);
        }

        foreach (GameObject g in Inimigos)
        {
            if (g != go)
            {
                Inimigos.Remove(g);
                break;
            }
        }
    }
}
