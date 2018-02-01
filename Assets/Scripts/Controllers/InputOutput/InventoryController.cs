using DeepSpace.Core;
using DeepSpace.InventorySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity-centric class for allowing Player interaction with the Inventory system.
/// </summary>
public class InventoryController : MonoBehaviour
{
	public static InventoryController Instance { get; protected set; }

	public Camera c;

	public GameObject ItemGraphic;
	public Sprite[] Sprites;
	public int graphicSize = 50;

	void OnEnable()
	{
		Instance = this;

		inventoryUI = this.transform.Find("InventoryUI").gameObject;
		overlayComponents = inventoryUI.GetComponentsInChildren<Image>();
	}

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set; }

	public enum InventoryShowMode { None, Internal, External}
	InventoryShowMode myShowMode;

	GameObject inventoryUI;
	Image[] overlayComponents;

	Dictionary<GameObject, ItemStack> gameObjectToItemStack;
	Dictionary<GameObject, Vector2> gameObjectToIndex;

	Queue<GameObject> overlayGraphics;

	Entity target;

	//bool showingInventoryOverlay;

	GameObject selectedStack_GO;

	Vector2 dragStartPosition;
	RectTransform dragObject;

	/// <summary>
	/// Determines whether this Player should be able to use movement controls.
	/// </summary>
	public bool IsControllable
	{
		get
		{
			return !Player.IsUsingInventorySystem;
		}
	}

	void Update()
	{
		Update_OverlayController ();
			
		if (Player.InventoryUpdateFlag)
		{
			UpdateInventoryView();

			Player.InventoryUpdateFlag = false;
		}
	}

	void Update_OverlayController()
	{
		if (Input.GetKeyDown (KeyCode.I))
		{
			if (Player.IsUsingInventorySystem) 
			{
				myShowMode = InventoryShowMode.None;

				Player.IsUsingInventorySystem = false;
				OnInventoryClose();
			} 
			else
			{
				//TOOD: Move this to a new class? Partial class this class?

				Ray center = new Ray(c.transform.position, c.transform.forward);
				RaycastHit hitInfo;

				myShowMode = InventoryShowMode.Internal;

				if (Physics.Raycast(center, out hitInfo, 3))
				{
					if (SolarSystemView.Instance.GameObjectToEntity(hitInfo.transform.gameObject).HasInventory)
					{
						myShowMode = InventoryShowMode.External;
						target = SolarSystemView.Instance.GameObjectToEntity(hitInfo.transform.gameObject);
					}
				}

				Player.IsUsingInventorySystem = true;
				OnInventoryOpen();
			}
		}
	}

	/// <summary>
	/// Opens this Player's inventory.
	/// </summary>
	private void OnInventoryOpen()
	{
		foreach (Image component in overlayComponents)
		{
			component.enabled = true;
		}

		UpdateInventoryView();
	}

	/// <summary>
	/// Closes this Player's inventory.
	/// </summary>
	private void OnInventoryClose()
	{
		foreach (Image component in overlayComponents)
			component.enabled = false;

		DestroyAllItemStackGraphics ();
	}

	/// <summary>
	/// Updates the Graphics for the Inventories that this Player is looking at when it called.
	/// </summary>
	public void UpdateInventoryView()
	{
		if (gameObjectToItemStack != null)
			DestroyAllItemStackGraphics();

		gameObjectToItemStack = new Dictionary<GameObject, ItemStack>();

		gameObjectToIndex = new Dictionary<GameObject, Vector2>();
		overlayGraphics = new Queue<GameObject>();

		if (myShowMode == InventoryShowMode.Internal)
		{
			ShowInventoryForEntity(Player, new Vector2(0, 0));
		}
		else if (myShowMode == InventoryShowMode.External)
		{
			ShowInventoryForEntity(Player, new Vector2(0, -Utility.SizeToCenter(Player.Inventory.InvSize_y)));
			ShowInventoryForEntity(target, new Vector2(0, Utility.SizeToCenter(target.Inventory.InvSize_y)));
		}
	}

	/// <summary>
	/// Spawns the InventorySlots for an Entity
	/// </summary>
	public void ShowInventoryForEntity(Entity e, Vector2 screenOffset)
	{
		for (int x = 0; x < e.Inventory.InvSize_x; x++)
		{
			for (int y = 0; y < e.Inventory.InvSize_y; y++)
			{
				Vector2 myPosition = Utility.IndexToWorldSpacePosition(x, y, graphicSize, e.Inventory.InvSize_x, e.Inventory.InvSize_y, screenOffset);

				if (e.Inventory.IsItemStackAt(x, y))
					overlayGraphics.Enqueue(SpawnItemStackGraphic(e.Inventory.GetItemStackAt(x, y), myPosition, new Vector2(x, y)));
				else
					overlayGraphics.Enqueue(SpawnItemStackGraphic(null, myPosition, new Vector2(x, y)));
			}
		}
	}

	/// <summary>
	/// Spawns a Graphic for an ItemStack.
	/// </summary>
	private GameObject SpawnItemStackGraphic(ItemStack s, Vector2 position, Vector2 index)
	{
		GameObject myGraphic = Instantiate(ItemGraphic, inventoryUI.transform);
		myGraphic.transform.localPosition = position;
		myGraphic.name = string.Format("ItemGraphic: {0}", index);

		Image myImage = myGraphic.GetComponentInChildren<Image>();
		myImage.rectTransform.sizeDelta = new Vector2 (graphicSize, graphicSize);

		if (s != null)
		{
			gameObjectToItemStack[myGraphic] = s;

			myImage.sprite = Sprites [s.ITypeId];
			myGraphic.GetComponentInChildren<Text> ().text = s.NumItems.ToString();
		}

		return myGraphic;
	}

	/// <summary>
	/// Destroys all ItemStack graphics.
	/// </summary>
	private void DestroyAllItemStackGraphics()
	{
		while (overlayGraphics.Count > 0)
		{
			GameObject myGraphic = overlayGraphics.Dequeue();

			if (gameObjectToItemStack.ContainsKey(myGraphic))
				gameObjectToItemStack.Remove(myGraphic);

			Destroy (myGraphic);
		}
	}

	/// <summary>
	/// Called by Interfacable when this PLayer clicks down on an Inventory item.
	/// </summary>
	public void OnPointerDown(Interfacable i)
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject myGO = i.gameObject;

			if (gameObjectToItemStack.ContainsKey(myGO))
			{
				selectedStack_GO = myGO;
				Debug.Log("Selected ItemGraphic with name: " + selectedStack_GO.name);
			}
		}
	}

	/// <summary>
	/// Called by Interfacable when this PLayer first moves their mouse after clicking down.
	/// </summary>
	public void OnBeginDrag(Interfacable i)
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
		{
			dragStartPosition = i.gameObject.GetComponent<RectTransform>().anchoredPosition;

			dragObject = SpawnItemStackGraphic(null, dragStartPosition, Vector2.zero).GetComponent<RectTransform>();
			dragObject.gameObject.GetComponentInChildren<Image>().enabled = false;
			dragObject.name = "StackDrag";
		}
	}

	/// <summary>
	/// Called by Interfacable when this Player is moving their mouse.
	/// </summary>
	public void OnDrag(Interfacable i, Vector2 mousePosition)
	{
		if (Input.GetMouseButton(0))
		{
			dragObject.transform.position = mousePosition;
		}
	}

	/// <summary>
	/// Called by Interfacable when this Player clicks up while dragging.
	/// </summary>
	public void OnEndDrag(Interfacable i)
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (selectedStack_GO != null)
			{
				Inventory inv = gameObjectToItemStack[selectedStack_GO].Inv;

				//TODO: Make moving Items work again and impleement movement between inventories.

				Vector2 currentIndex = gameObjectToIndex[selectedStack_GO];
				Vector2 newIndex = gameObjectToIndex[selectedStack_GO];

				inv.MoveItemStackTo((int)currentIndex.x, (int)currentIndex.y, inv, (int)newIndex.x, (int)newIndex.y);
				Debug.Log("We moved an ItemStack from " + currentIndex + " to " + newIndex);

				InventoryManager.Instance.UpdateItemStackGraphicsForPlayers();

				Destroy(dragObject.gameObject);

				selectedStack_GO = null;
			}
		}
	}
}