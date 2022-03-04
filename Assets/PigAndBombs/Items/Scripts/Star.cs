
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICapable apply))
        {
            apply.GiveStar();

            Debug.Log("GiveStar");

            Destroy(gameObject);
        }
    }

}
