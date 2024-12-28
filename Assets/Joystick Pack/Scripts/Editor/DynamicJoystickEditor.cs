using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private Vector2 inputVector;

    public Vector2 InputVector => inputVector;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, joystickBackground.position);
        Vector2 radius = joystickBackground.sizeDelta / 2;
        inputVector = (eventData.position - position) / radius;

        inputVector = inputVector.magnitude > 1 ? inputVector.normalized : inputVector;

        joystickHandle.anchoredPosition = new Vector2(
            inputVector.x * radius.x,
            inputVector.y * radius.y
        );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}
