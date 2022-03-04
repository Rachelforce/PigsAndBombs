using UnityEngine;

public class StartGameBanner : MonoBehaviour
{

    private float _countdown = 3f;

    void Update()
    {
        _countdown -= Time.deltaTime;

        if (_countdown <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
