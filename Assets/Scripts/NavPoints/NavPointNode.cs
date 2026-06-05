using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NavPointNode : MonoBehaviour
{
    public List<NavPointNode> neighbors = new();
    public int distanceToPlayer = 99999; // defaults to far away

}