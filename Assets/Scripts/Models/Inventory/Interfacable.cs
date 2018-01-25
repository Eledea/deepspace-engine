using UnityEngine;
using UnityEngine.EventSystems;

public class Interfacable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		InventoryController.Instance.OnPointerDown (this);
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		InventoryController.Instance.OnBeginDrag (this);
	}

	public void OnDrag (PointerEventData eventData)
	{
		InventoryController.Instance.OnDrag (this, eventData.position);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		InventoryController.Instance.OnEndDrag (this);
	}
}