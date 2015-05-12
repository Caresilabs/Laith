using UnityEngine;
using UnityEditor;
using System.Collections;

class EditorScrips : EditorWindow
{
	
	[MenuItem("Play/PlayMe _%h")]
	public static void RunMainScene()
	{
		EditorApplication.SaveScene ();
		EditorApplication.OpenScene("Assets/Scenes/MainMenu.unity");
		EditorApplication.isPlaying = true;
	}
}