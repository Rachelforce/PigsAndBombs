using UnityEngine;

public class Healing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICapable apply))
        {
            apply.Healing();

            Debug.Log("Healing");

            Destroy(gameObject);
        } 
    }

}
