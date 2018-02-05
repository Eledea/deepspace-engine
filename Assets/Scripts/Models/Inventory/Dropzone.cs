using DeepSpace.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Inventory Inventory;
	public Vector2 Index;

	public void OnPointerEnter(PointerEventData eventData)
	{
		InventoryController.Instance.OnPointerEnter(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		InventoryController.Instance.OnPointerExit();
	}
}