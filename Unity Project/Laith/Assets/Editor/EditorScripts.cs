using UnityEngine;
using UnityEditor;
using System.Collections;

class EditorScrips : EditorWindow
{
	
	[MenuItem("Play/PlayMe _%h")]
	public static void RunMainScene()
	{
		if (EditorApplication.isPlaying)
			return;

		EditorApplication.SaveScene ();
		EditorApplication.OpenScene("Assets/Scenes/MainMenu.unity");
		EditorApplication.isPlaying = true;
	}
}