using UnityEngine;
using UnityEngine.UI;


public class StarPanel : MonoBehaviour
{

    [SerializeField] private Image _starBar;

    public void OnNewStar(int starCount)
    {
        _starBar.fillAmount = starCount / 3f;
    }

}
