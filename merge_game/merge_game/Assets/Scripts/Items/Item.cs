using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{ 
    private Canvas canvas;

    private const float dragOpacity = 0.75f;
    private const float opacity = 1f;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Image img;

    private Vector2 position;

    public ItemType itemType;
    public int currentSlotID;

    [HideInInspector] public bool inSlot = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();

        position = rectTransform.anchoredPosition;
    }

    public void InitializeItem(Canvas _canvas, Vector2 _position, int _id, ItemType _type) 
    {
        itemType = _type;

        canvas = _canvas;
        currentSlotID = _id;
        position = _position;
        rectTransform.anchoredPosition = _position;
        img.sprite = Storage.GetItemIcon(_type);
    }

    public void ChangeSlot(Vector2 position, int newSlotId)
    {
        rectTransform.anchoredPosition = position;
        this.position = rectTransform.anchoredPosition;
        inSlot = true;
        currentSlotID = newSlotId;
    }

    public void MergeItem() 
    {
        itemType += 1;
        img.sprite = Storage.GetItemIcon(itemType);

        SlotManager.instance.TurnRaycasts(true);
        OrderManager.instance.CheckOrder();
    }
    #region Drag and Drop
    public void OnBeginDrag(PointerEventData eventData) 
    {
        canvasGroup.alpha = dragOpacity;
        SlotManager.instance.TurnRaycasts(false);
        inSlot = false;
    }

    public void TurnRayCast(bool isActive) => canvasGroup.blocksRaycasts = isActive;
    public void OnDrag(PointerEventData eventData) => rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    public void OnEndDrag(PointerEventData eventData) 
    {
        canvasGroup.alpha = opacity;
        SlotManager.instance.TurnRaycasts(true);

        if (inSlot == false) CancelDrag();
    }
    private void CancelDrag() => rectTransform.anchoredPosition = position;
    #endregion
}
