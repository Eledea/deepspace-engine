using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeepSpace.Controllers
{
	public class Interfacable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public event Action<Interfacable> PointerDown;
		public event Action<Interfacable> BeginDrag;
		public event Action<Interfacable> Drag;
		public event Action<Interfacable> EndDrag;

		public void OnPointerDown(PointerEventData eventData)
		{
			PointerDown(this);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			BeginDrag(this);
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (Drag != null)
			{
				Drag(this);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			EndDrag(this);
		}
	}
}