﻿using UnityEngine;
using System.Collections;

public class Gate : Triggerable {

	/// <summary>
	/// Moves object up when activated, down when deactivated.
	/// Author: Henrik P.
	/// </summary>

	public bool opening, closing;
	public float distanceMoved;
	float time;
	float timer;

	void Start () {
		time = distanceMoved / 5f;
		timer = time;
	}

	void Update(){
		if (opening) {
			transform.Translate (new Vector3 (0, 5, 0) * Time.deltaTime);
			timer -= Time.deltaTime;
			if (timer <= 0) {
				opening = false;
				timer = time;
			}
		} else if (closing) {
			transform.Translate (new Vector3 (0, -5, 0) * Time.deltaTime);
			timer -= Time.deltaTime;
			if (timer <= 0) {
				closing = false;
				timer = time;
			}
		}
	}

	public override void Activate (){
		if (closing || opening)
			return;
		opening = true;
		base.Activate ();
	}

	public override void Deactivate (){
		if (opening || closing)
			return;
		closing = true;
		base.Deactivate ();
	}

}
