using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NavPointManager : MonoBehaviour
{
    public static NavPointManager globalNavPointManager { get; private set; }
    public GameObject[] points;

    float time = 0;
    const int threshold = 5; // seconds till update of nav mesh
    const int maxConnectionDistance = 19; // max distance for a node to connect to another node
    //float connectionRadius = 3.5f;

    void Awake()
    {
        if (globalNavPointManager != null && globalNavPointManager != this)
        {
            Destroy(gameObject); // deletes if there already exists one, just in case 
            return;
        }
        globalNavPointManager = this;

        SetNeighborsQuickly();
        

    }

    void Start()
    {
        UpdateNavPointWeights();
    }
    // update loop for the nav point generator
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;

        time += Time.deltaTime;
        if (time >= threshold)
        {
            //Debug.Log("updating nav points");
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
        Vector3 bestNextPoint = GetClosestNavNode(currentPosition).transform.position;
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
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
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
                    neighbor.distanceToPlayer = (int)(neighbor.transform.position - GameManager.Instance.player.transform.position).magnitude;
                    queue.Enqueue(neighbor);
                }
            }
        }

       /* foreach (GameObject node in points)
        {
            NavPointNode navNode = node.GetComponent<NavPointNode>();
            //Debug.Log(navNode);
            //Debug.Log(navNode.distanceToPlayer);
        }*/
    }

    // Set Neighbors automatically sets up neighboring nodes, wiht consideration of sight and not distance
    // O(n^2) currently, annoyingly, but only is called once at startup
    public void SetNeighborsQuickly()
    {
        foreach (GameObject node in points)
        {
            //Debug.Log(node);
            NavPointNode navNode = node.GetComponent<NavPointNode>();

            foreach (GameObject potentialObject in points)
            {
                NavPointNode potentialNavNode = potentialObject.GetComponent<NavPointNode>();
                //Debug.Log(potentialNavNode);
                if (potentialNavNode == null || potentialNavNode == navNode) continue;

                if (HasClearPath(navNode.transform.position, potentialNavNode.transform.position))
                {
                    if (!navNode.neighbors.Contains(potentialNavNode))
                    {
                        //Debug.Log("Added node");
                        //Debug.Log(potentialNavNode);
                        navNode.neighbors.Add(potentialNavNode);
                    }
                }
            }
            

        }

    }

    // ClearPath finder, finds out if line of sight exists between points
    private bool HasClearPath(Vector3 givenPosition, Vector3 targetPosition)
    {
        RaycastHit2D hit = Physics2D.Linecast(givenPosition, targetPosition, LayerMask.GetMask("Non-AI Default"));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("World"))
            {
                return false; // unclear sight
            }
        }
        if ((givenPosition - targetPosition).magnitude >= maxConnectionDistance) return false;
        return true; // clear sight
    }
    
    /*private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;

        Gizmos.color = Color.cyan;

        foreach (GameObject node in points)
        {
            NavPointNode navNode = node.GetComponent<NavPointNode>();
            Gizmos.DrawWireSphere(node.transform.position, connectionRadius);

            foreach (NavPointNode neighbor in navNode.neighbors)
            {
                
                if (neighbor != null)
                {
                    
                    Gizmos.DrawLine(node.transform.position, neighbor.transform.position);
                }
            }
        }
    }*/
    
}