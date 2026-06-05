using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

public class NavPointManager : MonoBehaviour
{
    public static NavPointManager globalNavPointManager { get; private set; }
    public GameObject[] points;

    float time = 0;
    const int threshold = 10; // seconds till update of nav mesh

    private void Awake()
    {
        if (globalNavPointManager != null && globalNavPointManager != this)
        {
            Destroy(gameObject); // deletes if there already exists one, just in case 
            return;
        }
        globalNavPointManager = this;
    }

    // update loop for the nav point generator
    void Update()
    {
        time += Time.deltaTime;
        if (time >= threshold)
        {
            Debug.Log("updating nav points");
            time = 0;
            UpdateNavPointWeights();
        }
    }



    // Finds the closest point (Nav point) to the given point
    public GameObject GetClosestNavPoint(Vector3 point)
    {
        if (points == null || points.Length == 0) return null;
        if (points.Length == 1) return points[0];

        return points.Aggregate((a, b) => (a.transform.position - point).sqrMagnitude < (b.transform.position - point).sqrMagnitude ? a : b);
    }

    // Uses the GetClosestPoint to find the closest node
    public NavPointNode GetClosestNavNode(Vector3 point) 
    {
        return GetClosestNavPoint(point).GetComponent<NavPointNode>();
    }

    // gets next nav point
    public Vector3 GetNextNavPoint(Vector3 currentPosition)
    {
        Vector3 bestNextPoint = new Vector3();
        int lowestDistance = 99999;
        // gets closest nav node, which is used to determine if a search or next node is necessary 
        NavPointNode closestNavNode = GetClosestNavNode(currentPosition);

        foreach (NavPointNode neighbor in closestNavNode.neighbors)
        {
            if (neighbor.distanceToPlayer < lowestDistance)
            {
                lowestDistance = neighbor.distanceToPlayer;
                bestNextPoint = neighbor.transform.position;
            }
        }

        return bestNextPoint;
    }

    // updates all nav points relative to the player 
    public void UpdateNavPointWeights()
    {
        // queue for all current nodes to check
        Queue<NavPointNode> queue = new Queue<NavPointNode>();

        // closest is used for player finding
        GameObject closestObject = GetClosestNavPoint(GameManager.Instance.player.transform.position);
        NavPointNode closestNode = closestObject.GetComponent<NavPointNode>();

        // for a reset incase of any detached nodes, but this probably is never actually used
        //List<NavPointNode> nodes = new List<NavPointNode>();
        foreach (GameObject node in points)
        {
            NavPointNode navNode = node.GetComponent<NavPointNode>();
            navNode.distanceToPlayer = 99999;
        }

        closestNode.distanceToPlayer = 0;
        queue.Enqueue(closestNode);

        while (queue.Count > 0)
        {
            NavPointNode current = queue.Dequeue();

            foreach (NavPointNode neighbor in current.neighbors)
            {
                if (neighbor.distanceToPlayer == 99999)
                {
                    neighbor.distanceToPlayer = current.distanceToPlayer + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
}