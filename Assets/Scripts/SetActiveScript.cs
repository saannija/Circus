using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveScript : MonoBehaviour
{
    public GameObject buttonGroup;
    public GameObject characterSelection;
    public GameObject settingsButtons;
    public GameObject leaderboardButtons;

    //public GameObject gameObject2;
    //public void ToggleActiveAfterDelay(float delay)
    //{
    //    StartCoroutine(ToggleActiveCoroutine(delay));
    //}
    //private IEnumerator ToggleActiveCoroutine(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    gameObject2.SetActive(!gameObject2.activeSelf);
    //    gameObject.SetActive(!gameObject.activeSelf);
    //}
    public void OpenCharacterSelection()
    {
        buttonGroup.SetActive(false);
        characterSelection.SetActive(true);
    }

    public void OpenSettings()
    {
        buttonGroup.SetActive(false);
        settingsButtons.SetActive(true);
    }

    public void OpenLeaderboard()
    {
        buttonGroup.SetActive(false);
        leaderboardButtons.SetActive(true);
    }

    public void CloseCharacterSelection()
    {
        buttonGroup.SetActive(true);
        characterSelection.SetActive(false);
    }

    public void CloseSettings()
    {
        buttonGroup.SetActive(true);
        settingsButtons.SetActive(false);
    }

    public void CloseLeaderboard()
    {
        buttonGroup.SetActive(true);
        leaderboardButtons.SetActive(false);
    }

}
