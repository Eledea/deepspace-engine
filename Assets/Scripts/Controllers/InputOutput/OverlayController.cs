using DeepSpace.Core;
using DeepSpace.InventorySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity-centric class for allowing Player interaction with the Inventory system.
/// </summary>
public class OverlayController : MonoBehaviour
{
	public GameObject[] interfaces;
	public Sprite[] Sprites;
	public int graphicSize = 50;

	void OnEnable()
	{
		myCamera = transform.parent.GetComponentInChildren<Camera>();
		dragType = InventoryDragType.None;
	}

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set; }

	Camera myCamera;

	enum OverlayShowMode { None, Internal, External }
	OverlayShowMode myShowMode;

	enum InventoryDragType { None, Left, Right}
	InventoryDragType dragType;

	Queue<GameObject> overlayGraphics;

	Entity target;

	Dropzone startDragDrop;
	Dropzone mouseOverDrop;

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

		if (Player.InventoryUpdateFlag)
		{
			OnInventoryUpdate();

			Player.InventoryUpdateFlag = false;
		}
	}

	void Update_OverlayController()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			if (Player.IsUsingInventorySystem)
			{
				myShowMode = OverlayShowMode.None;
				HideOverlay();
			}
			else
			{
				Ray center = new Ray(myCamera.transform.position, myCamera.transform.forward);
				RaycastHit hitInfo;

				myShowMode = OverlayShowMode.Internal;
				target = Player;

				if (Physics.Raycast(center, out hitInfo, 3))
				{
					if (Player.solarSystemView.GameObjectToEntity(hitInfo.transform.gameObject).HasInventory)
					{
						myShowMode = OverlayShowMode.External;
						target = Player.solarSystemView.GameObjectToEntity(hitInfo.transform.gameObject);
					}
				}

				OnInventoryUpdate();
			}
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
			DrawInventoryForEntity(Player, new Vector2(0, 0));
		}
		else if (myShowMode == OverlayShowMode.External)
		{
			DrawInventoryForEntity(Player, new Vector2(0, -2.5f * graphicSize));
			DrawInventoryForEntity(target, new Vector2(0, 2.5f * graphicSize));
		}
	}

	/// <summary>
	/// Hide the Inventory for this Player.
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
				d.Inventory = e.Inventory; d.Index = new Vector2(x, y);
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
		mouseOverDrop = d;
	}

	/// <summary>
	/// Called by Dropzone when this Player moves their move into a new InventorySlot.
	/// </summary>
	public void OnPointerExit()
	{
		mouseOverDrop = null;
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
			dragType = InventoryDragType.Left;
		else if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
			dragType = InventoryDragType.Right;

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
		if (mouseOverDrop != null)
		{
			Inventory currentInv = startDragDrop.Inventory;
			Inventory targetInv = mouseOverDrop.Inventory;

			int my_x = (int)startDragDrop.Index.x; int my_y = (int)startDragDrop.Index.y;
			int new_x = (int)mouseOverDrop.Index.x; int new_y = (int)mouseOverDrop.Index.y;

			ItemStack currentToTargetStack = null;
			ItemStack targetToCurrentStack = null;

			if (dragType != InventoryDragType.None)
			{
				if (currentInv.GetItemStackAt(my_x, my_y) != targetInv.GetItemStackAt(new_x, new_y))
				{
					currentToTargetStack = currentInv.RemoveItemStackFrom(my_x, my_y);

					if (dragType == InventoryDragType.Left)
					{
						if (targetInv.IsItemStackAt(new_x, new_y))
						{
							targetToCurrentStack = targetInv.RemoveItemStackFrom(new_x, new_y);

							if (currentToTargetStack.Type == targetToCurrentStack.Type)
							{
								if (currentToTargetStack.ItemAddability >= targetToCurrentStack.NumItems)
								{
									InventoryManager.Instance.MoveItemsToStack(targetToCurrentStack, currentToTargetStack, targetToCurrentStack.NumItems);
									targetToCurrentStack = null;
								}
								else
									InventoryManager.Instance.MoveItemsToStack(targetToCurrentStack, currentToTargetStack, currentToTargetStack.ItemAddability);
							}
						}
					}
					if (dragType == InventoryDragType.Right)
					{
						if (targetInv.IsItemStackAt(new_x, new_y))
						{
							targetToCurrentStack = targetInv.RemoveItemStackFrom(new_x, new_y);

							if (currentToTargetStack.Type == targetToCurrentStack.Type)
							{
								InventoryManager.Instance.MoveItemsToStack(currentToTargetStack, targetToCurrentStack, Mathf.FloorToInt(currentToTargetStack.NumItems / 2));
								ItemStack s = currentToTargetStack;
								currentToTargetStack = targetToCurrentStack;
								targetToCurrentStack = s;
							}
						}
						else
						{
							InventoryManager.Instance.SpawnNewItemStackAt(currentToTargetStack.Type, 0, targetInv, new_x, new_y);
							targetToCurrentStack = targetInv.GetItemStackAt(new_x, new_y);

							InventoryManager.Instance.MoveItemsToStack(currentToTargetStack, targetToCurrentStack, Mathf.CeilToInt(currentToTargetStack.NumItems / 2));
						}
					}

					if (currentToTargetStack != null)
					{
						if (currentToTargetStack.NumItems != 0)
							targetInv.AddItemStackAt(currentToTargetStack, new_x, new_y);
					}
					if (targetToCurrentStack != null)
					{
						if (targetToCurrentStack.NumItems != 0)
							currentInv.AddItemStackAt(targetToCurrentStack, my_x, my_y);
					}

					InventoryManager.Instance.UpdateItemStackGraphicsForPlayersInSolarSystem(Player.SolarSystem);
				}
			}
		}

		Destroy(dragGraphic.gameObject);

		dragType = InventoryDragType.None;
	}
}