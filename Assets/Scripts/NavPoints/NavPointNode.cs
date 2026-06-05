using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NavPointNode
{
    public List<NavPointNode> Neighbors = new();
    public int DistanceToPlayer = 99999; // defaults to far away

    public void WeightedDistanceAssignment()
    {

    }
}