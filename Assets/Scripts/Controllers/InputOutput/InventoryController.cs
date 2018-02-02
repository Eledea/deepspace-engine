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

	public GameObject[] interfaces;
	public GameObject dropZone;

	public GameObject ItemGraphic;
	public Sprite[] Sprites;
	public int graphicSize = 50;

	void OnEnable()
	{
		Instance = this;
	}

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set; }

	enum InventoryShowMode { None, Internal, External}
	InventoryShowMode myShowMode;

	//bool showingInventoryOverlay;

	Dictionary<GameObject, ItemStack> gameObjectToItemStack;
	Queue<GameObject> overlayGraphics;

	Entity target;

	GameObject selectedStack_GO;
	Vector2 currentIndex;

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
		UpdateInventoryView();
	}

	/// <summary>
	/// Closes this Player's inventory.
	/// </summary>
	private void OnInventoryClose()
	{
		DestroyAllItemStackGraphics();
	}

	/// <summary>
	/// Updates the Graphics for the Inventories that this Player is looking at when it called.
	/// </summary>
	public void UpdateInventoryView()
	{
		if (gameObjectToItemStack != null)
			DestroyAllItemStackGraphics();

		gameObjectToItemStack = new Dictionary<GameObject, ItemStack>();
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
	public void ShowInventoryForEntity(Entity e, Vector2 screenPosition)
	{
		//First we need an Overlay...
		GameObject overlay = Instantiate(interfaces[0], this.transform);
		overlay.transform.localPosition = screenPosition;

		//Spawn a Dropzone for each Inventory slot.
		//Spawn graphics for each ItemStack in this Inventory.
		for (int x = 0; x < e.Inventory.InvSize_x; x++)
		{
			for (int y = 0; y < e.Inventory.InvSize_y; y++)
			{
				GameObject drop = Instantiate(dropZone, overlay.transform);
				drop.transform.position = Utility.IndexToWorldSpacePosition(x, y, graphicSize, e.Inventory.InvSize_x, e.Inventory.InvSize_y);

				if (e.Inventory.IsItemStackAt(x, y))
				{
					ItemStack s = e.Inventory.GetItemStackAt(x, y);

					GameObject graphic = Instantiate(ItemGraphic, drop.transform);
					graphic.name = string.Format("ItemGraphic: {0}", s.ITypeName);
					graphic.GetComponentInChildren<Text>().text = s.NumItems.ToString();

					Image myImage = graphic.GetComponentInChildren<Image>();
					myImage.rectTransform.sizeDelta = new Vector2(graphicSize, graphicSize);
					myImage.sprite = Sprites[s.ITypeId];

					gameObjectToItemStack[graphic] = s;

					overlayGraphics.Enqueue(graphic);
				}
			}
		}
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
			currentIndex = i.gameObject.transform.GetComponent<Dropzone>().Index;

			//TODO: Implement a dragging visual.
		}
	}

	/// <summary>
	/// Called by Interfacable when this Player is moving their mouse.
	/// </summary>
	public void OnDrag(Interfacable i, Vector2 mousePosition)
	{
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

				Vector2 newIndex = i.gameObject.transform.GetComponent<Dropzone>().Index;

				inv.MoveItemStackTo((int)currentIndex.x, (int)currentIndex.y, inv, (int)newIndex.x, (int)newIndex.y);
				Debug.Log("We moved an ItemStack from " + currentIndex + " to " + newIndex);

				InventoryManager.Instance.UpdateItemStackGraphicsForPlayers();

				Destroy(dragObject.gameObject);

				selectedStack_GO = null;
			}
		}
	}
}