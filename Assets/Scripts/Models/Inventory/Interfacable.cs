using UnityEngine;
using UnityEngine.EventSystems;

public class Interfacable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	void OnEnable()
	{
		invCtrl = this.transform.parent.parent.GetComponent<InventoryController>();
	}

	InventoryController invCtrl;

	public void OnPointerDown(PointerEventData eventData) {invCtrl.OnPointerDown (this);}

	public void OnBeginDrag (PointerEventData eventData) {invCtrl.OnBeginDrag (this);}

	public void OnDrag (PointerEventData eventData) {invCtrl.OnDrag (this, eventData.position);}

	public void OnEndDrag (PointerEventData eventData) {invCtrl.OnEndDrag (this);}
}