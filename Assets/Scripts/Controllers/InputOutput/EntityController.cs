using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.Controllers
{
	/// <summary>
	/// Unity-centric class for controlling an Entity.
	/// </summary>
	public class EntityController : MonoBehaviour
	{
		//TODO: Re-write this class with direction system.

		void OnEnable()
		{
			axisBindings = new Dictionary<BaseDirections.Axis, KeyBinding>()
			{
				{ BaseDirections.Axis.X, new KeyBinding(KeyCode.W, KeyCode.S) },
				{ BaseDirections.Axis.Y, new KeyBinding(KeyCode.D, KeyCode.A) },
				{ BaseDirections.Axis.Z, new KeyBinding(KeyCode.Space, KeyCode.LeftControl) }
			};

			roll = new KeyBinding(KeyCode.Q, KeyCode.E);
		}

		public Character Player { get; set; }

		Dictionary<BaseDirections.Axis, KeyBinding> axisBindings;
		KeyBinding roll;

		bool DampenersOn = true;

		Quaternion rotateTo;

		void Update()
		{
			Update_DampenersController();
			Update_PlayerAcceleration();

			if (Player.IsUsingInventorySystem == false)
			{
				Update_PlayerRotation();
				Update_PlayerRoll();
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

			int i = 0; foreach (BaseDirections.Axis axis in axisBindings.Keys)
			{
				float axisInput = axisBindings[axis].Magnitude;
				acceleration += directions[i] * axisInput; i++;
			}

			if (Input.GetKeyDown(KeyCode.Escape))
				Player.Transform.Velocity = Vector3D.zero;

			Player.Transform.Velocity += acceleration * 10F * Time.deltaTime;
		}

		void Update_PlayerRotation()
		{
			Player.Transform.Rotation = Player.Transform.Rotation * Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * 5F, Vector3.right);
			Player.Transform.Rotation = Player.Transform.Rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 5F, Vector3.up);
		}

		void Update_PlayerRoll()
		{
			Player.Transform.Rotation = Player.Transform.Rotation * Quaternion.AngleAxis(roll.Magnitude * 2F, Vector3.forward);
		}
	}
}