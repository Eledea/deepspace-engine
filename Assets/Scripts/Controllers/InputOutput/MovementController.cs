﻿using UnityEngine;

/// <summary>
/// Unity-side class for controlling a player's movement ingame.
/// </summary>
public class MovementController : MonoBehaviour
{
	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

	private bool DampenersOn = true;
	Vector3 rotateTo;

	void Update()
	{
		Update_PlayerMovement ();
		Update_PlayerRotation ();
	}

	void Update_PlayerMovement()
	{
		//TODO: Make Keyboard inputs not harcoded (eg: Move to a KeyboardManager class).

		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (DampenersOn == true)
				DampenersOn = false;
			else
				DampenersOn = true;
		}

		//Store changes to velocity this frame here.
		Vector3 acceleration = Vector3.zero;

		//For now, we'll use hardcoded inputs in 6 directions.
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

		//We need to factor in any deceleration that might be happening this frame.
		//Does this player have their dampeners on?
		if (DampenersOn && Player.Velocity.magnitude != 0)
		{
			if (acceleration.x == 0)
				acceleration.x -= Deceleration (Player.Velocity.x);
			if (acceleration.y == 0)
				acceleration.y -= Deceleration (Player.Velocity.y);
			if (acceleration.z == 0)
				acceleration.z -= Deceleration (Player.Velocity.z);
		}

		//Scale and update our velocity.
		Player.Velocity += acceleration * 10f * Time.deltaTime;

		//Debug.Log (Player.Velocity + " " + Player.Velocity.magnitude);
		//Update our position in our data.
		Player.UpdatePosition ();
	}

	private float Deceleration(float velocity)
	{
		if (velocity != 0)
			return Mathf.Clamp(velocity, -1f, 1f);
		else
			return 0;
	}

	void Update_PlayerRotation()
	{
		//TODO: Fix rotation when "upside-down".

		//Find the difference between the mouse position this frame and last.
		Vector2 diffMouse = new Vector2 (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		rotateTo += new Vector3 (diffMouse.x * 5f, -diffMouse.y * 5f, rotateTo.z);

		//Update the rotation in our data class.
		Player.Rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler (rotateTo.y, rotateTo.x, rotateTo.z), 30f * Time.deltaTime);
	}
}