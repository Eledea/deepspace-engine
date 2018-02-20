using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.Controllers
{
	/// <summary>
	/// The EntityController allows Player control over Entities that may be controlled.
	/// </summary>
	public class EntityController : MonoBehaviour
	{
		void OnEnable()
		{
			dirBindings = new Dictionary<Direction, KeyBinding>()
			{
				{ Direction.Right, new KeyBinding(KeyCode.D, KeyCode.A) },
				{ Direction.Up, new KeyBinding(KeyCode.Space, KeyCode.LeftControl) },
				{ Direction.Forward, new KeyBinding(KeyCode.W, KeyCode.S) }
			};

			roll = new KeyBinding(KeyCode.Q, KeyCode.E);
		}

		public Character Character { get; set; }
		public OverlayController OverlayController { get; set; }

		Dictionary<Direction, KeyBinding> dirBindings;
		KeyBinding roll;

		bool DampenersOn = true;

		Vector3 velocity;
		Quaternion rotateTo = new Quaternion(0F, 0F, 0F, 1F);

		void LateUpdate()
		{
			if (OverlayController.ShowingOverlay == false)
			{
				Update_DampenersController();
				Update_EntityVelocity();

				if (OverlayController.UsingInventory == false)
					Update_EntityRotation();
			}

			transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, Time.deltaTime * 30F);
			Character.Transform.Rotation = transform.rotation;
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

		void Update_EntityVelocity()
		{
			Vector3 acceleration = Vector3.zero;

			int i = 0; foreach (Direction dir in dirBindings.Keys)
			{
				float dirInput = dirBindings[dir].Magnitude;

				//Did we receive an input from this axis this frame?
				if (dirInput == 0)
				{
					//If not, then factor in any deceleration we need to apply.
					//What is the Velocity of this Entity in this direction?
					//Take the magnitude of this Velocity and clamp it between a range of 0 and 1.
				}
				acceleration += BaseDirections.VectorInDirection(transform.rotation, dir) * dirInput; i++;
			}

			velocity += acceleration * 15F * Time.deltaTime;
			//TODO: For debugging purposes only. Remove this ASAP.
			if (Input.GetKeyDown(KeyCode.Escape))
				velocity = Vector3.zero;

			transform.position += velocity * Time.deltaTime;
			Character.Transform.Position += velocity * Time.deltaTime;
		}

		void Update_EntityRotation()
		{
			rotateTo = rotateTo * Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * 300F * Time.deltaTime, Vector3.right);
			rotateTo = rotateTo * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 300F * Time.deltaTime, Vector3.up);

			rotateTo = rotateTo * Quaternion.AngleAxis(roll.Magnitude * 100F * Time.deltaTime, Vector3.forward);
		}
	}
}