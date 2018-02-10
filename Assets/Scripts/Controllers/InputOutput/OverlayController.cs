using DeepSpace.Core;
using DeepSpace.Networking;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeepSpace.Controllers
{
	/// <summary>
	/// The OverlayController controls Interaction with the overlay and the InventorySystem.
	/// </summary>
	public class OverlayController : MonoBehaviour
	{
		public GameObject[] interfaces;
		public Sprite[] Sprites;
		public int graphicSize = 50;

		void OnEnable()
		{
			myCamera = transform.parent.GetComponentInChildren<Camera>();
		}

		public Character Character { get; set; }

		Camera myCamera;

		enum OverlayShowMode { None, Internal, External }
		OverlayShowMode myShowMode;

		MouseButton dragButton;

		Entity target;
		Queue<GameObject> overlayGraphics;

		Dropzone startDragDrop;
		Dropzone endDragDrop;

		RectTransform dragGraphic;

		public bool ShowingOverlay
		{
			get
			{
				return myShowMode == OverlayShowMode.None;
			}
		}

		void Update()
		{
			Update_OverlayController();
		}

		void Update_OverlayController()
		{
			if (Input.GetKeyDown(KeyCode.I))
			{
				if (Character.IsUsingInventorySystem)
				{
					myShowMode = OverlayShowMode.None;
					HideOverlay();
				}
				else
				{
					RaycastHit hitInfo;

					myShowMode = OverlayShowMode.Internal;
					target = Character;

					if (Physics.Raycast(new Ray(myCamera.transform.position, myCamera.transform.forward), out hitInfo, 3))
					{
						if (Character.Player.View.GameObjectToEntity(hitInfo.transform.gameObject).HasInventory)
						{
							myShowMode = OverlayShowMode.External;
							target = Character.Player.View.GameObjectToEntity(hitInfo.transform.gameObject);
						}
					}

					DrawOverlay();
				}
			}
		}

		private void DrawOverlay()
		{
			OnInventoryUpdate();
		}

		private void HideOverlay()
		{
			while (this.transform.childCount > 0)
			{
				Transform child = transform.GetChild(0);
				child.SetParent(null);
				Destroy(child.gameObject);
			}
		}

		public void OnInventoryUpdate()
		{
			if (overlayGraphics != null)
				HideOverlay();

			overlayGraphics = new Queue<GameObject>();

			if (myShowMode == OverlayShowMode.Internal)
			{
				DrawInventoryAtPosition(Character.Inventory, new Vector2(0, 0), Character.Name);
			}
			else if (myShowMode == OverlayShowMode.External)
			{
				DrawInventoryAtPosition(Character.Inventory, new Vector2(0, -2.5f * graphicSize), Character.Name);
				DrawInventoryAtPosition(target.Inventory, new Vector2(0, 2.5f * graphicSize), target.Name);
			}
		}

		private void DrawInventoryAtPosition(MyInventoryComponent c, Vector2 screenPosition, string name)
		{
			GameObject overlay = Instantiate(interfaces[0], this.transform);
			overlay.transform.localPosition = screenPosition;
			overlay.name = string.Format("Inventory: {0}", name);

			for (int x = 0; x < c.InvSize_x; x++)
			{
				for (int y = 0; y < c.InvSize_y; y++)
				{
					GameObject drop = Instantiate(interfaces[1], overlay.transform);
					drop.transform.localPosition = Utility.IndexToWorldSpacePosition(x, y, graphicSize, c.InvSize_x, c.InvSize_y);
					drop.name = string.Format("{0}:{1}", x, y);
					Dropzone d = drop.GetComponentInParent<Dropzone>();
					d.Inventory = c; d.Index = new Vector2I(x, y);
					d.myController = this;

					if (c.IsItemStackAt(x, y))
					{
						ItemStack s = c.GetItemStackAt(x, y);
						GameObject graphic = Instantiate(interfaces[2], drop.transform);
						graphic.transform.localPosition = Vector2.zero;
						graphic.name = string.Format(s.TypeName);
						graphic.GetComponentInChildren<Interfacable>().myController = this;
						graphic.GetComponentInChildren<Image>().sprite = Sprites[s.TypeId];
						graphic.GetComponentInChildren<Text>().text = c.GetItemStackAt(x, y).NumItems.ToString();
						overlayGraphics.Enqueue(graphic);
					}
				}
			}
		}

		/// <summary>
		/// Called by Dropzone when this Player moves their move into a new InventorySlot.
		/// </summary>
		public void OnPointerEnter(Dropzone d)
		{
			endDragDrop = d;
		}

		/// <summary>
		/// Called by Dropzone when this Player moves their move into a new InventorySlot.
		/// </summary>
		public void OnPointerExit()
		{
			endDragDrop = null;
		}

		/// <summary>
		/// Called by Interfacable when this PLayer clicks down on an Inventory item.
		/// </summary>
		public void OnPointerDown(Interfacable i)
		{
			startDragDrop = i.gameObject.GetComponentInParent<Dropzone>();
		}

		/// <summary>
		/// Called by Interfacable when this PLayer first moves their mouse after clicking down.
		/// </summary>
		public void OnBeginDrag(Interfacable i)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
				dragButton = MouseButton.Left;
			else if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
				dragButton = MouseButton.Right;
			else
				dragButton = MouseButton.Unknown;

			dragGraphic = Instantiate(interfaces[3], this.transform).GetComponent<RectTransform>();
			dragGraphic.gameObject.transform.localPosition = Vector2.zero;
			//dragGraphic.gameObject.GetComponent<Image>().sprite = Sprites[startDragDrop.Inventory.GetItemStackAt((int)startDragDrop.Index.x, (int)startDragDrop.Index.y).TypeId];
		}

		/// <summary>
		/// Called by Interfacable when this Player is moving their mouse.
		/// </summary>
		public void OnDrag(Vector2 mousePosition)
		{
			dragGraphic.position = mousePosition;
		}

		/// <summary>
		/// Called by Interfacable when this Player ends an ItemStack drag.
		/// </summary>
		public void OnEndDrag()
		{
			if (endDragDrop != null)
			{
				MouseDrag drag = new MouseDrag(startDragDrop.Index, endDragDrop.Index, dragButton);
				ExecuteDragCommand(drag);
			}

			Destroy(dragGraphic.gameObject);
			dragButton = MouseButton.None;
		}

		/// <summary>
		/// Interprets the Drag this Player just made and run any operations that we should be doing.
		/// </summary>
		void ExecuteDragCommand(MouseDrag drag)
		{
			//We can now use guard clauses to simplify the logic for this mess dramatically! :D

			if (drag.Button == MouseButton.None)
				return;

			switch (drag.Button)
			{
				case MouseButton.Left:
					InventoryManager.Instance.MoveItemStackTo(startDragDrop.Inventory, drag.Start, endDragDrop.Inventory, drag.End);
					break;
				case MouseButton.Right:
					InventoryManager.Instance.SplitItemStackAtTo(startDragDrop.Inventory, drag.Start, 0.5f, endDragDrop.Inventory, drag.End);
					break;
				case MouseButton.Unknown:
					Debug.LogError("Player is attempting to make a mouse drag with a button that couldn't be identified!");
					return;
			}

			InventoryManager.Instance.UpdateItemStackGraphicsForPlayersInSolarSystem(Character.SolarSystem);
		}
	}
}