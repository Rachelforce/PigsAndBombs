using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DamageText : MonoBehaviour
{

    [SerializeField] private Text _textField;
    [SerializeField] private string _text;


    public void OnHealthChange(int damage, bool isHeal)
    {
        if (!isHeal)
        {
            StartCoroutine(DisplayTextRoutine(1f));
            Debug.Log("ONK");
        }
    }

    IEnumerator DisplayTextRoutine(float waitTime)
    {
        _textField.text = _text;
        _textField.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        _textField.gameObject.SetActive(false);
    }

}
