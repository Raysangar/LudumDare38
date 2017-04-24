using UnityEngine;

public class Twitter : MonoBehaviour {
  private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
  private const string TWEET_LANGUAGE = "en";
  public static string descriptionParam;
  private static string appStoreLink = "https://raysangar.github.io/SurviBall/";
  private string hashtag = "#surviball";

  public void ShareToTW(string linkParameter)
  {
    string nameParameter = "I have discovered the truth in " + ActionsManager.Instance.ActionsMadeByPlayerOnPreviousStages.Count + ". Can you unveil the secrets of the island? ";
    Application.OpenURL(TWITTER_ADDRESS +
       "?text=" + WWW.EscapeURL(nameParameter + "\n" + descriptionParam + "\n" + "Get the Game:\n" + appStoreLink + " " + hashtag));
  }
}
