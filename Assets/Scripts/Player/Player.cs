using UnityEngine;

/// <summary>
/// Independent data class for storing a Player.
/// </summary>
public class Player
{
	/// <summary>
	/// The Name of this Player.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The SolarSystem that this Player is currently in.
	/// </summary>
	public SolarSystem SolarSystem { get; set; }

	/// <summary>
	/// The world-space position of this Player in it's SolarSystem.
	/// </summary>
	public Vector3 Position { get; set; }

	/// <summary>
	/// The Velocity of this Player.
	/// </summary>
	public Vector3 Velocity { get; set;}

	/// <summary>
	/// The Rotation of this Player.
	/// </summary>
	public Quaternion Rotation { get; set; }

	/// <summary>
	/// The Health of this Player (out of 100).
	/// </summary>
	public float Health { get; set; }

	/// <summary>
	/// The Oxygen level of this Player (out of 100).
	/// </summary>
	public float Oxygen { get; set; }

	//What is the best way of storing the data for Items in an Inventory???
	//We have a few options...

	//The data type that makes the most sense in this situation in a 2D array
	//for that columns and rows that we will have on the interface.

	//But HOW will the game know what each slot has???
	//We COULD use integers to store data and have each interger correspond to an
	//item type in an enum....but how will we know how much of each item we will have???

	//A 2D int array in this situation is no good, because we are NOT able to know
	//how many of an item we have in each slot.

	/// <summary>
	/// The Inventory of this Player.
	/// </summary>
	public ItemStack[,] Inventory { get; set; }

	/// <summary>
	/// Updates the Position for this Player.
	/// </summary>
	public void UpdatePosition()
	{
		Position += (Velocity * Time.deltaTime) / 1000;
	}

	/// <summary>
	/// Adds an ItemStack at this array position.
	/// </summary>
	public void AddItemStackAt(ItemStack s, int x, int y)
	{
		if (Inventory == null)
			Inventory = new ItemStack [5,5];

		Inventory [x, y] = s;
	}

	/// <summary>
	/// Removes an ItemStack from this array position.
	/// </summary>
	public void RemoveItemStackFrom(int x, int y)
	{
		if (Inventory != null)
			Inventory [x, y] = null;
	}

	/// <summary>
	/// Gets an ItemStack at this array position.
	/// </summary>
	public ItemStack GetItemStackAt(int x, int y)
	{
		return Inventory [x, y];
	}
}