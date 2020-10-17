using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public List<GameObject> eni;
    [SerializeField]
    double maxTime = 0.2d;
    [SerializeField]
    Image image;
    [SerializeField]
    float sizeS, sizeB;
    [SerializeField]
    SpriteRenderer sprite;
    double time;
    private void Start()
    {
        eni = new List<GameObject>();
        sprite.color = Color.red;
        sizeB = this.gameObject.transform.localScale.x;
    }
    void Update()
    {



        time += Time.deltaTime;
        if (time >= maxTime)
        {
            foreach (GameObject g in eni)
            {
                Debug.Log(g.transform.position);
                // Vector3 v = new Vector3(g.transform.position.x * sizeS / sizeB, g.transform.position.y * sizeS / sizeB, g.transform.position.z * sizeS / sizeB);
                // Instantiate(sprite, v, Quaternion.identity);
            }
            time = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            eni.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            foreach (GameObject gameObject in eni)
            {
                if (gameObject == other.gameObject)
                {
                    eni.Remove(gameObject);
                    break;
                }
            }
        }
    }
}
