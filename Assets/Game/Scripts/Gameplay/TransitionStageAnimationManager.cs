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

	void Awake ()
	{
		_skyboxMaterial.SetFloat ("_DayFactor", 0f);
	}

	void Start ()
	{
		ActionsManager.OnStageFinished += OnStageFinished;
    MonumentSmartObject.OnEndGame += EndGameTransition;
	}

  private void OnDestroy ()
  {
    ActionsManager.OnStageFinished -= OnStageFinished;
    MonumentSmartObject.OnEndGame -= EndGameTransition;
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

		while (time < _timeAnimation / 2f) {

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


	private IEnumerator FinalAnimationCoroutine ()
	{
    yield return new WaitForSeconds(2.0f);
		float time = 0f;
		Vector3 _one = Vector3.one;
		Quaternion _id = _rotateElement.localRotation;
		Quaternion _anotherId = Quaternion.identity;
		Vector3 _aux;

		while (time < _timeAnimation) {

			_aux = _one * _scaleAnimationFinalCurve.Evaluate (time / _timeAnimation);
			_playerOriginal.localScale = 0.2f * _aux;

			_scaleSkyboxElements [0].localScale = _aux;
			_aux = _one * (1f - _scaleAnimationFinalCurve.Evaluate (time / _timeAnimation));
			_scaleSkyboxElements [1].localScale = _aux;
			_playerFinal.localScale = _aux;


			_aux.x = 0f;
			_aux.y = 0f;
			_aux.z = 360f * _rotateAnimationFinalCurve.Evaluate (time / _timeAnimation);

			_rotateElement.localRotation = _id;
			_rotateElement.Rotate (_turnNumber * _aux, Space.Self);

			_skyboxMaterial.SetFloat ("_DayFactor", _factorSkyboxAnimationFinalCurve.Evaluate (time / _timeAnimation));

			_aux.z = 180f * _rotateSkyboxAnimationFinalCurve.Evaluate (time / _timeAnimation);
			_rotateSkyboxElement.localRotation = _anotherId;
			_rotateSkyboxElement.Rotate (-_aux, Space.Self);


			time += Time.deltaTime;
			yield return null;

		}
			

		_scaleSkyboxElements [0].localScale = _one - _one;
		_aux = _one;
		_scaleSkyboxElements [1].localScale = _aux;

		_rotateElement.rotation = _id;
		_rotateSkyboxElement.localRotation = Quaternion.Euler (new Vector3 (0f, 0f, 180f));

		_skyboxMaterial.SetFloat ("_DayFactor", 1f);

	}


	private IEnumerator FinalAnimationTrailingCoroutine ()
	{
    yield return new WaitForSeconds(2.0f);
    float time = 0f;
		while (time < 7f * _timeAnimation) {

			_trailingFinal.cylindricalCoordinate.y = Mathf.Lerp (2.2f, 5.5f, _animationTrailingFinalCurve.Evaluate (time / (7f * _timeAnimation)));
			time += Time.deltaTime;
			yield return null;
		}
    yield return new WaitForSeconds(10.0f);
    GUIGameplayManager.Instance.BackToMainMenu();
	}

  public void EndGameTransition()
  {
    GUIGameplayManager.Instance.HideGUI();
    StartCoroutine(FinalAnimationTrailingCoroutine());
    StartCoroutine(FinalAnimationCoroutine());
  }


	[SerializeField]
	private List<Transform> _scaleElements;

	[SerializeField]
	private Transform _rotateElement;

	[SerializeField]
	private Transform _rotateSkyboxElement;

	[SerializeField]
	private Transform[] _scaleSkyboxElements;

	[Header ("Animation Stage")]
	[SerializeField]
	private AnimationCurve _scaleAnimationCurve;
	[SerializeField]
	private AnimationCurve _rotateAnimationCurve;
	[SerializeField]
	private AnimationCurve _rotateSkyboxAnimationCurve;
	[SerializeField]
	private AnimationCurve _factorSkyboxAnimationCurve;

	[Header ("Animation Final")]
	[SerializeField]
	private AnimationCurve _scaleAnimationFinalCurve;
	[SerializeField]
	private AnimationCurve _rotateSkyboxAnimationFinalCurve;
	[SerializeField]
	private AnimationCurve _rotateAnimationFinalCurve;
	[SerializeField]
	private AnimationCurve _factorSkyboxAnimationFinalCurve;
	[SerializeField]
	private AnimationCurve _animationTrailingFinalCurve;

	[SerializeField]
	private Transform _playerOriginal;
	[SerializeField]
	private Transform _playerFinal;

	[SerializeField]
	private float _timeAnimation;
	[SerializeField]
	private int _turnNumber;

	[SerializeField]
	private TrailingFinal _trailingFinal;

	[SerializeField]
	private Material _skyboxMaterial;

  [SerializeField]
  private GameObject GameUI;

}
