using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick (PointerEventData eventData)
	{
		//TODO: Send a message to the InventoryController and tell it
		//that this InventorySlot was clicked on.
	}
}