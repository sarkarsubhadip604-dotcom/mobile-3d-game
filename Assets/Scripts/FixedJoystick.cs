using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float handleRange = 100f;
    
    private Vector2 inputVector;
    private bool isDragging = false;
    
    public float Horizontal => inputVector.x;
    public float Vertical => inputVector.y;
    public Vector2 Direction => new Vector2(Horizontal, Vertical);
    
    private void Start()
    {
        if (background == null)
            background = GetComponent<RectTransform>();
            
        if (handle == null)
            handle = transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, eventData.position, eventData.pressEventCamera, out position);
        
        position = Vector2.ClampMagnitude(position, handleRange);
        handle.anchoredPosition = position;
        
        inputVector = position / handleRange;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        OnDrag(eventData);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}