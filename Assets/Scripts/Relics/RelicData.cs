// Note: File created by Jay for assignment 4 - Not provided by the professor
// Professor also recommends splitting the relic triggers and effects separated

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;



public class RelicTriggersData
{
    public string name { get; set; } = null;
    public int sprite { get; set; } = -1;
    public Trigger trigger { get; set; } = null;

}



public class RelicEventsData
{
    
    public string name { get; set; } = null;
    public int sprite { get; set; } = -1;
    public Effect effect { get; set; } = null;

}



// Type Trigger - used in the RelicTriggersData class
public class Trigger
{
    
    public string description { get; set; } = null;
    public string type { get; set; } = null;
    public string amount { get; set; } = null;

}



// Type Effect - used in the RelicEventsData class
public class Effect
{
    
    public string description { get; set; } = null;
    public string type { get; set; } = null;
    public string amount { get; set; } = null;
    public string until { get; set; } = null;

}