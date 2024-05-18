using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PathDisplayController : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] PathDrawing _pathDrawer;
    [SerializeField] TileMapSetter _map;
    [SerializeField] AStarPathfinding _pathFinding;
    [SerializeField] RunningCharacter _character;
    [SerializeField] TileObjectPlacer _objectPlacer;

    // Start is called before the first frame update
    void Start()
    {
        _pathFinding.OnPathCreated.AddListener(SetPath);
        _objectPlacer.OnTilePlaced.AddListener(CheckIfNewTileBlockPath);
    }
    public void CheckIfNewTileBlockPath(MapTile tile)
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        if (positions.Length <= 0) return;
        // Line is drawn on same height so we take height of first element to check for distance
        _lineRenderer.GetPositions(positions);
        Vector3 posToCheck = new Vector3(tile.transform.position.x, positions[0].y, tile.transform.position.z);
        foreach (Vector3 pos in positions)
        {
            if (Vector3.Distance(posToCheck, pos) < 0.01f)
            {
                HidePath();
            }
        }
    }
    public void CheckIfPathIsOnMap()
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        foreach (Vector3 pos in positions)
        {
            if (pos.x > _map.GridSize.x - 1 || pos.z > _map.GridSize.y - 1)
            {
                HidePath();
                break;
            }
        }
    }
    public void HidePath()
    {
        _pathDrawer.HidePath();
        _character.HideCharacter();
    }
    private void SetPath(List<Vector2Int> path)
    {
        if (path.Count == 0) return;
        _character.SetPath(path);
        _pathDrawer.DrawLine(path);
    }

    private void OnDestroy()
    {
        _pathFinding.OnPathCreated.RemoveListener(SetPath);
        _objectPlacer.OnTilePlaced.RemoveListener(CheckIfNewTileBlockPath);
    }
}
