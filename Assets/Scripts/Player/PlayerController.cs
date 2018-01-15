using UnityEngine;

/// <summary>
/// Unity-side class for controlling a player ingame.
/// </summary>
public class PlayerController : MonoBehaviour
{
	//For now, let's only work in terms of single-player.

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

	/// <summary>
	/// Does this Player have their dampeners on? Other players don't need to know this
	/// so we can keep this client side.
	/// </summary>
	public bool DampenersOn { get; set; }

	Vector3 rotateTo;

	void Update()
	{
		//TODO: Split this function into multiple seperate functions.

		//TODO: Add Inventory controls.

		Update_PlayerMovement ();
		Update_PlayerRotation ();
	}

	void Update_PlayerMovement()
	{
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
		if (DampenersOn && acceleration.magnitude == 0 && Player.Velocity.magnitude != 0)
		{
			//For every frame where our Velocity is not 0, work out a deceleration based on velocity.
			acceleration -= Update_Deceleration (Player.Velocity);
		}

		Player.Velocity += acceleration;

		//Debug.Log (Player.Velocity);
		//Update our position in our data.
		Player.UpdatePosition ();
	}

	Vector3 Update_Deceleration(Vector3 currentVelocity)
	{
		//We need to find an acceleration from the input Vector3.
		//We want deceleration to be (effectively) constant for the most part, but as we near Vector3.zero, deceleration should change so that the lower the velocity, the lower the acceleration.
		//This means that we need an logarithmic function to calculate deceleration, but what equation should we be using???
		//y = log10(x)+2 where y is acceleration and x is velocity.

		//But we need to work with Vector3 datatypes, so we'll have to work with the magnitude of our velocity...
		//float _acceleration = Mathf.Log10(currentVelocity.magnitude) + 2;

		//Now we need to convert this magnitude to a Vector3.
		//We know that root x^2 + y^2 + z^2 = magnitude of a Vector3. We know the magnitude of the Vector3, but NOT what each component of the Vector should actually be.
		//This is a very difficult situation because we don't know the proportions. This is, in fact, no good.

		//TODO: Make this work!

		Vector3 acceleration = Vector3.zero;

		return acceleration;
	}

	void Update_PlayerRotation()
	{
		//TODO: Fix rotation when "upside-down".
	
		//Find the difference between the mouse position this frame and last.
		Vector2 diffMouse = new Vector2 (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		rotateTo += new Vector3 (diffMouse.x * 5f, -diffMouse.y * 5f, 0);

		//Update the rotation in our data class.
		Player.Rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler (rotateTo.y, rotateTo.x, 0), 30f * Time.deltaTime);
	}
}