using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] Animator anim;

    private bool hasTargetPosition = false;
    private int scareMeter = 0;

    // Start is called before the first frame update
    void Start()
    {

        hasTargetPosition = anim.GetBool("HasTargetPosition");
        scareMeter = anim.GetInteger("ScareMeter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
