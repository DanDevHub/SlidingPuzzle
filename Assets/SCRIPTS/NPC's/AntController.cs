using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints
    public float despawnDistance = 1f; // Distance from waypoint to despawn the agent
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0; // To track the current waypoint

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            // Set the first waypoint as the destination
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (waypoints.Length > 0)
        {
            // If agent reaches current waypoint, move to the next waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= despawnDistance)
            {
                // If there are more waypoints, go to the next one
                currentWaypointIndex++;
                if (currentWaypointIndex < waypoints.Length)
                {
                    agent.SetDestination(waypoints[currentWaypointIndex].position);
                }
                else
                {
                    // If all waypoints have been visited, despawn the agent
                    Destroy(gameObject);
                }
            }
        }
    }

    // Set the waypoints dynamically from the Spawner script
    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        if (agent != null && waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position); // Update destination
        }
    }
}