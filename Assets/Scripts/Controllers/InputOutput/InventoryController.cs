using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	//In order to interact with our Inventory system, we can use a seperate
	//controller for each of our players that are in the Galaxy we have loaded.

	void OnEnable()
	{
		itemstackGameObjectMap = new Dictionary<ItemStack, GameObject> ();
		//Find our InventoryOverlay GameObject.
		inventorySlots = this.transform.Find("InventoryOverlay").Find("InventorySlots").gameObject;
		inventoryComponents = this.transform.GetComponentsInChildren<Image>();
	}

	//First we'll need a reference to the PLayer's inventory...

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set; }

	//We can now access our Player's inventory!
	//First let's figure out a way of showing the data visually...

	public Sprite[] Sprites;

	GameObject inventorySlots;
	Image[] inventoryComponents;

	bool showingInventoryOverlay = false;

	Dictionary<ItemStack, GameObject> itemstackGameObjectMap;

	void Update()
	{
		Update_OverlayController ();

		if (showingInventoryOverlay)
			UpdateInventory ();
	}

	void Update_OverlayController()
	{
		//TODO: Consider if it is more efficient to remove our ItemStack images each time a Player closes
		//their inventory or if it is better to just keep them and turn the SpriteRenderers off.

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

	/// <summary>
	/// Opens this Player's inventory.
	/// </summary>
	private void OpenInventory()
	{
		//Show our Inventory overlay when we call this function.

		foreach (Image component in inventoryComponents)
		{
			//Render the images in our overlay.
			component.enabled = true;
		}
	}

	/// <summary>
	/// Closes this PLayer's inventory.
	/// </summary>
	private void CloseInventory()
	{
		//Hide our Inventory overlay when we call this functiom.

		foreach (Image component in inventoryComponents)
		{
			//Stop rendering the images in our overlay.
			component.enabled = false;
		}
	}

	public void UpdateInventory()
	{
		//TODO: Call this function from the InventoryManager class when a player makes
		//a change to an Inventory instead of every frame.

		//Spawn images at co-ordinates for each ItemStack in our Inventory.
		//We need a way to keep track of our images representing each ItemStack.
		for (int x = 0; x < Player.InvSize_x; x++)
		{
			for (int y = 0; y < Player.InvSize_y; y++)
			{
				//Spawn an image at the correct grid co-ordinate.
				//How do we get the canvas position from our array?
				if (Player.IsItemStackAt(x, y))
				{
					UpdateItemStackGraphic(Player.GetItemStackAt(x, y), IndexToWorldSpacePosition (x, y, 50, 200, 200));
					//Debug.Log ("At " + x + "," + y + " there is an ItemStack containing: " + Player.GetItemStackAt(x, y).ITypeName);
				}

				//TODO: What about for ItemStacks that are no longer part of this inventory?
			}
		}
	}

	/// <summary>
	/// Updates the GameObject for an ItemStack.
	/// </summary>
	void UpdateItemStackGraphic(ItemStack s, Vector3 position)
	{
		//Do we already have a GameObject spawned for this Inventory item?
		if (!itemstackGameObjectMap.ContainsKey (s))
		{
			//Spawn a new image.
			GameObject newGraphic = new GameObject ();
			itemstackGameObjectMap [s] = newGraphic;
			newGraphic.transform.parent = inventorySlots.transform;

			Image image = newGraphic.AddComponent<Image> ();
			image.sprite = Sprites [0];
			image.rectTransform.sizeDelta = new Vector2 (50, 50);
			image.rectTransform.localScale = new Vector3 (1, 1, 1);
		}

		GameObject myGraphic = itemstackGameObjectMap [s];
		myGraphic.transform.localPosition = position;
	}

	/// <summary>
	/// Returns a world space position from an array index.
	/// </summary>
	Vector3 IndexToWorldSpacePosition(int x, int y, int s, int a, int b)
	{
		//TODO: Consider moving this helper function to a Utilities class?

		Vector2 centerOfThisInventory = new Vector2 (a / 2, b / 2);

		Vector2 offsetFromOrigin = new Vector2 ((x * s) + (s / 2), (y * s) + (s / 2));

		return new Vector3(offsetFromOrigin.x - centerOfThisInventory.x, offsetFromOrigin.y - centerOfThisInventory.y, 0);
	}
}