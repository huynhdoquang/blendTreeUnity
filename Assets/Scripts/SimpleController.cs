using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    [SerializeField] private CharacterController3D agent3D;
    [SerializeField] private Movement2D character2D;

    private void Start()
    {
        agent3D.AgentVelocityCallbackAction = OnRecieveAgentVelocity;
    }
    void OnRecieveAgentVelocity(Vector3 velocity, Transform transform)
    {
        character2D.UpdateAnimation(velocity);
        character2D.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, character2D.transform.localPosition.z);
    }
}
