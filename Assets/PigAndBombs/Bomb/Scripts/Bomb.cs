using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _countdown = 2f;

    // Update is called once per frame
    void Update()
    {
        _countdown -= Time.deltaTime;
        if (_countdown <= 0f)
        {
            FindObjectOfType<MapDestroyer>().Explode(transform.position);
            Destroy(gameObject);
        }
    }
}
