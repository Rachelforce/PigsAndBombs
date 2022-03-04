using UnityEngine;
using UnityEngine.Tilemaps;


public class BombSpawner : MonoBehaviour
{

    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private GameObject _bombPrefab;

    private int _cellToPosOffsetX = 2;
    private int _cellToPosOffsetY = -2;

    private float _bombCooldownTime;


    public void SpawnBomb()
    {
        if (Time.time> _bombCooldownTime )
        {
            float cooldownRate = 2f;
            _bombCooldownTime = Time.time + cooldownRate;

            Vector3 worldPos = transform.position;
            Vector3Int cell = _tilemap.WorldToCell(worldPos);
            Vector3 cellCenterPos = _tilemap.GetCellCenterWorld(new Vector3Int(cell.x - _cellToPosOffsetX, cell.y - _cellToPosOffsetY, 0));

            Instantiate(_bombPrefab, cellCenterPos, Quaternion.identity);
        }
    }

}
