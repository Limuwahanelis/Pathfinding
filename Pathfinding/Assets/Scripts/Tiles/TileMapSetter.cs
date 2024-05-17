using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TileMapSetter : MonoBehaviour
{
    public Vector2 GridSize=>_gridSize;
    [SerializeField] Material _mat;
    [SerializeField] GameObject _mapQuad;
    [SerializeField] Vector2 _gridSize;
    Vector2 _oldGridSize=new Vector2(3,3);
    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdatGridVertically(float newSize)
    {

        _gridSize.x = (int)newSize;
        if (_gridSize == _oldGridSize) return;
        _mat.SetVector("_Vector2", _gridSize);
        Vector3 halfSizeIncrease = new Vector3(-math.abs(_mapQuad.transform.localScale.x - _gridSize.x) / 2, 0, math.abs(_mapQuad.transform.localScale.y - _gridSize.y) / 2);
        halfSizeIncrease = new Vector3((_mapQuad.transform.localScale.x - _gridSize.x > 0 ? 1 : -1) * halfSizeIncrease.x, 0, (_mapQuad.transform.localScale.y - _gridSize.y > 0 ? -1 : 1) * halfSizeIncrease.z);
        _mapQuad.transform.localScale = _gridSize;
        _mapQuad.transform.position += halfSizeIncrease;
        _oldGridSize = _gridSize;
    }

    public void UpdatGridHorizontally(float newSize)
    {
        _gridSize.y = (int)newSize;
        if (_gridSize == _oldGridSize) return;
        _mat.SetVector("_Vector2", _gridSize);
        Vector3 halfSizeIncrease = new Vector3(-math.abs(_mapQuad.transform.localScale.x - _gridSize.x) / 2, 0, math.abs(_mapQuad.transform.localScale.y - _gridSize.y) / 2);
        halfSizeIncrease = new Vector3((_mapQuad.transform.localScale.x - _gridSize.x > 0 ? -1 : 1) * halfSizeIncrease.x, 0, (_mapQuad.transform.localScale.y - _gridSize.y > 0 ? -1 : 1) * halfSizeIncrease.z);
        _mapQuad.transform.localScale = _gridSize;
        _mapQuad.transform.position += halfSizeIncrease;
        _oldGridSize = _gridSize;
    }
}
