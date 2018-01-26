using UnityEngine;

/// <summary>
/// Unity-side class for controlling a player's movement ingame.
/// </summary>
public class MovementController : MonoBehaviour
{
	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

	bool DampenersOn = true;
	Vector3 acceleration;
	Vector3 rotateTo;

	void Update()
	{
		//TODO: Make Keyboard inputs not harcoded (eg: Move to a KeyboardManager class).

		if (InventoryController.Instance.IsControllable)
		{
			Update_DampenersController ();

			Update_PlayerRotation ();
		}

		Update_PlayerMovement ();
	}

	void Update_DampenersController()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (DampenersOn == true)
				DampenersOn = false;
			else
				DampenersOn = true;
		}
	} 

	void Update_PlayerMovement()
	{
		//Store changes to velocity this frame here.
		acceleration = Vector3.zero;

		//For now, we'll use hardcoded inputs in 6 directions.
		if (InventoryController.Instance.IsControllable)
		{
			if (Input.GetKey(KeyCode.W))
				acceleration += this.transform.forward;
			if (Input.GetKey(KeyCode.S))
				acceleration -= this.transform.forward;
			if (Input.GetKey(KeyCode.D))
				acceleration += this.transform.right;
			if (Input.GetKey(KeyCode.A))
				acceleration -= this.transform.right;
			if (Input.GetKey(KeyCode.Space))
				acceleration += this.transform.up;
			if (Input.GetKey(KeyCode.LeftControl))
				acceleration -= this.transform.up;
		}

		//We need to factor in any deceleration that might be happening this frame.
		//Does this player have their dampeners on?
		if (DampenersOn)
		{
			if (Player.Velocity.magnitude != 0)
			{
				if (acceleration.x == 0)
					acceleration.x -= Mathf.Clamp(Player.Velocity.x, -1f, 1f);
				if (acceleration.y == 0)
					acceleration.y -= Mathf.Clamp(Player.Velocity.y, -1f, 1f);
				if (acceleration.z == 0)
					acceleration.z -= Mathf.Clamp(Player.Velocity.z, -1f, 1f);
			}
		}

		//Scale and update our velocity.
		Player.Velocity += acceleration * 10f * Time.deltaTime;

		//Debug.Log (Player.Velocity + " " + Player.Velocity.magnitude);
		//Update our position in our data.
		Player.UpdatePosition ();
	}

	void Update_PlayerRotation()
	{
		//TODO: Fix rotation when "upside-down".
	
		//Find the difference between the mouse position this frame and last.
		rotateTo += new Vector3 (Input.GetAxis("Mouse X") * 5f, -Input.GetAxis("Mouse Y") * 5f, rotateTo.z);

		//Update the rotation in our data class.
		Player.Rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler (rotateTo.y, rotateTo.x, rotateTo.z), 30f * Time.deltaTime);
	}
}