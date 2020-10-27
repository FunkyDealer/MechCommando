using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public Transform nos;
    //Distancia do Inimigo
    public float visao = 10;
    //tag do inimigo 
    public string tag = "Enemy";
    //Raios por camada em X 
    public float raiosPorCamada = 100;
    //Angulo de visao 
    public float angVisao = 120;
    //numero de camadas em Y  
    public int numCamadas = 50;
    //Distancia entre cada camada de Y  
    public float distCm = 0.1f;


    public List<GameObject> inimTemp, Inimigos;
    RaycastHit ray;

    void Start()
    {
        inimTemp = new List<GameObject>();
        Inimigos = new List<GameObject>();
    }

    // Update is called once per frame
    public void Update()
    {
        //InimigosAimOne();
        InimigosAim();
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetCloser();
        }
    }


    void InimigosAimOne()
    {

        if (Physics.Raycast(nos.position, nos.forward, out ray, visao))
        {
            Debug.DrawLine(nos.position, nos.forward, Color.red);
            if (ray.collider.gameObject.CompareTag(tag))
            {

                if (!inimTemp.Contains(ray.collider.gameObject))
                {
                    inimTemp.Add(ray.collider.gameObject);
                }
                if (!Inimigos.Contains(ray.collider.gameObject))
                {
                    Inimigos.Add(ray.collider.gameObject);
                }
            }
        }
        else
        {
            Debug.DrawLine(nos.position, nos.forward, Color.green);
        }

        for (int i = 0; i < Inimigos.Count; i++)
        {
            if (!inimTemp.Contains(Inimigos[i]))
            {
                Inimigos.Remove(Inimigos[i]);
            }
        }
        inimTemp.Clear();

    }
    void InimigosAim()
    {

        float limiteCamadas = numCamadas * 0.5f;
        //RaiosExtraPorCamada
        for (int j = 0; j <= raiosPorCamada; j++)
        {
            for (float i = -limiteCamadas; i <= limiteCamadas; i++)
            {
                float angDoRay = j * (angVisao / raiosPorCamada);
                Vector3 directionPraDivisaoRay = (-nos.transform.right) + (nos.up * i * distCm);
                Vector3 dir = Quaternion.AngleAxis(angDoRay, nos.up) * directionPraDivisaoRay * 100;



                if (Physics.Raycast(nos.position, dir, out ray, visao))
                {
                    if (ray.collider.gameObject.tag == tag)
                    {
                        Debug.DrawLine(nos.position, dir, Color.red);
                        if (!inimTemp.Contains(ray.collider.gameObject))
                        {
                            inimTemp.Add(ray.collider.gameObject);
                        }
                        if (!Inimigos.Contains(ray.collider.gameObject))
                        {
                            Inimigos.Add(ray.collider.gameObject);
                        }
                    }
                }
                else
                {

                    Debug.DrawLine(nos.position, dir, Color.green);
                }

            }
        }

        for (int i = 0; i < Inimigos.Count; i++)
        {
            if (!inimTemp.Contains(Inimigos[i]))
            {
                Inimigos.Remove(Inimigos[i]);
            }
        }
        inimTemp.Clear();
    }

    public GameObject GetCloser()
    {
        GameObject maisProximo = null;
        float distancia = 10000;

        foreach (GameObject g in Inimigos)
        {
            float di = Vector3.Distance(nos.position, g.transform.position);

            if (di < distancia && di > 5)
            {
                distancia = di;
                maisProximo = g;
            }
        }
        Debug.Log("Piu" + maisProximo.transform.position);
        return maisProximo;
    }
}
