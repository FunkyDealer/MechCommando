using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTankMissile : SpecialWeapon
{
    AutoAim mira;
    Camera camera;
    [SerializeField]
    GameObject projectile;

    GameObject closest;
    public Transform nos;
    //Distancia do Inimigo
    public float visao = 1000;
    //tag do inimigo 
    public string tag = "Enemy";
    //Raios por camada em X 
    public float raiosPorCamada = 100;
    //Angulo de visao 
    public float angVisao = 120;
    //numero de camadas em Y  
    public int numCamadas = 1;
    //Distancia entre cada camada de Y  
    public float distCm = 0.1f;



    public List<GameObject> inimTemp, Inimigos;
    RaycastHit ray;

    public override void Start()
    {
        closest = null;
        base.Start();
        //camera = GetComponentInParent<Camera>();
        //nos = camera.transform;
        inimTemp = new List<GameObject>();
        Inimigos = new List<GameObject>();
    }

    public override void Update()
    {
        base.Update();

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    InimigosAim();
        //    GetCloser();
        //}
    }


    public override void Disparo()
    {
        base.Disparo();
        InimigosAim();
        GameObject a = Instantiate(projectile, this.transform.position, Quaternion.identity);
        GuidedMissile tankMissile = a.GetComponent<GuidedMissile>();
        tankMissile.go = GetCloser();
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
                Vector3 directionPraDivisaoRay = (-nos.right) + (nos.up * i * distCm);
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
        GameObject maisProximo = null ;
        float distancia = 10000000;

        foreach (GameObject g in Inimigos)
        {
            float di = Vector3.Distance(nos.position, g.transform.position);

            if (di < distancia)
            {
                distancia = di;
                maisProximo = g;
            }
        }
        Debug.Log("Piu" + maisProximo.transform.position);

        return maisProximo;
    }
}
