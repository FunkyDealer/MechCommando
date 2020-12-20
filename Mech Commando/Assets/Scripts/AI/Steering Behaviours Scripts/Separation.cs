using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = ("Behaviour/Separation"))]
public class Separation : SteeringBehaviour
{
    private List<MovementInfo> targetList;
    [SerializeField]
    float threshold = 7f;
    [SerializeField]
    float maxAccell = 1.5f;
    [SerializeField]
    float k = 3f;

    private void OnEnable()
    {
        EnemyManager e = GameObject.FindObjectOfType<EnemyManager>();

        targetList = e.Enemies.Select(x => x.GetInfo).ToList();
    }


    public override Steering GetSteering(MovementInfo npc, MovementInfo ignored)
    {
        Steering steering = new Steering();
        foreach (MovementInfo t in targetList)
        {
            Vector3 direction = npc.position - t.position;
            float distanceSqr = direction.sqrMagnitude;

            if (distanceSqr < threshold * threshold)
            {
                float strenght = Mathf.Min(maxAccell, k / (distanceSqr));
                steering.linear += direction.normalized * strenght;
            }
        }

        return steering;
    }
}
