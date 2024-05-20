using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    public Vector3 SelectedTilePos => _selectedTilePos;
    public bool IsHittingMap => _isHittingMap;
    public MapTile SelectedTile => _selectedObject;
    [SerializeField] GameObject _tilePrefab;
    [SerializeField] LayerMask _mask;
    [SerializeField] TileMapSetter _map;
    private MapTile _selectedObject;
    private Vector3 _selectedTilePos;
    private Vector3 _lastPos;
    private bool _isHittingMap;
    private Vector2 _mousePos;
    private Ray r;
    private bool _canHitMap=true;
    // Start is called before the first frame update
    void Start()
    {
        _lastPos.x = 0;
        _lastPos.z = 0;
        _tilePrefab.transform.position = new Vector3(0, 0.001f, 0);
        _selectedTilePos.x = 0;
        _selectedTilePos.y = 0.001f;
        _selectedTilePos.z = 0;
        _tilePrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canHitMap) return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        r = ray;
        if (_isHittingMap=Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
        {

            float posX = Mathf.Round(hit.point.x);
            float posZ = Mathf.Round(hit.point.z);
            if (_lastPos.x != posX || _lastPos.z != posZ)
            {
                _lastPos.x = posX;
                _lastPos.z = posZ;
                _tilePrefab.transform.position = new Vector3(posX, 0.001f, posZ);
                _selectedTilePos.x = posX;
                _selectedTilePos.y = 0.001f;
                _selectedTilePos.z= posZ;
                
            }
            _selectedObject = hit.transform.gameObject.GetComponent<MapTile>();
        }

    }
    public void CheckIfSelectionIsInGrid()
    {
        if (_tilePrefab.transform.position.x > _map.GridSize.x - 1 || _tilePrefab.transform.position.z > _map.GridSize.y - 1) _tilePrefab.transform.position = new Vector3(0, 0.001f, 0);
    }
    public void SetMousePos(Vector2 pos)
    {
        _mousePos = pos;

    }
    public void SetCanHitMap(bool value)
    {
        _canHitMap = value;
        if (!_canHitMap) _isHittingMap = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(r.origin,r.direction*20f);
    }
}
