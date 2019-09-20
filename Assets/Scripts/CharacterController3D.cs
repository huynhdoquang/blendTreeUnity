using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController3D : MonoBehaviour
{

    private NavMeshAgent agent;
    public Camera cameraAgent;

    public System.Action<Vector3, Transform> AgentVelocityCallbackAction;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            agent.enabled = false;
            Move();
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            agent.enabled = true;
            Debug.Log("mouse down");
            Ray ray = cameraAgent.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, Camera.main.transform.forward * 50000000, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit.collider: " + hit.collider.tag);
                if (hit.collider.tag.Equals("Ground"))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }

        if (AgentVelocityCallbackAction != null)
        {
            Vector3 heading = new Vector3(agent.velocity.x, agent.velocity.z, agent.velocity.x);
            AgentVelocityCallbackAction.Invoke(heading.normalized, agent.transform);
        }
        return;
#if (UNITY_EDITOR)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraAgent.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Equals("Ground"))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
#else
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag.Equals("Ground"))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
#endif
    }

    [SerializeField] private float moveSpeed = 4f;
    private void Move()
    {
        Vector3 rightMovement = Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        Vector3 upMovement = Vector3.up * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.localPosition += rightMovement;
        transform.localPosition += upMovement;

        if (AgentVelocityCallbackAction != null)
            AgentVelocityCallbackAction.Invoke(heading, agent.transform);
    }
}

