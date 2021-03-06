﻿using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour 
{
	private Animator anim;
	private AnimationClip clip;

	private void Start()
	{
		anim = GetComponent<Animator> ();
		clip = this.anim.runtimeAnimatorController.animationClips [0];
		StartCoroutine ("checkLifetime");
	}

	IEnumerator checkLifetime()
	{
		yield return new WaitForSeconds (clip.length);
		Destroy (gameObject);
	}

}
