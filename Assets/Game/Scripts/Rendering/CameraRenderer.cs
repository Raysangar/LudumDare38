using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRenderer : MonoBehaviour
{

	void OnRenderImage (RenderTexture src, RenderTexture dst)
	{
		_outlineMaterial.SetTexture ("_OutlineTexture", _outlineCamera.targetTexture);
		Graphics.Blit (src, dst, _outlineMaterial);
	}

	[SerializeField]
	private Camera _outlineCamera;
	[SerializeField]
	private Material _outlineMaterial;
}
