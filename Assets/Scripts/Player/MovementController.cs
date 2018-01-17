using UnityEngine;

/// <summary>
/// Unity-side class for controlling a player's movement ingame.
/// </summary>
public class MovementController : MonoBehaviour
{
	//For now, let's only work in terms of single-player.

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

	bool DampenersOn = true;

	Vector3 rotateTo;

	void Update()
	{
		Update_PlayerMovement ();
		Update_PlayerRotation ();
	}

	void Update_PlayerMovement()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (DampenersOn == true)
				DampenersOn = false;
			else
				DampenersOn = true;
		}

		//TODO: Make this work with any rotation.

		//We want to be able to move in each direction relative to the rotation that we current at.
		//Therefore, we need to use the way we're orientated somehow.

		Vector3 acceleration = Vector3.zero;

		if (Input.GetKey (KeyCode.W))
			acceleration.x += 10f * Time.deltaTime;
		if (Input.GetKey (KeyCode.S))
			acceleration.x -= 10f * Time.deltaTime;
		
		if (Input.GetKey (KeyCode.A))
			acceleration.y += 10f * Time.deltaTime;
		if (Input.GetKey (KeyCode.D))
			acceleration.y -= 10f * Time.deltaTime;
		
		if (Input.GetKey (KeyCode.Space))
			acceleration.z += 10f * Time.deltaTime;
		if (Input.GetKey (KeyCode.LeftControl))
			acceleration.z -= 10f * Time.deltaTime;

		//If our inertia dampeners are on, we need to factor in possible deceleration.
		//Is our current acceleration 0?
		if (DampenersOn && acceleration.magnitude == 0)
		{
			//For every frame where our Velocity is not "0", work out a deceleration based on velocity.
			if (Player.Velocity.magnitude > 0.001f)
				acceleration -= Update_Deceleration (Player.Velocity);
			else
				acceleration = Vector3.zero;
		}

		Player.Velocity += acceleration;

		Debug.Log (Player.Velocity);
		//Update our position in our data.
		Player.UpdatePosition ();
	}

	Vector3 Update_Deceleration(Vector3 currentVelocity)
	{
		//We need to find an acceleration from the input Vector3.
		//We want deceleration to be (effectively) constant for the most part, but as we near Vector3.zero, deceleration should change so that the lower the velocity, the lower the acceleration.
		//This means that we need an logarithmic function to calculate deceleration.
		//y = log10(x)+2 where y is acceleration and x is velocity.

		Vector3 acceleration = new Vector3(CalculateDeceleration(Player.Velocity.x), CalculateDeceleration(Player.Velocity.y), CalculateDeceleration(Player.Velocity.z));

		return acceleration;
	}

	float CalculateDeceleration(float velocity)
	{
		//TODO: Add a function to return deceleration. 
	}

	void Update_PlayerRotation()
	{
		//TODO: Fix rotation when "upside-down".
	
		//Find the difference between the mouse position this frame and last.
		Vector2 diffMouse = new Vector2 (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		rotateTo += new Vector3 (diffMouse.x * 5f, -diffMouse.y * 5f, 0);

		//Update the rotation in our data class.
		Player.Rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler (rotateTo.y, rotateTo.x, rotateTo.z), 30f * Time.deltaTime);
	}
}