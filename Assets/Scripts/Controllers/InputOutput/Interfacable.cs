using UnityEngine;
using UnityEngine.EventSystems;

namespace DeepSpace.Controllers
{
	public class Interfacable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public OverlayController myController;

		public void OnPointerDown(PointerEventData eventData)
		{
			myController.OnPointerDown(this);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			myController.OnBeginDrag(this);
		}

		public void OnDrag(PointerEventData eventData)
		{
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			myController.OnEndDrag();
		}
	}
}