using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDistorsion : MonoBehaviour
{

	void OnRenderImage (RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit (src, dst, _distorsionMaterial);
	}

	[SerializeField]
	private Material _distorsionMaterial;
}
