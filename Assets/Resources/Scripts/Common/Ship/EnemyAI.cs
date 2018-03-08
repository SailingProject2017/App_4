using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : BaseObject
{
    [SerializeField]
    private Transform[] wayPoints; // マーカーの当たる箇所を格納

    [SerializeField]
    private int currentRoot; 

    public override void OnUpdate()
    {
        Vector3 pos = wayPoints[currentRoot].position;

        if (Vector3.Distance(transform.position, pos) < 0.5f)
        {
            currentRoot = (currentRoot < wayPoints.Length - 1) ? currentRoot + 1 : 0;
        }

        GetComponent<NavMeshAgent>().SetDestination(pos);
    }
}