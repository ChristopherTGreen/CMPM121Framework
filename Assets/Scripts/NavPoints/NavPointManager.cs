using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

public class NavPointManager : MonoBehaviour
{
    public GameObject[] points;

    public GameObject GetClosestPoint(Vector3 point)
    {
        if (points == null || points.Length == 0) return null;
        if (points.Length == 1) return points[0];

        return points.Aggregate((a, b) => (a.transform.position - point).sqrMagnitude < (b.transform.position - point).sqrMagnitude ? a : b);
    }

    public void UpdateWaypointWeights()
    {
        // queue for all current nodes to check
        Queue<NavPointNode> queue = new Queue<NavPointNode>();

        // closest is used for player finding
        GameObject closestObject = GetClosestPoint(GameManager.Instance.player.transform.position);
        NavPointNode closestNode = closestObject.GetComponent<NavPointNode>();

        // for a reset incase of any detached nodes, but this probably is never actually used
        //List<NavPointNode> nodes = new List<NavPointNode>();
        foreach (GameObject node in points) 
        {
            NavPointNode navNode = node.GetComponent<NavPointNode>();
            navNode.DistanceToPlayer = 99999;
        }

        closestNode.DistanceToPlayer = 0;
        queue.Enqueue(closestNode);

        while (queue.Count > 0)
        {
            NavPointNode current = queue.Dequeue();

            foreach (NavPointNode neighbor in current.Neighbors)
            {
                if (neighbor.DistanceToPlayer == 99999)
                {
                    neighbor.DistanceToPlayer = current.DistanceToPlayer + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
}