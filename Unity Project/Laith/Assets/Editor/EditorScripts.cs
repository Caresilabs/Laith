﻿using UnityEngine;
using UnityEditor;
using System.Collections;

class EditorScrips : EditorWindow
{
	
	[MenuItem("Play/PlayMe _%h")]
	public static void RunMainScene()
	{
		EditorApplication.SaveScene ();
		EditorApplication.OpenScene("Assets/Scenes/StartUp.unity");
		EditorApplication.isPlaying = true;
	}
}