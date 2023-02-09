using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
// When the player clicks on a part of the world,
// the MavMeshAgent moves to that position
[RequireComponent(typeof(NavMeshAgent))]

public class MoveToPoint : MonoBehaviour
{
    // The agent we'll be moving around
    private NavMeshAgent m_Agent;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // When the usser clicks move the agent.
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get the position onscreen, in screen coordinates (i.e., pixels).
            var mousePosition = Input.mousePosition;

            // Convert this position into a ray that starts at the
            // camera and moves toward where the mouse cursor is.
            var ray = Camera.main.ScreenPointToRay(mousePosition);

            // Store information about any raycast hit in this variable.
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Tell the agent to move towards the position where the ray hits an object.
                m_Agent.destination = hit.point;
                transform.LookAt(hit.point);
            }
        }
    }
}
