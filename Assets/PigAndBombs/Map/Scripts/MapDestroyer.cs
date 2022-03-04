using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapDestroyer : MonoBehaviour
{

    [SerializeField] private List<Tile> _undestractable;
    [SerializeField] private List<Tile> _destractable;


    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private GameObject _explotionPref;

    private ItemsSpawner _itemsSpawner;

    private int _explotionLength = 2;

    private void Awake()
    {
        _itemsSpawner = gameObject.GetComponent<ItemsSpawner>();
    }

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = _tileMap.WorldToCell(worldPos);

        ExplodeCell(originCell,new Vector3Int(0,0,0),0);
        ExplodeCell(originCell,new Vector3Int(1, 0, 0),1);
        ExplodeCell(originCell,new Vector3Int(0, 1, 0),1);
        ExplodeCell(originCell,new Vector3Int(-1, 0, 0),1);
        ExplodeCell(originCell,new Vector3Int(0, -1, 0),1);
    }

    private void ExplodeCell (Vector3Int cell, Vector3Int celloffset, int iteration)
    {
        cell += celloffset;
        cell.z = 0;
        Tile tile = _tileMap.GetTile<Tile>(cell);
        Debug.Log(cell);

        if (_undestractable.Contains(tile))
        {
            return;
        }

        Vector3 pos = _tileMap.GetCellCenterWorld(new Vector3Int(cell.x - 2, cell.y + 2, cell.z));
        Instantiate(_explotionPref, pos, Quaternion.identity);

        if (_destractable.Contains(tile))
        {
            _itemsSpawner.SpawnItem(tile, pos);
            _tileMap.SetTile(cell, null);
            
        }
        else
        {
            if (iteration == _explotionLength || iteration == 0)
            {
                return;
            }
            ExplodeCell(cell, celloffset, iteration + 1);
        }
    }

}
