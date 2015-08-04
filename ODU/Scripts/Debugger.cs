using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Debugger class that handles the logs.
/// </summary>
[ExecuteInEditMode]
public class Debugger : MonoBehaviour {
	
	/// <summary>
	/// Enables showing the logs in the Game View
	/// while it is running.
	/// </summary>
	public bool m_GuiEnabled;
	/// <summary>
	/// Enables showing the logs in the browser's
	/// JavaScript console.
	/// </summary>
	public bool m_JsEnabled;
	/// <summary>
	/// The percentage of height of the debugger
	/// relative to the screen size.
	/// </summary>
	public float m_Height = 0.25f;
	/// <summary>
	/// The ODU's game object.
	/// </summary>
	private static GameObject m_GameObject = null;
	/// <summary>
	/// The ODU's Debugger component.
	/// </summary>
	private static Debugger m_Debugger = null;
	/// <summary>
	/// The name.
	/// </summary>
	private static string m_Name = "ODU";
	/// <summary>
	/// The output prefix.
	/// </summary>
	private string m_OutputPrefix;
	/// <summary>
	/// The ODU's position in the viewport.
	/// </summary>
	private Rect m_Position;
	/// <summary>
	/// An iterator used among several member functions.
	/// </summary>
	private int i;
	/// <summary>
	/// The logs counter for every log's type.
	/// </summary>
	private int[] m_Num;
	/// <summary>
	/// The option for showing every log's type.
	/// </summary>
	private bool[] m_Show;
	/// <summary>
	/// The option for hiding the ODU's body.
	/// </summary>
	private bool m_Hide;
	/// <summary>
	/// The size of ODU.
	/// </summary>
	private Vector2 m_OduSize;
	/// <summary>
	/// The ScrollView position.
	/// </summary>
	private Vector2 m_ScrollPosition;
	/// <summary>
	/// The size for the standard button.
	/// </summary>
	private Vector2 m_BtnSize;
	/// <summary>
	/// The buttons' position holder.
	/// </summary>
	private Rect m_ButtonRect;
	/// <summary>
	/// The ScrollView position in the viewport.
	/// </summary>
	private Rect m_ScrollRect;
	/// <summary>
	/// The GUILabels' offset.
	/// </summary>
	private RectOffset m_Offset;
	/// <summary>
	/// The stripped background for the Labels.
	/// </summary>
	private Texture2D[] m_TexBackground;
	/// <summary>
	/// The logs' icons.
	/// </summary>
	private Texture2D[] m_TexLog;
	/// <summary>
	/// The main button's icon.
	/// </summary>
	private Texture2D m_Icon;
	/// <summary>
	/// The list of logs.
	/// </summary>
	private List<DebuggerItem> m_Items;
	/// <summary>
	/// The list of logs for testing.
	/// </summary>
	private List<DebuggerItem> m_TestItems;

	private GUIStyle m_StylePressed;
	private GUIStyle m_StyleNormal;
	
	/// <summary>
	/// Initializes the member variables
	/// </summary>
	void Awake () {
		m_OutputPrefix = m_Name + "::";
		m_Hide = false;
		m_BtnSize = new Vector2(64, 20);
		m_OduSize = new Vector2(Screen.width, (int)(Screen.height * m_Height));
		m_Position = new Rect(0, Screen.height - m_OduSize.y, m_OduSize.x, m_OduSize.y);
		m_Offset = new RectOffset(0,0,0,0);
		m_TexBackground = new Texture2D[2];
		m_TexBackground[0] = MakeTexture((int)m_Position.width, (int)m_Position.height, new Color(0.0f, 0.0f, 0.0f, 0.0f));
		m_TexBackground[1] = MakeTexture((int)m_Position.width, (int)m_Position.height, new Color(0.0f, 0.0f, 0.0f, 0.3f));
		m_TexLog = new Texture2D[3];
		m_Num = new int[3];
		m_Show = new bool[3];
		for (i = 0; i < m_Num.Length; i++) {
			m_Num[i] = 0;
			m_Show[i] = true;
		}
		m_Icon = Resources.Load(m_Name.ToLower()+"_img_icon") as Texture2D;
		m_TexLog[(int)DebuggerItemType.NORMAL] = Resources.Load(m_Name.ToLower()+"_img_normal") as Texture2D;
		m_TexLog[(int)DebuggerItemType.WARNING] = Resources.Load(m_Name.ToLower()+"_img_warning") as Texture2D;
		m_TexLog[(int)DebuggerItemType.ERROR] = Resources.Load(m_Name.ToLower()+"_img_error") as Texture2D;
		m_Items = new List<DebuggerItem>();
		Clear();
	}

	
	/// <summary>
	/// Checks whether a Debugger instance exists and
	/// instantiate one in case there's none.
	/// </summary>
	public static Debugger CheckDebugger () {
		if (m_GameObject == null)
			m_GameObject = GameObject.Find(m_Name);
		if (m_GameObject != null)
			m_Debugger = m_GameObject.GetComponent<Debugger>();
		if (m_Debugger == null) {
			GameObject prefab = Resources.Load(m_Name, typeof(GameObject)) as GameObject;
			m_GameObject = Instantiate(prefab) as GameObject;
			m_GameObject.name = m_Name;
			m_Debugger = m_GameObject.GetComponent<Debugger>();
		}
		return m_Debugger;
	}

	/// <summary>
	/// Logs the specified message object.
	/// </summary>
	/// <param name="message">Message.</param>
	public static void Log (object message) {
		CheckDebugger();
		Debug.Log(message);
		m_Debugger.AddText(message.ToString(), DebuggerItemType.NORMAL);
	}
	/// <summary>
	/// Logs as warning the specified message object.
	/// </summary>
	/// <param name="message">Message.</param>
	public static void LogWarning (object message) {
		CheckDebugger();
		Debug.LogWarning(message);
		m_Debugger.AddText(message.ToString(), DebuggerItemType.WARNING);
	}
	/// <summary>
	/// Logs as error the specified message object.
	/// </summary>
	/// <param name="message">Message.</param>
	public static void LogError (object message) {
		CheckDebugger();
		Debug.LogError(message);
		m_Debugger.AddText(message.ToString(), DebuggerItemType.ERROR);
	}
	/// <summary>
	/// Clear the items list.
	/// </summary>
	private void Clear () {
		if (m_Items != null)
			m_Items.Clear();
		for (i = 0; i < m_Num.Length; i++)
			m_Num[i] = 0;
	}

	/// <summary>
	/// Adds the log into the list.
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="type">Type.</param>
	public void AddText (string message, DebuggerItemType type = DebuggerItemType.NORMAL) {
		m_Items.Add(new DebuggerItem(message, type));
		m_Num[(int)type] += 1;
		if (Application.isWebPlayer && m_JsEnabled) {
			Application.ExternalCall("ODU_Output", m_OutputPrefix+message, type.ToString().ToLower());
		}
	}
	/// <summary>
	/// Creates a random DebuggerItem object
	/// </summary>
	/// <returns>The random item.</returns>
	private List<DebuggerItem> GetTestItems () {
		int i, j;
		string[] message = {
			"This is a test Log message",
			"This is a test LogWarning message",
			"This is a test LogError message"
		};
		DebuggerItemType[] type = {
			DebuggerItemType.NORMAL,
			DebuggerItemType.WARNING,
			DebuggerItemType.ERROR
		};
		List<DebuggerItem> list = new List<DebuggerItem>();
		for (i = 0; i < 3; i++)
			m_Num[i] = 0;
		for (i = 0; i < 10; i++) {
			for (j = 0; j < 3; j++) {
				list.Add(new DebuggerItem(message[j],type[j]));
				m_Num[j]++;
			}
		}
		return list;
	}
	/// <summary>
	/// Raises the GUI event.
	/// Shows the debugger.
	/// </summary>
	void OnGUI () {
		m_OduSize.y = (int)(Screen.height * m_Height);
		m_OduSize.x = Mathf.Max(Screen.width, m_BtnSize.x*5);
		m_Position = new Rect(0, Screen.height - m_OduSize.y, m_OduSize.x, m_OduSize.y);
		if (m_StyleNormal == null || m_StylePressed == null) {
			m_StyleNormal = new GUIStyle(GUI.skin.button);
			m_StylePressed = new GUIStyle(GUI.skin.button);
			m_StylePressed.normal = m_StylePressed.active;
			m_StylePressed.hover = m_StylePressed.active;
		}

		if (Application.isEditor && !Application.isPlaying)
			m_Items = GetTestItems();

		if (m_GuiEnabled) {
			Texture2D image;
			GUIContent content;
			Rect r;
			int line;
			
			// Stores GUI previous values
			Color tmpContentColor = GUI.contentColor;
			Texture2D tmpScrollViewBackground = GUI.skin.scrollView.normal.background;
			Texture2D tmpLabelBackground = GUI.skin.label.normal.background;
			RectOffset tmpLabelMargin = GUI.skin.label.margin;

			GUI.skin.scrollView.normal.background = m_TexBackground[1];
			GUI.skin.label.margin = m_Offset;
			r = new Rect(m_Position.x, m_Position.y, m_Position.width, m_BtnSize.y + m_Position.y);
			if (m_Hide)
				r.y = Screen.height - m_BtnSize.y;
			GUILayout.BeginArea(r);
			int type = (int)DebuggerItemType.NORMAL;
			r = new Rect(m_Position.x, 0, m_BtnSize.x, m_BtnSize.y);
			if (GUI.Button(r, new GUIContent(m_Name, m_Icon))) {
				m_Hide = !m_Hide;
			}
			if (!m_Hide) {
				r = new Rect(m_Position.width - (m_BtnSize.x * 4), 0, m_BtnSize.x, m_BtnSize.y);
				if (GUI.Button(r, "Clear")) {
					Clear();
				}
				for (i = 0; i < m_Num.Length; i++) {
					r = new Rect(m_Position.width - (m_BtnSize.x * (3-i)), 0, m_BtnSize.x, m_BtnSize.y);
					image = m_TexLog[i];
					GUIStyle style;
					if (m_Show[i])
						style = m_StylePressed;
					else
						style = m_StyleNormal;
					if (GUI.Button(r, new GUIContent(m_Num[i].ToString(), image), style)) {
						m_Show[i] = !m_Show[i];
					}
				}
			}
			GUILayout.EndArea();
			if (!m_Hide) {
				r = new Rect(m_Position.x, m_Position.y + m_BtnSize.y, m_Position.width, m_Position.height - m_BtnSize.y);
				GUILayout.BeginArea(r);
				m_ScrollPosition = GUILayout.BeginScrollView(m_ScrollPosition, GUILayout.Width(m_Position.width), GUILayout.Height(m_Position.height - m_BtnSize.y));
				for (i = 0, line = 0; i < m_Items.Count; i++) {
					GUI.skin.label.normal.background = m_TexBackground[line%2];
					type = (int)m_Items[i].m_Type;
					if (m_Show[type]) {
						image = m_TexLog[(int)m_Items[i].m_Type];
						content = new GUIContent(m_Items[i].m_Text, image);
						GUILayout.Label(content);
						line++;
					}
				}
				GUILayout.EndScrollView();
				GUILayout.EndArea();
			}
			
			// restores GUI previous values
			GUI.skin.label.margin = tmpLabelMargin;
			GUI.skin.scrollView.normal.background = tmpScrollViewBackground;
			GUI.skin.label.normal.background = tmpLabelBackground;
			GUI.contentColor = tmpContentColor;
		}
	}

	/// <summary>
	/// Show/hides the given log type.
	/// </summary>
	/// <param name="type">Type.</param>
	private void ToggleShow (int type) {
		m_Show[type] = !m_Show[type];
	}
	
	/// <summary>
	/// Creates a new texture given a size and color.
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	/// <param name="color">Color.</param>
	private Texture2D MakeTexture(int width, int height, Color color) {
		width = Math.Max(1, width);
		height = Math.Max(1, height);
		Color[] pix = new Color[width * height];
		for (int i = 0; i < pix.Length; i++)
			pix[i] = color;
		Texture2D texture = new Texture2D(width, height);
		texture.SetPixels(pix);
		texture.Apply();
		return texture;
	}
	
}
