using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] AStarPathfinding _AStarPathfinding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawLine()
    {
        List<Vector3> positions = new List<Vector3>();
        List<Vector2Int> nodes = _AStarPathfinding.GetPathPoints();
        _lineRenderer.positionCount=nodes.Count;
        for (int i = 0 ;i< nodes.Count;i++)
        {
            
            _lineRenderer.SetPosition(i, new Vector3(nodes[i].x, 0.05f, nodes[i].y));
        }
    }
}
