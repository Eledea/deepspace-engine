using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	//In order to interact with our Inventory system, we can use a seperate
	//controller for each of our players that are in the Galaxy we have loaded.

	public GameObject Graphic;
	public Sprite[] Sprites;
	public int graphicSize = 50;

	void OnEnable()
	{
		manager = FindObjectOfType<InventoryManager>();

		inventoryUI = this.transform.Find("InventoryOverlay").Find("InventoryUI").gameObject;
		overlayComponents = this.transform.GetComponentsInChildren<Image>();
	}

	//First we'll need a reference to the Player's inventory...

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set; }

	//We can now access our Player's inventory!
	//First let's figure out a way of showing the data visually...

	/// <summary>
	/// Gets a value indicating whether this Player should be able to use controls.
	/// </summary>
	public bool IsControllable
	{
		get
		{
			return !showingInventoryOverlay;
		}
	}

	InventoryManager manager;

	GameObject inventoryUI;
	Image[] overlayComponents;

	bool showingInventoryOverlay = false;

	Dictionary<ItemStack, Vector3> itemStackPositionMap;
	Dictionary<ItemStack, GameObject> itemstackGameObjectMap;

	void Update()
	{
		Update_OverlayController ();

		if (showingInventoryOverlay)
			Update_ItemStackController ();
	}

	void Update_OverlayController()
	{
		if (Input.GetKeyDown (KeyCode.I))
		{
			if (showingInventoryOverlay) 
			{
				showingInventoryOverlay = false;
				//Stop showing this Player's Inventory.
				CloseInventory();
			} 
			else
			{
				showingInventoryOverlay = true;
				//Start showing this Player's Inventory.
				OpenInventory();
			}
		}
	}

	void Update_ItemStackController()
	{

	}

	/// <summary>
	/// Opens this Player's inventory.
	/// </summary>
	private void OpenInventory()
	{
		//Show our Inventory overlay when we call this function.

		foreach (Image component in overlayComponents)
		{
			//Render the images in our overlay.
			component.enabled = true;
		}

		UpdateInventoryView ();
	}

	/// <summary>
	/// Closes this PLayer's inventory.
	/// </summary>
	private void CloseInventory()
	{
		//Hide our Inventory overlay when we call this functiom.

		foreach (Image component in overlayComponents)
		{
			//Stop rendering the images in our overlay.
			component.enabled = false;
		}

		RemoveAllItemStackGraphics ();
	}

	/// <summary>
	/// Updates the Graphics for the Inventories that this Player is looking at when it is called.
	/// </summary>
	public void UpdateInventoryView()
	{
		//TODO: Call this function from the InventoryManager class when a Player makes a change to an Inventory.

		if (showingInventoryOverlay)
		{
			//Remove the existing graphics.
			if (itemstackGameObjectMap != null)
				RemoveAllItemStackGraphics ();

			itemStackPositionMap = new Dictionary<ItemStack, Vector3>();
			itemstackGameObjectMap = new Dictionary<ItemStack, GameObject>();

			for (int x = 0; x < Player.InvSize_x; x++)
			{
				for (int y = 0; y < Player.InvSize_y; y++)
				{
					//Is there an ItemStack at this position this iteration?
					if (Player.IsItemStackAt (x, y))
					{
						itemStackPositionMap [Player.GetItemStackAt (x, y)] = manager.IndexToWorldSpacePosition (x, y, graphicSize, Player.InvSize_x, Player.InvSize_y);
					}
				}
			}

			//Spawn new graphics.
			List<ItemStack> itemStacks = new List<ItemStack> (itemStackPositionMap.Keys);

			foreach (ItemStack s in itemStacks)
			{
				SpawnItemStackGraphic (s);
			}
		}
	}

	/// <summary>
	/// Updates the GameObject for an ItemStack.
	/// </summary>
	void SpawnItemStackGraphic(ItemStack s)
	{
		//Spawn a new image.
		GameObject myGraphic = (GameObject)Instantiate(Graphic, inventoryUI.transform);
		myGraphic.transform.localPosition = itemStackPositionMap [s];
		itemstackGameObjectMap [s] = myGraphic;

		Image myImage = myGraphic.GetComponentInChildren<Image>();
		myImage.sprite = Sprites [0];
		myImage.rectTransform.sizeDelta = new Vector2 (graphicSize, graphicSize);
		myImage.rectTransform.localScale = new Vector3 (1, 1, 1);
	
		myGraphic.GetComponentInChildren<Text>().text = s.ItemCount;;
	}

	/// <summary>
	/// Removes an ItemStack graphic.
	/// </summary>
	void RemoveItemStackGraphic(ItemStack s)
	{
		Destroy (itemstackGameObjectMap [s]);
		itemstackGameObjectMap.Remove (s);
	}

	/// <summary>
	/// Removes all ItemStack graphics.
	/// </summary>
	void RemoveAllItemStackGraphics()
	{
		List<ItemStack> graphics = new List<ItemStack> (itemstackGameObjectMap.Keys);

		foreach (ItemStack s in graphics)
		{
			RemoveItemStackGraphic (s);
		}
	}
}