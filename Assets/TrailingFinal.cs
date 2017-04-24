using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailingFinal : MonoBehaviour
{

	public Transform pointToView;
	public Vector3 cylindricalCoordinate;

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3 (cylindricalCoordinate.x * Mathf.Cos (cylindricalCoordinate.y), cylindricalCoordinate.z, cylindricalCoordinate.x * Mathf.Sin (cylindricalCoordinate.y));
		transform.LookAt (pointToView.position);
	}
}
