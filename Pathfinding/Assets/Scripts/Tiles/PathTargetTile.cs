using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathTargetTile : MapTile
{
    public UnityEvent OnTileRemoved;

    public override void RemoveTile()
    {
        base.RemoveTile();
        OnTileRemoved?.Invoke();
    }
}
