using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] Animator anim;

    private PlayerInputActionMap _inputActions;
    private PlayerInputActionMap _InputActions
    {
        get
        {
            if (_inputActions != null) return _inputActions;
            return _inputActions = new PlayerInputActionMap();
        }
    }

    private bool hasTargetPosition = false;
    private bool haunting = false;

    private Vector3 targetPosition;

    [SerializeField] private Camera cam;
    Mouse mouse;

    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;


        hasTargetPosition = anim.GetBool("HasTargetPosition");
        haunting = anim.GetBool("Haunting");

        _inputActions.Main.Move.performed += ctx => MoveActionPerformed();
    }

    // Update is called once per frame
    void Update()
    {
        /*float distToEndLoc = Vector3.Distance(transform.position, agent.destination);


        if (agent.destination == null)

        if (distToEndLoc < 1)
        {
            anim.SetBool("HasTargetPosition", false);
            hasTargetPosition = false;
            return;
        }



        distToPlayer = Vector3.Distance(stateMachine.NavigationAgent.GameObject().transform.position, stateMachine.FollowTarget.transform.position);

        if (distToPlayer < 3)
            targetLocation = stateMachine.NavigationAgent.GameObject().transform.position;*/
    }

    private void OnEnable() => _InputActions.Enable();


    private void OnDisable() => _InputActions.Disable();

    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> MoveActionPerformed()
    {
        Vector3 mousePosition = mouse.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(mousePosition);
        Debug.Log(ray);
        if (Physics.Raycast(ray, out RaycastHit hit, 9999999, LayerMask.GetMask("Walkable")))
        {
            if (agent.SetDestination(hit.point))
            {
                Debug.Log("destination set");
                hasTargetPosition = true;
            }
            Debug.Log("destination not set");

            
        }

        return null;
    }
}
