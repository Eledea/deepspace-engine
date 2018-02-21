using DeepSpace.Core;
using DeepSpace.Settings;
using UnityEngine;

namespace DeepSpace.Controllers
{
	/// <summary>
	/// The EntityController allows Player control over Entities that may be controlled.
	/// </summary>
	public class EntityController : MonoBehaviour
	{
		public SettingsInput InputBindings;

		public Character Character { get; set; }
		public OverlayController OverlayController { get; set; }

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
			if (Input.GetKeyDown(InputBindings.dampeners))
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

			//TODO: Consider whether it's best to store a pointer to the respective SettingsInput class or to just have direct pointers for each controller class..

			int i = 0; foreach (Direction dir in InputBindings.dirBindings.Keys)
			{
				float dirInput = InputBindings.dirBindings[dir].Magnitude;

				//Did we receive an input from this axis this frame?
				if (dirInput == 0)
				{
					//TODO: Getting this to work is an urgent priority!

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

			rotateTo = rotateTo * Quaternion.AngleAxis(InputBindings.worldRoll.Magnitude * 100F * Time.deltaTime, Vector3.forward);
		}
	}
}