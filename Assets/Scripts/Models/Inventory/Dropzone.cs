using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler
{
	Vector2 myIndex;

	public Vector2 Index
	{
		get
		{
			return myIndex;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
	}
}