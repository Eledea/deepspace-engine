using UnityEngine;

/// <summary>
/// Independent data class for storing a Player.
/// </summary>
public class Player : Inventory
{
	public Player()
	{
		Inventory_ = new ItemStack [5,5];
	}

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

	/// <summary>
	/// Updates the Position for this Player.
	/// </summary>
	public void UpdatePosition()
	{
		if (float.IsNaN(Velocity.x))
			return;
		
		Position += (Velocity * Time.deltaTime) / 1000;
	}
}