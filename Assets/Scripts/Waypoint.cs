// Waypoint.cs
using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> neighbors = new List<Waypoint>(); // Adjacent waypoints

    private void OnDrawGizmos() // Visualize connections in the editor
    {
        Gizmos.color = Color.green;
        foreach (Waypoint neighbor in neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}