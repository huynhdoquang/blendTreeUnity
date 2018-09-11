using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController2D : MonoBehaviour
{

    private NavMeshAgent agent;
    public Camera cameraAgent;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
}

