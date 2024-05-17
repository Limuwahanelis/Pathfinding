using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AStarPathfinding : MonoBehaviour
{
    public UnityEvent<List<Vector2Int>> OnPathCreated;
    public Vector2Int startTilePos;
    public Vector2Int endTilePos;

    public PathFindingNode startNode;
    public PathFindingNode endNode;

    public List<PathFindingNode> open;
    public List<PathFindingNode> close;
    public List<List<PathFindingNode>> all;
    public List<Vector2Int> obstaclesPos;

    public Vector2Int gridSize;
    [SerializeField] ObstaclePool _obstaclePool;
    [SerializeField] TileObjectPlacer _tileObjectPlacer;
    [SerializeField] TileMapSetter _tileMapSetter;

    private List<Vector2Int> _pathPoints;
    enum Direction
    {
        UP,RIGHT,DOWN,LEFT
    }
    public void SetObstacles()
    {
        this.obstaclesPos = new List<Vector2Int>( _obstaclePool.GetAllActiveObstaclePositions());
    }
    public void SetTargetTiles()
    {
        if (_tileObjectPlacer.IsGoalPlaced) endTilePos =new Vector2Int( ((int)_tileObjectPlacer.GoalTile.transform.position.x), ((int)_tileObjectPlacer.GoalTile.transform.position.z));
        if (_tileObjectPlacer.IsStartPlaced) startTilePos = new Vector2Int(((int)_tileObjectPlacer.StartTile.transform.position.x), ((int)_tileObjectPlacer.StartTile.transform.position.z));
    }
    public void SetGridSize()
    {
        gridSize =new Vector2Int( ((int)_tileMapSetter.GridSize.x), ((int)_tileMapSetter.GridSize.y));
    }
    public void StartLooking()
    {
        if (!_tileObjectPlacer.IsGoalPlaced) return;
        if (!_tileObjectPlacer.IsStartPlaced) return;
            open = new List<PathFindingNode>();
        close = new List<PathFindingNode>();
        startNode.gcost = 0;
        startNode.hcost = Mathf.RoundToInt(Vector2.Distance(startTilePos, endTilePos) * 10);
        //startTile.fcost = startTile.gcost + 
        open.Add(startNode);
        PathFindingNode current = null;
        while (open.Count>0)
        {
            current = FindNodeWithLowestFCost(open);
            open.Remove(current);
            close.Add(current);

            if (current == endNode) break;

            foreach (PathFindingNode node in current.neighbours)
            {
                if (node == null ||!node.traversable || close.Exists((x) => x == node) ) continue;
                bool isNodeInOpen = open.Exists((x) => x == node);
                if (!isNodeInOpen || current.gcost+Mathf.RoundToInt(Vector2.Distance(current.position, node.position) * 10)<node.gcost)
                {
                    node.VisitNode(current, endNode);
                    if (!isNodeInOpen) open.Add(node);
                }

            }
        }
        _pathPoints = new List<Vector2Int>();
        if (current == endNode)
        {
            while (current != null)
            {
                _pathPoints.Add(current.position);
                Debug.Log(current.position);
                current = current.previousNode;
            }
        }
        _pathPoints.Reverse();
        OnPathCreated?.Invoke(_pathPoints);
    }

    public void DivideMapIntoTiles()
    {
        if (!_tileObjectPlacer.IsGoalPlaced) return;
        if (!_tileObjectPlacer.IsStartPlaced) return;
        all = new List<List<PathFindingNode>>();
        for (int i = 0;i<gridSize.x;i++)
        {
            all.Add(new List<PathFindingNode>());
            for(int j = 0;j<gridSize.y;j++)
            {
                all[i].Add(new PathFindingNode(i, j));
                if (i == startTilePos.x && j == startTilePos.y) startNode = all[i][j];
                if( i == endTilePos.x &&j == endTilePos.y) endNode = all[i][j];
                
                if (obstaclesPos.Exists((vec) => vec.x == i && vec.y == j)) 
                {
                    Vector2Int obstaclepos = obstaclesPos.Find((vec) => vec.x == i && vec.y == j);
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
                if (i == 0) all[i][j].neighbours[((int)Direction.LEFT)] = null;
                else all[i][j].neighbours[((int)Direction.LEFT)] = all[i - 1][j];

                if(i== all.Count-1) all[i][j].neighbours[((int)Direction.RIGHT)] = null;
                else all[i][j].neighbours[((int)Direction.RIGHT)] = all[i+1][j];

                if(j==0) all[i][j].neighbours[((int)Direction.DOWN)] = null;
                else all[i][j].neighbours[((int)Direction.DOWN)] = all[i][j-1];

                if(j == all[i].Count - 1) all[i][j].neighbours[((int)Direction.UP)] = null;
                else all[i][j].neighbours[((int)Direction.UP)] = all[i][j+1];
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

    public List<Vector2Int> GetPathPoints()
    {
        return _pathPoints;
    }

}
