﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOutline : MonoBehaviour
{

	void Awake ()
	{
		_ownCamera = GetComponent<Camera> ();
		_outlineTexture = new RenderTexture (Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		_ownCamera.targetTexture = _outlineTexture;
	}


	[SerializeField]
	private Camera _ownCamera;

	private RenderTexture _outlineTexture;
}