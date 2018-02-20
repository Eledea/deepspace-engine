using DeepSpace.Core;
using DeepSpace.Networking;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeepSpace.Controllers
{
	[System.Serializable]
	public struct UIComponent
	{
		public string Name;
		public GameObject Prefab;
	}

	/// <summary>
	/// The OverlayController controls Interaction with the overlay and the InventorySystem.
	/// </summary>
	public class OverlayController : MonoBehaviour
	{
		public LayerMask EntityLayer;
		public UIComponent[] UIObjects;
		public Sprite[] Sprites;
		public int GraphicSize = 50;

		public Character Character { get; set; }
		public Camera Camera { get; set; }

		bool m_usingConsole;
		GameObject m_consoleObject;

		enum InventoryShowMode { None, Internal, External }
		InventoryShowMode m_showMode;

		MouseButton m_dragButton;

		Entity m_target;
		Queue<GameObject> m_overlayGraphics;

		Dropzone m_startDragDrop;
		Dropzone m_endDragDrop;

		public bool ShowingOverlay
		{
			get { return UsingInventory || m_usingConsole; }
		}

		public bool UsingInventory
		{
			get { return m_showMode != InventoryShowMode.None; }
		}

		void Update()
		{
			Update_OverlayController();
		}

		void Update_OverlayController()
		{
			if (Input.GetKeyDown(KeyCode.BackQuote))
			{
				if (m_usingConsole)
				{
					m_usingConsole = false;
					HideConsole();
				}
				else
				{
					m_usingConsole = true;
					DrawConsole();
				}
			}

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				if (Character.IsUsingInventorySystem)
				{
					m_showMode = InventoryShowMode.None;
					HideOverlay();
				}
				else
				{
					RaycastHit hitInfo;

					m_showMode = InventoryShowMode.Internal;
					m_target = Character;

					Ray fromCamera = new Ray(Camera.transform.position, Camera.transform.forward);
					if (Physics.Raycast(fromCamera, out hitInfo, 3, EntityLayer))
					{
						if (Character.Player.View.GameObjectToEntity(hitInfo.transform.gameObject).Inventory != null)
						{
							m_showMode = InventoryShowMode.External;
							m_target = Character.Player.View.GameObjectToEntity(hitInfo.transform.gameObject);
						}
					}

					DrawOverlay();
				}
			}
		}

		private void DrawConsole()
		{
			m_consoleObject = Instantiate(UIObjects[4].Prefab, this.transform);
			m_consoleObject.GetComponentInChildren<ConsoleInput>().OnConsoleInput += Console.Console.ReadConsoleInput;
			m_consoleObject.name = "Console";
		}

		private void DrawOverlay()
		{
			DrawInventoryOverlay();
		}

		public void DrawInventoryOverlay()
		{
			if (m_overlayGraphics != null)
				HideOverlay();

			m_overlayGraphics = new Queue<GameObject>();

			if (m_showMode == InventoryShowMode.Internal)
			{
				DrawInventoryAtPosition(Character.Inventory, new Vector2(0, 0), Character.Name);
			}
			else if (m_showMode == InventoryShowMode.External)
			{
				DrawInventoryAtPosition(Character.Inventory, new Vector2(0, -2.5F * GraphicSize), Character.Name);
				DrawInventoryAtPosition(m_target.Inventory, new Vector2(0, 2.5F * GraphicSize), m_target.Name);
			}
		}

		private void DrawInventoryAtPosition(MyEntityInventoryComponent c, Vector2 screenPosition, string name)
		{
			var overlay = Instantiate(UIObjects[0].Prefab, this.transform);
			overlay.transform.localPosition = screenPosition;
			overlay.name = string.Format("Inventory: {0}", name);

			for (int x = 0; x < c.InvSize_x; x++)
			{
				for (int y = 0; y < c.InvSize_y; y++)
				{
					var drop = Instantiate(UIObjects[1].Prefab, overlay.transform);
					drop.transform.localPosition = Utility.IndexToWorldSpacePosition(x, y, GraphicSize, c.InvSize_x, c.InvSize_y);
					drop.name = string.Format("{0}:{1}", x, y);
					var d = drop.GetComponentInParent<Dropzone>() as Dropzone;
					d.Inventory = c; d.Index = new Vector2I(x, y);
					d.PointerEnter += OnPointerEnter;
					d.PointerExit += OnPointerExit;

					if (c.IsItemStackAt(x, y))
					{
						var s = c.GetItemStackAt(x, y) as Stack;
						var graphic = Instantiate(UIObjects[2].Prefab, drop.transform);
						graphic.transform.localPosition = Vector2.zero;
						graphic.name = string.Format(s.DefinitionId.ToString());
						var i = graphic.GetComponentInChildren<Interfacable>();
						i.PointerDown += OnPointerDown;
						i.BeginDrag += OnBeginDrag;
						i.EndDrag += OnEndDrag;
						graphic.GetComponentInChildren<Image>().sprite = Sprites[s.DefinitionId.Id];
						graphic.GetComponentInChildren<Text>().text = c.GetItemStackAt(x, y).NumItems.ToString();
						m_overlayGraphics.Enqueue(graphic);
					}
				}
			}
		}

		private void HideConsole()
		{
			Destroy(m_consoleObject);
		}

		private void HideOverlay()
		{
			while (this.transform.childCount > 0)
			{
				Transform child = transform.GetChild(0);
				child.SetParent(null);
				Destroy(child.gameObject);
			}
		}

		/// <summary>
		/// Called by Dropzone when this Player moves their move into a new InventorySlot.
		/// </summary>
		public void OnPointerEnter(Dropzone d)
		{
			m_endDragDrop = d;
		}

		/// <summary>
		/// Called by Dropzone when this Player moves their move into a new InventorySlot.
		/// </summary>
		public void OnPointerExit()
		{
			m_endDragDrop = null;
		}

		/// <summary>
		/// Called by Interfacable when this PLayer clicks down on an Inventory item.
		/// </summary>
		public void OnPointerDown(Interfacable i)
		{
			m_startDragDrop = i.gameObject.GetComponentInParent<Dropzone>();
		}

		/// <summary>
		/// Called by Interfacable when this PLayer first moves their mouse after clicking down.
		/// </summary>
		public void OnBeginDrag(Interfacable i)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
				m_dragButton = MouseButton.Left;
			else if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
				m_dragButton = MouseButton.Right;
			else
				m_dragButton = MouseButton.Unknown;
		}

		/// <summary>
		/// Called by Interfacable when this Player ends an ItemStack drag.
		/// </summary>
		public void OnEndDrag(Interfacable i)
		{
			if (m_endDragDrop != null)
			{
				MouseDrag drag = new MouseDrag(m_startDragDrop.Index, m_endDragDrop.Index, m_dragButton);
				ExecuteDragCommand(drag);
			}

			m_dragButton = MouseButton.None;
		}

		/// <summary>
		/// Interprets the Drag this Player just made and runs any operations that should occur.
		/// </summary>
		void ExecuteDragCommand(MouseDrag drag)
		{
			//We can now use guard clauses to simplify the logic for this mess dramatically! :D

			if (drag.Button == MouseButton.None)
				return;

			switch (drag.Button)
			{
				case MouseButton.Left:
					InventoryManager.OnItemStackMoved(m_startDragDrop.Inventory, drag.Start, m_endDragDrop.Inventory, drag.End);
					break;
				case MouseButton.Right:
					InventoryManager.OnItemStackSplit(m_startDragDrop.Inventory, drag.Start, 0.5F, m_endDragDrop.Inventory, drag.End);
					break;
				case MouseButton.Unknown:
					Debug.LogError("Player is attempting to make a mouse drag with a button that couldn't be identified!");
					return;
			}
		}
	}
}