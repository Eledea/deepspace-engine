using DeepSpace.Characters;
using DeepSpace.Core;
using DeepSpace.InventorySystem;
using DeepSpace.Networking;
using DeepSpace.World;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
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

	/// <summary>
	/// Determines whether this Player should be able to use movement controls.
	/// </summary>
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

	/// <summary>
	/// Draws the Overlay for this Player.
	/// </summary>
	private void DrawOverlay()
	{
		OnInventoryUpdate();
	}

	/// <summary>
	/// Hides the Overlay for this Player.
	/// </summary>
	private void HideOverlay()
	{
		while (this.transform.childCount > 0)
		{
			Transform child = transform.GetChild(0);
			child.SetParent(null);
			Destroy(child.gameObject);
		}
	}

	/// <summary>
	/// Updates the Graphics for the Inventories that this Player is looking at when it called.
	/// </summary>
	public void OnInventoryUpdate()
	{
		if (overlayGraphics != null)
			HideOverlay();

		overlayGraphics = new Queue<GameObject>();

		if (myShowMode == OverlayShowMode.Internal)
		{
			DrawInventoryForEntity(Character, new Vector2(0, 0));
		}
		else if (myShowMode == OverlayShowMode.External)
		{
			DrawInventoryForEntity(Character, new Vector2(0, -2.5f * graphicSize));
			DrawInventoryForEntity(target, new Vector2(0, 2.5f * graphicSize));
		}
	}

	/// <summary>
	/// Spawns the Inventory for an Entity.
	/// </summary>
	private void DrawInventoryForEntity(Entity e, Vector2 screenPosition)
	{
		GameObject overlay = Instantiate(interfaces[0], this.transform);
		overlay.transform.localPosition = screenPosition;
		overlay.name = string.Format("Inventory: {0}", e.Name.ToString());

		for (int x = 0; x < e.Inventory.InvSize_x; x++)
		{
			for (int y = 0; y < e.Inventory.InvSize_y; y++)
			{
				GameObject drop = Instantiate(interfaces[1], overlay.transform);
				drop.transform.localPosition = Utility.IndexToWorldSpacePosition(x, y, graphicSize, e.Inventory.InvSize_x, e.Inventory.InvSize_y);
				drop.name = string.Format("{0}:{1}", x, y);
				Dropzone d = drop.GetComponentInParent<Dropzone>();
				d.Inventory = e.Inventory; d.Index = new Vector2I(x, y);
				d.myController = this;

				if (e.Inventory.IsItemStackAt(x, y))
				{
					ItemStack s = e.Inventory.GetItemStackAt(x, y);
					GameObject graphic = Instantiate(interfaces[2], drop.transform);
					graphic.transform.localPosition = Vector2.zero;
					graphic.name = string.Format(s.TypeName);
					graphic.GetComponentInChildren<Interfacable>().myController = this;
					graphic.GetComponentInChildren<Image>().sprite = Sprites[s.TypeId];
					graphic.GetComponentInChildren<Text>().text = e.Inventory.GetItemStackAt(x, y).NumItems.ToString();
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