using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField player1;
    [SerializeField] private TMP_InputField player2;

    public void SetMode(int mode)
    {
        PlayerPrefs.SetInt("mode", mode);
    }

    public void SetPointsToWin(int points)
    {
        PlayerPrefs.SetInt("points", points);
    }

    public void SetRounds(int rounds)
    {
        PlayerPrefs.SetInt("rounds", rounds);
    }
    public void SetName(int playerIndex)
    {
        if(playerIndex == 1)
            PlayerPrefs.SetString("player1", player1.text);
        else
            PlayerPrefs.SetString("player2", player2.text);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
}
