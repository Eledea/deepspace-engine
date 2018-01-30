using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity-centric class for controlling a player's movement ingame.
/// </summary>
public class MovementController : MonoBehaviour
{
	enum Axis { X, Y, Z };

	void OnEnable()
	{
		//TODO: Make Keyboard inputs not harcoded (eg: Move to a KeyboardManager class).

		axisBindings = new Dictionary<Axis, KeyBinding>()
		{
			{ Axis.X, new KeyBinding(KeyCode.W, KeyCode.S) },
			{ Axis.Y, new KeyBinding(KeyCode.D, KeyCode.A) },
			{ Axis.Z, new KeyBinding(KeyCode.Space, KeyCode.LeftControl) }
		};

		roll = new KeyBinding (KeyCode.Q, KeyCode.E);
	}

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

	Dictionary<Axis, KeyBinding> axisBindings;
	KeyBinding roll;

	bool DampenersOn = true;

	Vector3 rotateTo;

	void Update()
	{
		Update_DampenersController ();
		Update_PlayerAcceleration ();

		if (InventoryController.Instance.IsControllable)
		{
			Update_PlayerRotation ();
			Update_PlayerRoll ();
		}

		if (Player.Velocity.magnitude != 0)
			Player.UpdatePosition ();
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

	void Update_PlayerAcceleration()
	{
		Vector3 acceleration = Vector3.zero;
		List<Vector3> directions = new List<Vector3> { transform.forward.normalized, transform.right.normalized, transform.up.normalized };

		int i = 0;
		foreach (Axis axis in axisBindings.Keys)
		{
			float axisInput = axisBindings [axis].magnitude;

			//TODO: Re-implement velocity dampening system.

			acceleration += directions[i] * axisInput;
			i++;
		}

		if (Input.GetKeyDown (KeyCode.Escape))
			Player.Velocity = Vector3D.zero;

		Player.Velocity += acceleration * 10f * Time.deltaTime;
	}

	void Update_PlayerRotation()
	{
		Player.Rotation = Player.Rotation * Quaternion.AngleAxis (-Input.GetAxis ("Mouse Y") * 5f, Vector3.right);
		Player.Rotation = Player.Rotation * Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * 5f, Vector3.up);
	}

	void Update_PlayerRoll()
	{
		Player.Rotation = Player.Rotation * Quaternion.AngleAxis (roll.magnitude * 2f, Vector3.forward);
	}
}