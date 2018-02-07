﻿using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity-centric class for controlling an Entity.
/// </summary>
public class EntityController : MonoBehaviour
{
	enum Axis { X, Y, Z };

	void OnEnable()
	{
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

	Quaternion rotateTo;

	void Update()
	{
		Update_DampenersController ();
		Update_PlayerAcceleration ();

		if (Player.IsUsingInventorySystem == false)
		{
			Update_PlayerRotation ();
			Update_PlayerRoll ();
		}
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

		int i = 0; foreach (Axis axis in axisBindings.Keys)
		{
			float axisInput = axisBindings[axis].Magnitude;
			acceleration += directions[i] * axisInput; i++;
		}

		//TODO: Re-implement dampeners.

		if (Input.GetKeyDown(KeyCode.Escape))
			Player.Velocity = Vector3D.zero;

		Player.Velocity += acceleration * 10f * Time.deltaTime;
	}

	void Update_PlayerRotation()
	{
		Player.Rotation = Player.Rotation * Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * 5f, Vector3.right);
		Player.Rotation = Player.Rotation * Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * 5f, Vector3.up);
	}

	void Update_PlayerRoll()
	{
		Player.Rotation = Player.Rotation * Quaternion.AngleAxis(roll.Magnitude * 2f, Vector3.forward);
	}
}