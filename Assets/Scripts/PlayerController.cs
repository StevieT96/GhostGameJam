using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;

    private bool hasTargetPosition = false;
    private bool haunting = false;

    private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        hasTargetPosition = anim.GetBool("HasTargetPosition");
        haunting = anim.GetBool("Haunting");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
