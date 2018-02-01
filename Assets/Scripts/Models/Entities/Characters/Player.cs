using DeepSpace.InventorySystem;

/// <summary>
/// Independent data class for storing a Player.
/// </summary>
public class Player : Entity
{
	public Player()
	{
		Inventory = new Inventory(this, 4, 4);
	}

	public float Health { get; private set; }
	public float Oxygen { get; private set; }

	/// <summary>
	/// Returns a value indicating whether this Player is using the Inventory system.
	/// </summary>
	public bool IsUsingInventorySystem { get; set; }

	/// <summary>
	/// Specifies whether or not this Player should update it's Inventory view.
	/// </summary>
	public bool InventoryUpdateFlag { get; set; }
}