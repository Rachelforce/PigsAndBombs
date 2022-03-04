using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public Image joystick;
    public Image sosok;
    private Vector2 _inputVector;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 poss;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick.rectTransform, eventData.position, eventData.pressEventCamera, out poss))
        {
            poss.x = (poss.x / joystick.rectTransform.sizeDelta.x);
            poss.y = (poss.y / joystick.rectTransform.sizeDelta.y);
            _inputVector = new Vector2(poss.x * 2 - 1, poss.y * 2 - 1);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized*1 : _inputVector;

            sosok.rectTransform.anchoredPosition = new Vector2
                (_inputVector.x * (joystick.rectTransform.sizeDelta.x / 2), 
                _inputVector.y * (joystick.rectTransform.sizeDelta.y / 2));
            _inputVector.x = (_inputVector.x > 1f) ? _inputVector.x * 2: _inputVector.x;
            _inputVector.y = (_inputVector.y > 1f) ? _inputVector.y * 2: _inputVector.y;        
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        sosok.rectTransform.anchoredPosition = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        joystick = GetComponent<Image>();
        sosok = transform.GetChild(0).GetComponent<Image>();
    }

    public float Horizontal()
    {
        if (_inputVector.x != 0)
            return _inputVector.x;
        else
            return Input.GetAxisRaw("Horizontal");
    }
    public float Vertical()
    {
        if (_inputVector.y != 0)
            return _inputVector.y;
        else
            return Input.GetAxisRaw("Vertical");
    }

    public float HorizontalMouse()
    {
        if (_inputVector.x != 0)
            return _inputVector.x;
        else
            return Input.GetAxisRaw("JoystickMouseX");
    }
    public float VerticalMouse()
    {
        if (_inputVector.y != 0)
            return _inputVector.y;
        else
            return Input.GetAxisRaw("JoystickMouseY");
    }


    // Update is called once per frame

    void Update()
        {
        
        }
}
