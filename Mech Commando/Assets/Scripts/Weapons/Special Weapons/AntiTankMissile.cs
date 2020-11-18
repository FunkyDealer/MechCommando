using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTankMissile : SpecialWeapon
{
    Camera camera;
    [SerializeField]
    GameObject projectile;

    GameObject closest;
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



    public List<GameObject> EnemiesTemp, Enemies;
    RaycastHit ray;

    public override void Start()
    {
        closest = null;
        base.Start();
        //camera = GetComponentInParent<Camera>();
        //nos = camera.transform;
        EnemiesTemp = new List<GameObject>();
        Enemies = new List<GameObject>();
    }

    public override void Update()
    {
        base.Update();

    }


    public override void Shoot()
    {
        base.Shoot();
        InimigosAim();
        if (Enemies.Count > 0)
        {
            GameObject a = Instantiate(projectile, this.transform.position, Quaternion.identity);
            GuidedMissile tankMissile = a.GetComponent<GuidedMissile>();
            tankMissile.go = GetClosest();
            tankMissile.damage = baseDamage;
            tankMissile.direction = transform.forward;
        }
        else
        {
            Debug.Log("No Enemies Found");
        }       
      
    }

    void InimigosAimOne()
    {

        if (Physics.Raycast(transform.position, transform.forward, out ray, visao))
        {
            Debug.DrawLine(transform.position, transform.forward, Color.red);
            if (ray.collider.gameObject.CompareTag(tag))
            {

                if (!EnemiesTemp.Contains(ray.collider.gameObject))
                {
                    EnemiesTemp.Add(ray.collider.gameObject);
                }
                if (!Enemies.Contains(ray.collider.gameObject))
                {
                    Enemies.Add(ray.collider.gameObject);
                }
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.forward, Color.green);
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            if (!EnemiesTemp.Contains(Enemies[i]))
            {
                Enemies.Remove(Enemies[i]);
            }
        }
        EnemiesTemp.Clear();

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
                Vector3 directionPraDivisaoRay = (-transform.right) + (transform.up * i * distCm);
                Vector3 dir = Quaternion.AngleAxis(angDoRay, transform.up) * directionPraDivisaoRay * 100;



                if (Physics.Raycast(transform.position, dir, out ray, visao))
                {
                    if (ray.collider.gameObject.tag == tag)
                    {
                        Debug.DrawLine(transform.position, dir, Color.red);
                        if (!EnemiesTemp.Contains(ray.collider.gameObject))
                        {
                            EnemiesTemp.Add(ray.collider.gameObject);
                        }
                        if (!Enemies.Contains(ray.collider.gameObject))
                        {
                            Enemies.Add(ray.collider.gameObject);
                        }
                    }
                }
                else
                {

                    Debug.DrawLine(transform.position, dir, Color.green);
                }

            }
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            if (!EnemiesTemp.Contains(Enemies[i]))
            {
                Enemies.Remove(Enemies[i]);
            }
        }


       
        EnemiesTemp.Clear();
    }

    public GameObject GetClosest()
    {
        GameObject maisProximo = null ;
        float distancia = 10000000;

        foreach (GameObject g in Enemies)
        {
            float di = Vector3.Distance(transform.position, g.transform.position);

            if (di < distancia)
            {
                distancia = di;
                maisProximo = g;
            }
        }
       // Debug.Log("Piu" + maisProximo.transform.position);

        return maisProximo;
    }
}
