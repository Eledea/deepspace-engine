using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Graphical class responsible for showing server side data about a Player's Inventory.
/// </summary>
public class InventoryView : MonoBehaviour
{
	public GameObject Graphic;
	public Sprite[] Sprites;
	public int graphicSize = 50;

	void OnEnable()
	{
		invCtrl = this.transform.GetComponent<InventoryController>();

		inventoryUI = this.transform.Find("InventoryOverlay").Find("InventoryUI").gameObject;
		overlayComponents = inventoryUI.GetComponentsInChildren<Image>();
	}

	static GameObject inventoryUI;
	static Image[] overlayComponents;

	List<Inventory> inventoryShow;

	Dictionary<ItemStack, Vector3> itemStackPositionMap;
	Dictionary<ItemStack, GameObject> itemstackGameObjectMap;

	static InventoryController invCtrl;

	/// <summary>
	/// Gets the Player this class is showing the Inventory for.
	/// </summary>
	Player Player
	{
		get
		{
			return invCtrl.Player;
		}
	}

	/// <summary>
	/// Should this class show it's Player's Inventory?
	/// </summary>
	bool ShowingOverlay
	{
		get
		{
			return invCtrl.showingInventoryOverlay;
		}
	}

	/// <summary>
	/// Opens this Player's inventory.
	/// </summary>
	public void OpenInventory()
	{
		//Show our Inventory overlay when we call this function.

		foreach (Image component in overlayComponents)
		{
			//Render the images in our overlay.
			component.enabled = true;
		}

		//Spawn graphics for any ItemStacks in the Inventory(s) we are accessing.
		UpdateInventoryView ();
	}

	/// <summary>
	/// Closes this PLayer's inventory.
	/// </summary>
	public void CloseInventory()
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
	/// Updates the Graphics for the Inventories that this Player is looking at when it is called.
	/// </summary>
	public void UpdateInventoryView()
	{
		//TODO: Call this function from the InventoryManager class when a Player makes a change to an Inventory.

		if (ShowingOverlay)
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
		GameObject myGraphic = (GameObject)Instantiate(Graphic, inventoryUI.transform);
		myGraphic.transform.localPosition = itemStackPositionMap [s];
		itemstackGameObjectMap [s] = myGraphic;

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