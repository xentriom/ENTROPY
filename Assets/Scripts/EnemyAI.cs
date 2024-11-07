using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public float speed = 4.0f;
    public float chaseDistance = 5.0f;
    public GameObject player;
    public Waypoint startingWaypoint; // Starting waypoint for teleportation
    public DoorHandler door;

    private Waypoint currentWaypoint;
    private Queue<Waypoint> path = new Queue<Waypoint>();
    private bool isChasingPlayer = false;

    void Start()
    {
        // Set the current waypoint to the starting waypoint at the beginning
        currentWaypoint = startingWaypoint;
        FindPlayerPath(); // Initialize path to the player
    }

    void Update()
    {
        // Only proceed if the door is open
        if (door.states != DoorHandler.States.Open) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if enemy should switch to direct pursuit
        if (distanceToPlayer <= chaseDistance)
        {
            isChasingPlayer = true;
        }
        else
        {
            isChasingPlayer = false;
        }

        if (isChasingPlayer)
        {
            // Move directly towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            // Follow the waypoints
            if (path.Count == 0) return;

            Waypoint targetWaypoint = path.Peek();
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetWaypoint.transform.position) < 0.1f)
            {
                path.Dequeue();
            }

            // Recalculate path if reached the end
            if (path.Count == 0) FindPlayerPath();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the enemy has collided with the player
        if (other.CompareTag("Player"))
        {
            // Teleport the player to their respawn location
            player.transform.position = player.GetComponent<PlayerZeroG>().respawnLoc.transform.position;

            // Teleport enemy back to the starting waypoint
            transform.position = startingWaypoint.transform.position;

            // Reset pathfinding to start from the initial waypoint
            currentWaypoint = startingWaypoint;
            FindPlayerPath();
        }
    }

    void FindPlayerPath()
    {
        Waypoint playerWaypoint = FindClosestWaypoint(player.transform.position);
        path = BFS(currentWaypoint, playerWaypoint);
    }

    Queue<Waypoint> BFS(Waypoint start, Waypoint goal)
    {
        Queue<Waypoint> queue = new Queue<Waypoint>();
        Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
        queue.Enqueue(start);
        cameFrom[start] = null;

        while (queue.Count > 0)
        {
            Waypoint current = queue.Dequeue();
            if (current == goal) break;

            foreach (Waypoint neighbor in current.neighbors)
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        Stack<Waypoint> reversePath = new Stack<Waypoint>();
        for (Waypoint at = goal; at != null; at = cameFrom[at])
        {
            reversePath.Push(at);
        }

        Queue<Waypoint> path = new Queue<Waypoint>();
        while (reversePath.Count > 0)
        {
            path.Enqueue(reversePath.Pop());
        }
        return path;
    }

    Waypoint FindClosestWaypoint(Vector3 position)
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        Waypoint closest = null;
        float minDist = Mathf.Infinity;

        foreach (Waypoint waypoint in waypoints)
        {
            float dist = Vector3.Distance(position, waypoint.transform.position);
            if (dist < minDist)
            {
                closest = waypoint;
                minDist = dist;
            }
        }
        return closest;
    }
}