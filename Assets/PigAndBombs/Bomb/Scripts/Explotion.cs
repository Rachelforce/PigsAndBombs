using UnityEngine;


public class Explotion : MonoBehaviour
{

    [SerializeField] private float _countdown = 0.5f;


    void Update()
    {
        _countdown -= Time.deltaTime;

        if (_countdown <= 0f)
        {  
            Destroy(gameObject);
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamageable hit))
        {
            hit.Damage();
            
        }

        Debug.Log("Collision");
    }

}
