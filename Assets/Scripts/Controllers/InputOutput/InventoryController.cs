using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
	//In order to interact with our Inventory system, we can use a seperate
	//controller for each of our players that are in the Galaxy we have loaded.

	void OnEnable()
	{
		manager = FindObjectOfType<InventoryManager>();
		view = this.transform.GetComponent<InventoryView>();
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

	static InventoryManager manager;
	static InventoryView view;

	public bool showingInventoryOverlay = false;

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
				view.CloseInventory();
			} 
			else
			{
				showingInventoryOverlay = true;
				//Start showing this Player's Inventory.
				view.OpenInventory();
			}
		}
	}

	void Update_ItemStackController()
	{
	}
}