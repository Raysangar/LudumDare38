﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionStageAnimationManager : MonoBehaviour
{

	public static System.Action OnAnimationStart = delegate {		
	};

	public static System.Action OnHalfAnimationDone = delegate {
	};

	public static System.Action OnAnimationDone = delegate {
	};

	void Start ()
	{
		ActionsManager.OnStageFinished -= OnStageFinished;
		ActionsManager.OnStageFinished += OnStageFinished;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			StopCoroutine (AnimationCoroutine ());
			StartCoroutine (AnimationCoroutine ());
		}
	}

	private void OnStageFinished (ActionsManager.Stage s)
	{
		StopCoroutine (AnimationCoroutine ());
		StartCoroutine (AnimationCoroutine ());
	}

	private IEnumerator AnimationCoroutine ()
	{

		OnAnimationStart ();

		float time = 0f;
		Vector3 _one = Vector3.one;
		Quaternion _id = _rotateElement.localRotation;
		Vector3 _aux;

		while (time < _timeAnimation / 2f) {

			_aux = _one * _scaleAnimationCurve.Evaluate (time / _timeAnimation);
			for (int i = 0; i < _scaleElements.Count; ++i) {
				_scaleElements [i].localScale = _aux;
			}

			_aux.x = 0f;
			_aux.y = 0f;
			_aux.z = 360f * _rotateAnimationCurve.Evaluate (time / _timeAnimation);

			_rotateElement.localRotation = _id;
			_rotateElement.Rotate (_turnNumber * _aux, Space.Self);


			time += Time.deltaTime;
			yield return null;

		}
			
		OnHalfAnimationDone ();

		while (time < _timeAnimation) {

			_aux = _one * _scaleAnimationCurve.Evaluate (time / _timeAnimation);
			for (int i = 0; i < _scaleElements.Count; ++i) {
				_scaleElements [i].localScale = _aux;
			}

			_aux.x = 0f;
			_aux.y = 0f;
			_aux.z = 360f * _rotateAnimationCurve.Evaluate (time / _timeAnimation);

			_rotateElement.rotation = _id;
			_rotateElement.Rotate (_turnNumber * _aux);


			time += Time.deltaTime;
			yield return null;

		}

		for (int i = 0; i < _scaleElements.Count; ++i) {
			_scaleElements [i].localScale = _one;
		}
		_rotateElement.rotation = _id;

		OnAnimationDone ();
	}

	[SerializeField]
	private List<Transform> _scaleElements;

	[SerializeField]
	private Transform _rotateElement;

	[SerializeField]
	private AnimationCurve _scaleAnimationCurve;

	[SerializeField]
	private AnimationCurve _rotateAnimationCurve;

	[SerializeField]
	private float _timeAnimation;
	[SerializeField]
	private int _turnNumber;

}