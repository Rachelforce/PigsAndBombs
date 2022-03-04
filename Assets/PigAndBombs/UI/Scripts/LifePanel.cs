using UnityEngine;
using UnityEngine.UI;


public class LifePanel : MonoBehaviour
{

    [SerializeField] private Image _healthBar;

    public void OnHealthChange(int helath, bool isHeal)
    {
        _healthBar.fillAmount = helath / 100f;
    }

}
