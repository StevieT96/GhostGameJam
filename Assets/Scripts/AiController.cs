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


    [SerializeField] private Waypoint AiRunPoint;
    [SerializeField] private List<Waypoint> AiWayPoints;

    [SerializeField] private float distanceNeededToArriveAtWaypoint = 3;

    [SerializeField] private int LowerRandomTimeToWaitBeforeMoving = 2;
    [SerializeField] private int UpperRandomTimeToWaitBeforeMoving = 5;

    private bool arrivedAtWaypoint = false;

    private Waypoint currentWayPoint;

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
        distanceToWaypoint = Vector3.Distance(gameObject.transform.position, currentWayPoint.gameObject.transform.position);

        if (distanceToWaypoint < distanceNeededToArriveAtWaypoint && !arrivedAtWaypoint)
            ArrivedAtWaypoint();
    }

    private void MoveToNewWayPoint()
    {
        arrivedAtWaypoint = false;

        currentWayPoint = FindRandomUnoccupiedWaypoint();

        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(currentWayPoint.gameObject.transform.position, out myNavHit, 100, -1))
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

    private Waypoint FindRandomUnoccupiedWaypoint()
    {
        List<Waypoint> unoccupiedWaypoints = new List<Waypoint>();

        foreach (var item in AiWayPoints)
        {
            if (!item.occupied)
                unoccupiedWaypoints.Add(item);
        }


        bool foundWayPoint = false;

        if (unoccupiedWaypoints.Count > 0)
        {
            while (foundWayPoint)
            {
                int randomWayPoint = RandomNumberGenerator.GetInt32(0, unoccupiedWaypoints.Count);

                if (currentWayPoint != null)
                {
                    if (AiWayPoints[randomWayPoint] != currentWayPoint)
                    {
                        return unoccupiedWaypoints[randomWayPoint];
                    }
                }
                else
                {
                    return unoccupiedWaypoints[randomWayPoint];
                }
            }
        }
        

        return null;
    }
}
