using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ItemsSpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> _itemPrefs;
    [SerializeField] private GameObject _starPref;

    [SerializeField] private List<Tile> _ordinaryTiles;
    [SerializeField] private List<Tile> _specialTiles;

    public void SpawnItem(Tile tile, Vector3 pos, float spawnChance = 30)
    {
        float chance = Random.Range(0, 100);
        if (_ordinaryTiles.Contains(tile))
        {
            if (chance <= spawnChance)
            {
                Instantiate(_itemPrefs[Random.Range(0, _itemPrefs.Count)], pos, Quaternion.identity);
            }
        }
        else if (_specialTiles.Contains(tile))
        {
            Instantiate(_starPref, pos, Quaternion.identity);
        }
    }

}