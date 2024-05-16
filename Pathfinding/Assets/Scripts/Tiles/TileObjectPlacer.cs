using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectPlacer : MonoBehaviour
{
    [SerializeField] TileSelection _tileSelector;
    [SerializeField] TileType _startTileType;
    [SerializeField] TileType _goalTileType;
    [SerializeField] GameObject _goalTile;
    [SerializeField] GameObject _startTile;
    [SerializeField] ObstaclePool _obstaclePool;
    private TileType _tileToPlace;
    // Start is called before the first frame update
    void Start()
    {
        _tileToPlace = _goalTileType;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(_tileSelector.IsHittingMap) PlaceTile();

        }
    }

    public void SetTileTypeToPlace(TileType type)
    {
        _tileToPlace = type;
    }
    public void PlaceTile()
    {
        Debug.Log(_tileSelector.SelectedTile);

        if(_tileSelector.SelectedTile !=null) _tileSelector.SelectedTile.RemoveTile();
        if (_tileToPlace == _goalTileType)
        {
            _goalTile.transform.position = _tileSelector.SelectedTilePos;
            _goalTile.SetActive(true);
        }
        else if (_tileToPlace == _startTileType) 
        {
            _startTile.transform.position = _tileSelector.SelectedTilePos;
            _startTile.SetActive(true);
        }
        else
        {
            _obstaclePool.SetCurrentObstacleType((ObstacleTileType)_tileToPlace);
            _obstaclePool.GetObstacle().transform.position = _tileSelector.SelectedTilePos;
        }
    }
}

