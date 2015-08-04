using UnityEngine;
using System.Collections;

/// <summary>
/// Debugger item type.
/// </summary>
public enum DebuggerItemType {
	NORMAL,
	WARNING,
	ERROR
}
/// <summary>
/// Debugger item to be stored in the logs list.
/// </summary>
public class DebuggerItem {
	/// <summary>
	/// The log's text.
	/// </summary>
	public string m_Text;
	/// <summary>
	/// The log's color.
	/// </summary>
	public Color m_Color;
	/// <summary>
	/// The log's type.
	/// </summary>
	public DebuggerItemType m_Type;
	/// <summary>
	/// Initializes a new instance of the <see cref="DebuggerItem"/> class.
	/// </summary>
	/// <param name="text">Text.</param>
	/// <param name="type">Type.</param>
	public DebuggerItem(string text, DebuggerItemType type) {
		Color color;
		switch (type) {
			default:
			case DebuggerItemType.NORMAL:
				color = Color.white;
				break;
			case DebuggerItemType.WARNING:
				color = Color.yellow;
				break;
			case DebuggerItemType.ERROR:
				color = Color.red;
				break;
		}
		m_Text = text;
		if (color.Equals(null))
			color = Color.white;
		m_Color = color;
		m_Type = type;
	}
}
