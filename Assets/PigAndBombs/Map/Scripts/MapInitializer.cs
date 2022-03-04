using UnityEngine;
using UnityEngine.Tilemaps;


public class MapInitializer : MonoBehaviour
{

    [SerializeField] private Tilemap _tileMap;

    [SerializeField] private Tile _leafTile;
    [SerializeField] private Tile _cloverTile;


    void Awake()
    {
        GenerateTile(_leafTile, 20);
        GenerateTile(_cloverTile, 4);
    }

    private void GenerateTile(Tile tile, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3Int tileCoords;
            do
            {
                int y;
                int x = Random.Range(-8, 1);
                if (x % 2 != 0)
                {
                    y = Random.Range(1, 8) * 2;
                }
                else
                {
                    y = Random.Range(1, 16);
                }
                tileCoords = new Vector3Int(x, y, 0);
                Debug.Log($"{_tileMap.GetTile<Tile>(tileCoords)} {x} {y}");
            } while (_tileMap.GetTile<Tile>(tileCoords) != null);
            _tileMap.SetTile(tileCoords, tile);
        }
    }

}
