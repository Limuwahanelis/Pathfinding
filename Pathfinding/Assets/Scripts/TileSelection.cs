using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    private Vector3 _lastPos;
    public GameObject tilePrefab;
    public LayerMask mask;
    //public PositionHolder2D posForTower;
    private int _enemyTilesLayer;
    public string enemyTileLayer;
    public Ray r;
    [SerializeField] bool ddH;
    [SerializeField] bool ddV;
    // Start is called before the first frame update
    void Start()
    {

        _enemyTilesLayer = LayerMask.NameToLayer(enemyTileLayer);
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        r = ray;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (hit.collider.gameObject.layer == _enemyTilesLayer) return;

            //Debug.Log(hit.collider.gameObject.layer);

            float posX = Mathf.Round(hit.point.x);
            float posZ = Mathf.Round(hit.point.z);
            if(ddH) posX = MathF.Floor(hit.point.x) + 0.5f;
            if(ddV) posZ = MathF.Floor(hit.point.z) + 0.5f;
            if (_lastPos.x != posX || _lastPos.z != posZ)
            {
                _lastPos.x = posX;
                _lastPos.z = posZ;
               // posForTower.posX = posX;
               // posForTower.posZ = posZ;
                tilePrefab.transform.position = new Vector3(posX, 0.001f, posZ);
                //_towers[_selectedTower].transform.position = new Vector3(posX, _towers[_selectedTower].transform.position.y, posZ);
            }

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(r.origin,r.direction*20f);
    }
}
