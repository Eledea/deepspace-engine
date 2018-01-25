using DeepSpace.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	//In order to interact with our Inventory system, we can use a seperate
	//controller for each of our players that are in the Galaxy we have loaded.

	public GameObject ItemGraphic;
	public Sprite[] Sprites;
	public int graphicSize = 50;

	void OnEnable()
	{
		inventoryUI = this.transform.Find("InventoryUI").gameObject;
		overlayComponents = inventoryUI.GetComponentsInChildren<Image>();
	}

	//First we'll need a reference to the Player we are controlling...

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set; }

	//We can now access our Player!
	//First let's figure out a way of showing the Inventory data visually...

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
		
	GameObject inventoryUI;
	Image[] overlayComponents;

	/// <summary>
	/// The Inventories that we should currently be showing.
	/// </summary>
	List<Inventory> inventoryShow;

	Dictionary<ItemStack, Vector3> itemStackPositionMap;

	Dictionary<ItemStack, GameObject> itemstackGameObjectMap;
	Dictionary<GameObject, ItemStack> gameObjectItemStackMap;

	bool showingInventoryOverlay = false;

	GameObject selectedStack_GO;

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
		//Is our mouse down this frame?
		if (Input.GetMouseButton (0))
		{
			//Do a raycast and select the graphic we clicked on.
		}

		//Do we have a graphic selected?
		//Check for mouse movement.

		//If we get mouse movement, check for the mouse button going back up.
		if (Input.GetMouseButtonUp (0))
		{
			//Do another raycast to find out if we over a graphic. If so, store it's GameObject locally.
			//Are we over the same Graphic? If so, display the data for it in the Inventory.
		
			//If not, move the graphic we have selected to this graphic's position and vice versa.
			//Update the data for the ItemStacks.
		}
	

		/*if (Input.GetMouseButtonUp (0))
		{
			Debug.Log ("Our mouse went up this frame.");

			//Do we have an ItemStack selected?
			if (selectedStack_GO != null)
			{
				//Move the stack to a new Inventory slot both in our data and graphically.
				Inventory inv = gameObjectItemStackMap [selectedStack_GO].Inv;
				//We need the index of the current ItemStack position and the index of the new ItemStack position.
				Vector2 currentIndex = Utility.WorldSpacePositionToIndex(selectedStack_GO.transform.position, graphicSize, inv.InvSize_x, inv.InvSize_y);
				Vector2 newIndex = Utility.WorldSpacePositionToIndex(selectedStack_GO.transform.position, graphicSize, inv.InvSize_x, inv.InvSize_y);

				inv.MoveItemStackTo ((int)currentIndex.x, (int)currentIndex.y, inv, (int)newIndex.x, (int)newIndex.y);

				selectedStack_GO = null;

				Debug.Log("We moved an ItemStack to " + newIndex);
			}
		}*/
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

		//Spawn graphics for any ItemStacks in the Inventory(s) we are accessing.
		StartInventoryView ();
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

		//Remove all the ItemStack graphics on this Player's overlay.
		RemoveAllItemStackGraphics ();
	}

	/// <summary>
	/// Spawns the Graphics for the Inventories that this Player is looking at when it is called.
	/// </summary>
	public void StartInventoryView()
	{
		//TODO: Call this function from the InventoryManager class when a Player makes a change to an Inventory.

		if (showingInventoryOverlay)
		{
			//Remove the existing graphics.
			if (itemstackGameObjectMap != null)
				RemoveAllItemStackGraphics ();

			itemStackPositionMap = new Dictionary<ItemStack, Vector3>();

			itemstackGameObjectMap = new Dictionary<ItemStack, GameObject>();
			gameObjectItemStackMap = new Dictionary<GameObject, ItemStack>();

			for (int x = 0; x < Player.InvSize_x; x++)
			{
				for (int y = 0; y < Player.InvSize_y; y++)
				{
					//Is there an ItemStack at this position this iteration?
					if (Player.IsItemStackAt (x, y))
					{
						itemStackPositionMap [Player.GetItemStackAt (x, y)] = Utility.IndexToWorldSpacePosition (x, y, graphicSize, Player.InvSize_x, Player.InvSize_y);
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
	/// Spawns a Graphic for an ItemStack.
	/// </summary>
	void SpawnItemStackGraphic(ItemStack s)
	{
		//Spawn a new image.
		GameObject myGraphic = (GameObject)Instantiate(ItemGraphic, inventoryUI.transform);
		myGraphic.transform.localPosition = itemStackPositionMap [s];
		itemstackGameObjectMap [s] = myGraphic;
		gameObjectItemStackMap [myGraphic] = s;

		Image myImage = myGraphic.GetComponentInChildren<Image>();
		myImage.sprite = Sprites [s.ITypeInt];
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