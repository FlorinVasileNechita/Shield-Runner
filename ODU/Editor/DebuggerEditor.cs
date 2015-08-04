using UnityEditor;
using System.Collections;

/// <summary>
/// Debugger object Inspector editor.
/// </summary>
[CustomEditor(typeof(Debugger))]
public class DebuggerEditor : Editor {
	public override void OnInspectorGUI () {
		Debugger debugger = (Debugger)target;
		float prevHeight = debugger.m_Height;
		bool prevGuiEnabled = debugger.m_GuiEnabled;
		bool prevJsEnabled = debugger.m_JsEnabled;
		debugger.m_GuiEnabled = EditorGUILayout.Toggle("Gui Enabled", debugger.m_GuiEnabled);
		debugger.m_JsEnabled = EditorGUILayout.Toggle("Js Enabled", debugger.m_JsEnabled);
		debugger.m_Height = EditorGUILayout.Slider("Height", debugger.m_Height, 0.1f, 1.0f);
		// Repaint the GameView ONLY if some value has been updated
		if (!prevHeight.Equals(debugger.m_Height) || !prevGuiEnabled.Equals(debugger.m_GuiEnabled) || !prevJsEnabled.Equals(debugger.m_JsEnabled)) {
			System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
			System.Type type = assembly.GetType( "UnityEditor.GameView" );
			EditorWindow gameview = EditorWindow.GetWindow(type);
			gameview.Repaint();
		}
	}
}
