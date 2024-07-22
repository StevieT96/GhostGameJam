using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Security.Cryptography;
using Unity.Mathematics;


public class AiController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] Animator anim;

    private bool hasTargetPosition = false;
    private int scareMeter = 0;


    [SerializeField] private GameObject AiRunPoint;
    [SerializeField] private List<GameObject> AiWayPoints;

    [SerializeField] private float distanceNeededToArriveAtWaypoint = 3;

    [SerializeField] private int LowerRandomTimeToWaitBeforeMoving = 2;
    [SerializeField] private int UpperRandomTimeToWaitBeforeMoving = 5;

    private bool arrivedAtWaypoint = false;

    private GameObject currentWayPoint;

    float distanceToWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {

        MoveToNewWayPoint();

        hasTargetPosition = anim.GetBool("HasTargetPosition");
        scareMeter = anim.GetInteger("ScareMeter");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToWaypoint = Vector3.Distance(transform.position, currentWayPoint.transform.position);

        if (distanceToWaypoint < distanceNeededToArriveAtWaypoint && !arrivedAtWaypoint)
            ArrivedAtWaypoint();
    }

    private void MoveToNewWayPoint()
    {
        arrivedAtWaypoint = false;

        int randomWayPoint = RandomNumberGenerator.GetInt32(0, AiWayPoints.Count);

        currentWayPoint = AiWayPoints[randomWayPoint];

        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(currentWayPoint.transform.position, out myNavHit, 100, -1))
        {
            agent.SetDestination(myNavHit.position);

            hasTargetPosition = true;
            anim.SetBool("HasTargetPosition", true);

        }
    }

    private void ArrivedAtWaypoint()
    {
        arrivedAtWaypoint = true;

        int randomWaitTime = RandomNumberGenerator.GetInt32(LowerRandomTimeToWaitBeforeMoving, UpperRandomTimeToWaitBeforeMoving + 1);

        hasTargetPosition = false;
        anim.SetBool("HasTargetPosition", false);

        StartCoroutine(WaitSecondsMoveToNewWayPoint(randomWaitTime));

    }

    private IEnumerator WaitSecondsMoveToNewWayPoint(float _waitTime)
    {
        yield return new WaitForSecondsRealtime(_waitTime);

        MoveToNewWayPoint();
    }
}
