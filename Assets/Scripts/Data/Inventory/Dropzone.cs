﻿using DeepSpace.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeepSpace.InventorySystem
{
	public class Dropzone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public OverlayController myController;

		public Inventory Inventory;
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