using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	[System.Serializable]
	public struct AudioStruct
	{
		public AudioType audioType;
		public AudioSource[] audioClips;
	}

	public enum AudioType
	{
		Default,
		Main,
		Gameover,
		Wood,
		Drink,
		Eat,
		Orchard,
		Sheep,
		TakeWater,
		Wrong,
		Build,
		UIButton,
		Monument,
		Bark
	}


	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		PlaySound (AudioType.Main);

		PlayerManager.OnPlayerDrink -= OnPlayerDrink;
		PlayerManager.OnPlayerEat -= OnPlayerEat;
		SmartObject.OnPlayerInteraction -= OnPlayerInteraction;
		SmartObjectsManager.OnPlayerUpgradeMonument -= OnPlayerUpgradeMonument;
		SmartObjectsManager.OnPlayerUpgradeHouse -= OnPlayerUpgradeHouse;
		SmartObjectsManager.OnPlayerIrrigate -= OnPlayerIrrigate;
		SmartObjectsManager.OnPlayerBuildRanch -= OnPlayerBuildRanch;
		SmartObjectsManager.OnPlayerBuildGarden -= OnPlayerBuildGarden;
    ActionsManager.OnStageFinished -= OnStageFinished;

    PlayerManager.OnPlayerDrink += OnPlayerDrink;
		PlayerManager.OnPlayerEat += OnPlayerEat;
		SmartObject.OnPlayerInteraction += OnPlayerInteraction;
		SmartObjectsManager.OnPlayerUpgradeMonument += OnPlayerUpgradeMonument;
		SmartObjectsManager.OnPlayerUpgradeHouse += OnPlayerUpgradeHouse;
		SmartObjectsManager.OnPlayerIrrigate += OnPlayerIrrigate;
		SmartObjectsManager.OnPlayerBuildRanch += OnPlayerBuildRanch;
		SmartObjectsManager.OnPlayerBuildGarden += OnPlayerBuildGarden;
    ActionsManager.OnStageFinished += OnStageFinished;


	}

	private void PlaySound (AudioType audioType, bool loop = false)
	{
		if (!_audios [(int)audioType].audioClips [0].isPlaying) {
			_audios [(int)audioType].audioClips [0].loop = loop;
			_audios [(int)audioType].audioClips [0].Play ();	
		}			
	}

	private void OnPlayerDrink ()
	{
		PlaySound (AudioType.Drink);
	}

	private void OnPlayerEat ()
	{
		PlaySound (AudioType.Eat);
	}

	private void OnPlayerUpgradeMonument ()
	{
		PlaySound (AudioType.Monument);
	}

	private void OnPlayerUpgradeHouse ()
	{
		PlaySound (AudioType.Build);
	}

	private void OnPlayerBuildRanch ()
	{
		PlaySound (AudioType.Build);
	}

	private void OnPlayerBuildGarden ()
	{
		PlaySound (AudioType.Build);
	}

	private void OnPlayerIrrigate ()
	{
		PlaySound (AudioType.TakeWater);
	}

	private void OnPlayerInteraction (SmartObject so)
	{
		switch (so.Type) {
		case SmartObject.ObjectType.Wood:
			PlaySound (AudioType.Wood);
			break;
		case SmartObject.ObjectType.Ranch:
			PlaySound (AudioType.Sheep);
			break;
		case SmartObject.ObjectType.Garden:
			PlaySound (AudioType.Orchard);
			break;
		case SmartObject.ObjectType.Water:
			PlaySound (AudioType.TakeWater);
			break;
		}
	}

  private void OnStageFinished (ActionsManager.Stage stage)
  {
    if (GameController.Instance.HasStageFailed(stage))
    {
      PlaySound (AudioType.Wrong);
    }
  }


	[SerializeField]
	private AudioStruct[] _audios;

}
