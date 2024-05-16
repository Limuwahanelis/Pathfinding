using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFindingNode
{
    public PathFindingNode(int x,int y)
    {
        position = new Vector2Int(x,y);
    }

    public Vector2 position;
    public List<PathFindingNode> neighbours = new List<PathFindingNode>() {null,null,null,null };
    public bool traversable = true;

    public int gcost;
    public int hcost;
    public int fcost;

    public void CalculateCosts(PathFindingNode start, PathFindingNode end)
    {
        //gcost 
    }
}
