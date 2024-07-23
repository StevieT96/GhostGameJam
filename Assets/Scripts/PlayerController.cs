using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] Animator anim;

    [SerializeField] LayerMask layerMask;

    [SerializeField] private float stopDist = 0.1f;
    [SerializeField] private float scareStopDist = 1;

    private PlayerInputActionMap _inputActions;
    private PlayerInputActionMap _InputActions
    {
        get
        {
            if (_inputActions != null) return _inputActions;
            return _inputActions = new PlayerInputActionMap();
        }
    }

    private bool movingToHaunt = false;
    private GameObject objectToHaunt;

    [SerializeField] private Camera cam;
    Mouse mouse;

    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;


        /*hasTargetPosition = anim.GetBool("HasTargetPosition");
        haunting = anim.GetBool("Haunting");*/

        _inputActions.Main.Move.performed += ctx => MoveActionPerformed();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.destination != null)
        {
            float distToEndLoc = Vector3.Distance(gameObject.transform.position, agent.destination);

            if (!movingToHaunt)
            {
                if (distToEndLoc < stopDist)
                {
                    anim.SetBool("HasTargetPosition", false);

                    NavMeshHit myNavHit;
                    NavMesh.SamplePosition(gameObject.transform.position, out myNavHit, 100, -1);

                    agent.SetDestination(myNavHit.position);

                    return;
                }
            }
            else if (objectToHaunt != null)
            {
                if (distToEndLoc < scareStopDist)
                {
                    anim.SetBool("HasTargetPosition", false);

                    NavMeshHit myNavHit;
                    NavMesh.SamplePosition(gameObject.transform.position, out myNavHit, 100, -1);

                    agent.SetDestination(myNavHit.position);

                    anim.SetBool("Haunting", true);

                    movingToHaunt = false;

                    objectToHaunt.GetComponent<ScareObject>().haunt();

                    return;
                }
            }
        }
    }

    private void OnEnable() => _InputActions.Enable();


    private void OnDisable() => _InputActions.Disable();

    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> MoveActionPerformed()
    {
        Vector3 mousePosition = mouse.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 9999999, layerMask))
        {
            anim.SetBool("Haunting", false);

            switch (hit.collider.gameObject.layer)
            {
                case 3: // ScareObject layer

                    Debug.Log("clicked on a scareObject");

                    if (agent.SetDestination(hit.point))
                    {
                        anim.SetBool("HasTargetPosition", true);
                        movingToHaunt = true;
                        objectToHaunt = hit.transform.gameObject;
                    }

                    break;

                case 7: // Walkable layer

                    Debug.Log("clicked on Walkable");

                    if (agent.SetDestination(hit.point))
                    {
                        movingToHaunt = false;
                        anim.SetBool("HasTargetPosition", true);
                    }

                    break;
            }

        }

        return null;
    }
}
