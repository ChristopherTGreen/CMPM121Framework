using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NavPointNode : MonoBehaviour
{
    [SerializeField] public List<NavPointNode> neighbors = new();
    [SerializeField] public int distanceToPlayer = 99999; // defaults to far away

}