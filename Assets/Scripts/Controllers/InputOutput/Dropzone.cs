using DeepSpace.Core;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeepSpace.Controllers
{
	public class Dropzone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public MyEntityInventoryComponent Inventory;
		public Vector2I Index;

		public event Action<Dropzone> PointerEnter;
		public event Action PointerExit;

		public void OnPointerEnter(PointerEventData eventData)
		{
			PointerEnter(this);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			PointerExit();
		}
	}
}