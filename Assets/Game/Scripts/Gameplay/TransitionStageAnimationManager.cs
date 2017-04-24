using System.Collections;
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
		Quaternion _anotherId = Quaternion.identity;
		Vector3 _aux;
		float oriLightInt = _directionalLight.intensity;


		while (time < _timeAnimation / 2f) {

			_directionalLight.intensity = oriLightInt * _scaleAnimationCurve.Evaluate (time / _timeAnimation);
			_aux = _one * _scaleAnimationCurve.Evaluate (time / _timeAnimation);
				
			for (int i = 0; i < _scaleElements.Count; ++i) {
				_scaleElements [i].localScale = _aux;
			}

			_scaleSkyboxElements [0].localScale = _aux;
			_aux = _one * (1f - _scaleAnimationCurve.Evaluate (time / _timeAnimation));
			_scaleSkyboxElements [1].localScale = _aux;


			_aux.x = 0f;
			_aux.y = 0f;
			_aux.z = 360f * _rotateAnimationCurve.Evaluate (time / _timeAnimation);

			_rotateElement.localRotation = _id;
			_rotateElement.Rotate (_turnNumber * _aux, Space.Self);

			_skyboxMaterial.SetFloat ("_DayFactor", _factorSkyboxAnimationCurve.Evaluate (time / _timeAnimation));

			_aux.z = 360f * _rotateSkyboxAnimationCurve.Evaluate (time / _timeAnimation);
			_rotateSkyboxElement.localRotation = _anotherId;
			_rotateSkyboxElement.Rotate (-_aux, Space.Self);


			time += Time.deltaTime;
			yield return null;

		}
			
		OnHalfAnimationDone ();

		while (time < _timeAnimation) {

			_directionalLight.intensity = oriLightInt * _scaleAnimationCurve.Evaluate (time / _timeAnimation);
			_aux = _one * _scaleAnimationCurve.Evaluate (time / _timeAnimation);
			for (int i = 0; i < _scaleElements.Count; ++i) {
				_scaleElements [i].localScale = _aux;
			}

			_scaleSkyboxElements [0].localScale = _aux;
			_aux = _one * (1f - _scaleAnimationCurve.Evaluate (time / _timeAnimation));
			_scaleSkyboxElements [1].localScale = _aux;

			_aux.x = 0f;
			_aux.y = 0f;
			_aux.z = 360f * _rotateAnimationCurve.Evaluate (time / _timeAnimation);

			_rotateElement.rotation = _id;
			_rotateElement.Rotate (_turnNumber * _aux);

			_skyboxMaterial.SetFloat ("_DayFactor", _factorSkyboxAnimationCurve.Evaluate (time / _timeAnimation));

			_aux.z = 360f * _rotateSkyboxAnimationCurve.Evaluate (time / _timeAnimation);
			_rotateSkyboxElement.localRotation = _anotherId;
			_rotateSkyboxElement.Rotate (-_aux, Space.Self);

			time += Time.deltaTime;
			yield return null;

		}

		_directionalLight.intensity = oriLightInt;	
		for (int i = 0; i < _scaleElements.Count; ++i) {
			_scaleElements [i].localScale = _one;
		}

		_scaleSkyboxElements [0].localScale = _one;
		_aux = _one - _one;
		_scaleSkyboxElements [1].localScale = _aux;

		_rotateElement.rotation = _id;
		_rotateSkyboxElement.localRotation = _anotherId;

		_skyboxMaterial.SetFloat ("_DayFactor", 0f);


		OnAnimationDone ();
	}

	[SerializeField]
	private List<Transform> _scaleElements;

	[SerializeField]
	private Transform _rotateElement;

	[SerializeField]
	private Transform _rotateSkyboxElement;

	[SerializeField]
	private Transform[] _scaleSkyboxElements;

	[SerializeField]
	private AnimationCurve _scaleAnimationCurve;

	[SerializeField]
	private AnimationCurve _rotateAnimationCurve;

	[SerializeField]
	private AnimationCurve _rotateSkyboxAnimationCurve;

	[SerializeField]
	private AnimationCurve _factorSkyboxAnimationCurve;

	[SerializeField]
	private Light _directionalLight;

	[SerializeField]
	private float _timeAnimation;
	[SerializeField]
	private int _turnNumber;

	[SerializeField]
	private Material _skyboxMaterial;

}
