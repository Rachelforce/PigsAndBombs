using UnityEngine;


public class Acceleration : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICapable apply))
        {
            apply.Aceleration();

            Debug.Log("Aceleration");

            Destroy(gameObject);
        }

        
    }

}
