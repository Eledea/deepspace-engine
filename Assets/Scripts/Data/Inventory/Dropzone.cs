using DeepSpace.Controllers;
using DeepSpace.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeepSpace
{
	public class Dropzone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public OverlayController myController;

		public MyEntityInventoryComponent Inventory;
		public Vector2I Index;

		public void OnPointerEnter(PointerEventData eventData)
		{
			myController.OnPointerEnter(this);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			myController.OnPointerExit();
		}
	}
}