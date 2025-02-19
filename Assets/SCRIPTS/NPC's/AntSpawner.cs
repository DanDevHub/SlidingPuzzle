using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject agentPrefab;  // Prefab of the agent
    public Transform spawnPoint;    // Where agents will spawn
    public Transform[] waypoints;   // Array of waypoints for agents to follow
    public float minSpawnInterval = 0.2f;  // Minimum time between spawns
    public float maxSpawnInterval = 1.5f;  // Maximum time between spawns

    private void Start()
    {
        ScheduleNextSpawn();
    }

    void SpawnAgent()
    {
        if (!gameObject.activeInHierarchy) return;  // Stop if the GameObject is inactive

        if (agentPrefab != null && waypoints.Length > 0)
        {
            // Calculate direction to the first waypoint
            Vector3 directionToWaypoint = waypoints[0].position - spawnPoint.position;
            directionToWaypoint.y = 0; // Ignore vertical component

            // Create rotation facing the first waypoint
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

            // Instantiate the agent with the correct rotation
            GameObject newAgent = Instantiate(agentPrefab, spawnPoint.position, targetRotation);

            // Set the waypoints for the agent
            AgentController agentController = newAgent.GetComponent<AgentController>();
            if (agentController != null)
            {
                agentController.SetWaypoints(waypoints); // Assign all waypoints to the agent
            }
        }

        // Schedule the next spawn
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke(nameof(SpawnAgent), randomInterval);
    }
}