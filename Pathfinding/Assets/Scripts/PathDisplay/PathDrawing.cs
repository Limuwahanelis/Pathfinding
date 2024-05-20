using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathDrawing : MonoBehaviour
{
    
    [SerializeField] LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
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
    public void HidePath()
    {
        _lineRenderer.positionCount = 0;
    }
}
