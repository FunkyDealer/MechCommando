using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    bool billBoard;

    void Awake()
    {
      if (billBoard) mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        BillBoard();
    }

    // Update is called once per frame
    void Update()
    {
        BillBoard();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    void BillBoard()
    {
        if (billBoard) transform.forward = mainCamera.transform.forward;
    }
}
