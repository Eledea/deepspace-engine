using UnityEngine;
using UnityEngine.EventSystems;

public class Interfacable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public InventoryController myController;

	public void OnPointerDown(PointerEventData eventData)
	{
		myController.OnPointerDown (this);
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		myController.OnBeginDrag (this);
	}

	public void OnDrag (PointerEventData eventData)
	{
		myController.OnDrag (eventData.position);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		myController.OnEndDrag();
	}
}