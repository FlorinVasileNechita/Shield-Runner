using UnityEngine;
using UnityEditor;
/// <summary>
/// Osom Debugger class for the Menu.
/// </summary>
public class DebuggerMenu : MonoBehaviour {
	private static string m_Name = "ODU";

	[MenuItem("ODU/Add")]
	/// <summary>
	/// Adds a new instance of ODU when
	/// there's none in the scene. Otherwise, selects the
	/// instance in the scene for selecting options.
	/// </summary>
	static void Add () {
		GameObject obj = GameObject.Find(m_Name);
		if (obj == null) {
			GameObject prefab = Resources.Load(m_Name, typeof(GameObject)) as GameObject;
			obj = Instantiate(prefab) as GameObject;
			obj.name = m_Name;
		}
		Selection.activeObject = obj;
	}
}
