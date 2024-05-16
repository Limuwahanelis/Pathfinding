using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public Vector2Int startTilePos;
    public Vector2Int endTilePos;

    public PathFindingNode startTile;
    public PathFindingNode endTile;

    public List<PathFindingNode> open;
    public List<PathFindingNode> close;
    public List<List<PathFindingNode>> all;
    public List<Vector2Int> obstaclesPos;

    public Vector2Int gridSize;
    public Vector2 gridStartPos = new Vector2(0, -1);
    [SerializeField] ObstaclePool _obstaclePool;
    enum Direction
    {
        UP,RIGHT,DOWN,LEFT
    }
    public void SetObstacles()
    {
        
        this.obstaclesPos = new List<Vector2Int>( _obstaclePool.GetAllActiveObstaclePositions());
    }
    public void StartLooking()
    {
        open = new List<PathFindingNode>();
        close = new List<PathFindingNode>();
        open.Add(startTile);
        PathFindingNode current = null;
        while (open.Count==0)
        {
            current = FindNodeWithLowestFCost(open);
            open.Remove(current);
            close.Add(current);

            if (current == endTile) break;

            foreach (PathFindingNode node in current.neighbours)
            {

            }
        }
    }

    public void DivideMapIntoTiles()
    {
        all = new List<List<PathFindingNode>>();
        for (int i = 0;i<gridSize.x;i++)
        {
            all.Add(new List<PathFindingNode>());
            for(int j = 0;j<gridSize.y;j++)
            {
                all[i].Add(new PathFindingNode(i, j));
                if (i == startTilePos.x && j == startTilePos.y) startTile = all[i][j];
                if( i == endTilePos.x &&j == endTilePos.y) endTile = all[i][j];
                Vector2Int obstaclepos = obstaclesPos.Find((vec) => vec.x == i && vec.y == j);
                if(obstaclepos != null ) 
                {
                    all[i][j].traversable = false;
                    obstaclesPos.Remove(obstaclepos);
                }
            }
        }
        AssignNeighbours();
    }

    private void AssignNeighbours()
    {
        for (int i = 0; i < all.Count; i++)
        {
            for(int j = 0; j < all[i].Count; j++)
            {
                if(i==0) all[i][j].neighbours[((int)Direction.DOWN)] = null;
                else all[i][j].neighbours[((int)Direction.DOWN)] = all[i][j-1];

                if(i== all.Count-1) all[i][j].neighbours[((int)Direction.UP)] = null;
                else all[i][j].neighbours[((int)Direction.UP)] = all[i][j+1];

                if(j==0) all[i][j].neighbours[((int)Direction.LEFT)] = null;
                else all[i][j].neighbours[((int)Direction.LEFT)] = all[i-1][j];

                if(j == all.Count - 1) all[i][j].neighbours[((int)Direction.RIGHT)] = null;
                else all[i][j].neighbours[((int)Direction.RIGHT)] = all[i + 1][j];
            }
        }
    }

    private PathFindingNode FindNodeWithLowestFCost(List<PathFindingNode> nodes)
    {
        PathFindingNode toReturn = nodes[0];
        int lowestFcost = nodes[0].fcost;

        for(int i=0;i<nodes.Count;i++) 
        {
            if (nodes[i].fcost < lowestFcost)
            {
                toReturn = nodes[i];
                lowestFcost = nodes[i].fcost;
            }
        }
        return toReturn;
    }

}
