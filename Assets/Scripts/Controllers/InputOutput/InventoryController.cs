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
		
	GameObject inventoryUI;
	Image[] overlayComponents;

	List<Inventory> inventoryShow;

	Dictionary<ItemStack, GameObject> itemstackToGameObject;
	Dictionary<GameObject, ItemStack> gameObjectToItemStack;

	Queue<GameObject> overlayGraphics;

	//bool showingInventoryOverlay = false;

	GameObject selectedStack_GO;

	Vector2 dragStartPosition;
	RectTransform dragObject;

	/// <summary>
	/// Gets a value indicating whether this Player should be able to use controls.
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

		if (Player.IsUsingInventorySystem)
		{			
			if (Player.InventoryUpdateFlag)
			{
				//TODO: We only need to update the position and display information here!
				StartInventoryView();
				Player.InventoryUpdateFlag = false;
			}
		}
	}

	void Update_OverlayController()
	{
		if (Input.GetKeyDown (KeyCode.I))
		{
			if (Player.IsUsingInventorySystem) 
			{
				Player.IsUsingInventorySystem = false;
				CloseInventory();
			} 
			else
			{
				Player.IsUsingInventorySystem = true;
				OpenInventory();
			}
		}
	}

	/// <summary>
	/// Called by Interfacable when this PLayer clicks down on an Inventory item.
	/// </summary>
	public void OnPointerDown(Interfacable i)
	{
		if (Input.GetMouseButtonDown (0))
		{
			selectedStack_GO = i.gameObject;

			if (gameObjectToItemStack.ContainsKey (selectedStack_GO))
				Debug.Log ("Selected ItemGraphic with name: " + selectedStack_GO.name);
		}
	}

	/// <summary>
	/// Called by Interfacable when this PLayer first moves their mouse after clicking down.
	/// </summary>
	public void OnBeginDrag(Interfacable i)
	{
		if (Input.GetMouseButtonDown (0) || Input.GetMouseButton(0))
		{
			dragStartPosition = i.gameObject.GetComponent<RectTransform>().anchoredPosition;

			dragObject = SpawnItemStackGraphic (null, dragStartPosition).GetComponent<RectTransform>();
			dragObject.gameObject.GetComponentInChildren<Image>().enabled = false;
			dragObject.name = "StackDrag";
		}
	}

	/// <summary>
	/// Called by Interfacable when this Player is moving their mouse.
	/// </summary>
	public void OnDrag(Interfacable i, Vector2 mousePosition)
	{
		if (Input.GetMouseButton (0))
		{
			dragObject.transform.position = mousePosition;
		}
	}

	/// <summary>
	/// Called by Interfacable when this Player clicks up while dragging.
	/// </summary>
	public void OnEndDrag(Interfacable i)
	{
		if (Input.GetMouseButtonUp (0))
		{
			if (selectedStack_GO != null)
			{
				Inventory inv = gameObjectToItemStack [selectedStack_GO].Inv;

				Vector2 currentIndex = Utility.WorldSpacePositionToIndex(dragStartPosition, graphicSize, inv.InvSize_x, inv.InvSize_y);
				Vector2 newIndex = Utility.WorldSpacePositionToIndex(dragObject.anchoredPosition, graphicSize, inv.InvSize_x, inv.InvSize_y);

				if (currentIndex != newIndex && newIndex != -Vector2.one)
				{
					inv.MoveItemStackTo ((int)currentIndex.x, (int)currentIndex.y, inv, (int)newIndex.x, (int)newIndex.y);
					Debug.Log("We moved an ItemStack from " + currentIndex + " to " + newIndex);

					InventoryManager.Instance.UpdateItemStackGraphicsForPlayers ();
				}

				Destroy (dragObject.gameObject);

				selectedStack_GO = null;
			}
		}
	}

	/// <summary>
	/// Opens this Player's inventory.
	/// </summary>
	private void OpenInventory()
	{
		Player.IsUsingInventorySystem = true;

		foreach (Image component in overlayComponents)
		{
			component.enabled = true;
		}
			
		StartInventoryView ();
	}

	/// <summary>
	/// Closes this PLayer's inventory.
	/// </summary>
	private void CloseInventory()
	{
		Player.IsUsingInventorySystem = false;

		foreach (Image component in overlayComponents)
			component.enabled = false;

		DestroyAllItemStackGraphics ();
	}

	/// <summary>
	/// Spawns the Graphics for the Inventories that this Player is looking at when it is called.
	/// </summary>
	public void StartInventoryView()
	{
		if (Player.IsUsingInventorySystem)
		{
			if (itemstackToGameObject != null)
				DestroyAllItemStackGraphics ();

			itemstackToGameObject = new Dictionary<ItemStack, GameObject>();
			gameObjectToItemStack = new Dictionary<GameObject, ItemStack>();

			overlayGraphics = new Queue<GameObject>();

			for (int x = 0; x < Player.InvSize_x; x++)
			{
				for (int y = 0; y < Player.InvSize_y; y++)
				{
					Vector2 myPosition = Utility.IndexToWorldSpacePosition (x, y, graphicSize, Player.InvSize_x, Player.InvSize_y);

					if (Player.IsItemStackAt (x, y))
						overlayGraphics.Enqueue (SpawnItemStackGraphic (Player.GetItemStackAt (x, y), myPosition));
					else
						overlayGraphics.Enqueue (SpawnItemStackGraphic (null, myPosition));
				}
			}
		}
	}

	/// <summary>
	/// Spawns a Graphic for an ItemStack.
	/// </summary>
	GameObject SpawnItemStackGraphic(ItemStack s, Vector3 position)
	{
		GameObject myGraphic = (GameObject)Instantiate(ItemGraphic, inventoryUI.transform);
		myGraphic.transform.localPosition = position;

		Image myImage = myGraphic.GetComponentInChildren<Image>();
		myImage.rectTransform.sizeDelta = new Vector2 (graphicSize, graphicSize);

		if (s != null)
		{
			itemstackToGameObject [s] = myGraphic;
			gameObjectToItemStack [myGraphic] = s;

			myGraphic.name = s.InventoryIndex.ToString ();
			myImage.sprite = Sprites [s.ITypeInt];
			myGraphic.GetComponentInChildren<Text> ().text = s.ItemCount;
		}

		return myGraphic;
	}

	/// <summary>
	/// Removes an ItemStack graphic.
	/// </summary>
	void RemoveItemStackData(ItemStack s)
	{
		GameObject myGO = itemstackToGameObject [s];
		gameObjectToItemStack.Remove (myGO);
		itemstackToGameObject.Remove (s);
	}

	/// <summary>
	/// Destroys all ItemStack graphics.
	/// </summary>
	void DestroyAllItemStackGraphics()
	{
		while (overlayGraphics.Count > 0)
		{
			GameObject myGraphic = overlayGraphics.Dequeue();

			if (gameObjectToItemStack.ContainsKey(myGraphic))
				RemoveItemStackData (gameObjectToItemStack [myGraphic]);

			Destroy (myGraphic);
		}
	}
}