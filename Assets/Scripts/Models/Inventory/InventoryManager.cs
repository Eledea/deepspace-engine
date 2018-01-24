using UnityEngine;

/// <summary>
/// Manage all our Inventories in the Galaxy that we have loaded.
/// </summary>
public class InventoryManager : MonoBehaviour
{
	/// <summary>
	/// Returns a world space position from an array index.
	/// </summary>
	public Vector3 IndexToWorldSpacePosition(int x, int y, int s, int a, int b)
	{
		return new Vector3 (((x * s) + (s / 2)) - (a * s / 2), ((y * s) + (s / 2)) - (b * s / 2), 0);
	}
}