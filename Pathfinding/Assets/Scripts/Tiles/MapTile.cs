using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MapTile : MonoBehaviour
{
    public TileType TileType=>_tileType;
    [SerializeField] TileType _tileType;

    public virtual void RemoveTile()
    {
        gameObject.SetActive(false);
    }
}
