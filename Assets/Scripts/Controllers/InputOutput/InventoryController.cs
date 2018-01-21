using UnityEngine;

public class InventoryController : MonoBehaviour
{
	//In order to interact with our Inventory system, we can use a seperate
	//controller for each of our players that are in the Galaxy we have loaded.

	//Firt we'll need a reference to the PLayer's inventory...

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set; }

	//We can now access our Player's inventory!
	//First let's figure out a way of showing the data visually...

	bool showingInventoryOverlay = false;

	void Update()
	{
		Update_OverlayController ();

		if (showingInventoryOverlay)
			Update_ShowInventory ();
	}

	void Update_OverlayController()
	{
		if (Input.GetKeyDown (KeyCode.I))
		{
			if (showingInventoryOverlay) 
			{
				showingInventoryOverlay = false;
				//Stop showing this Player's Inventory.
			} 
			else
			{
				showingInventoryOverlay = true;
				//Start showing this Player's Inventory.
			}
		}
	}

	void Update_ShowInventory()
	{
		//First we need an overlay interface.

		//For now, we'll hardcode our values.

		//First construct an Inventory interface we can click on...

		//TODO: Build an interface from sprites on the Player's screen.

	}
}