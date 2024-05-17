using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathDrawing : MonoBehaviour
{
    
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] AStarPathfinding _AStarPathfinding;
    [SerializeField] TileMapSetter _map;
    // Start is called before the first frame update
    void Start()
    {
        _AStarPathfinding.OnPathCreated.AddListener(DrawLine);
        _lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawLine(List<Vector2Int> nodes)
    {
        _lineRenderer.positionCount=nodes.Count;
        for (int i = 0 ;i< nodes.Count;i++)
        {
            
            _lineRenderer.SetPosition(i, new Vector3(nodes[i].x, 0.05f, nodes[i].y));
        }
    }
    public void CheckIfPathIsOnMap()
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        foreach (Vector3 pos in positions)
        {
            if(pos.x>_map.GridSize.x-1 || pos.z>_map.GridSize.y-1)
            {
                _lineRenderer.positionCount = 0; break;
            }
        }
    }
    private void OnDestroy()
    {
        _AStarPathfinding.OnPathCreated.RemoveListener(DrawLine);
    }
}
