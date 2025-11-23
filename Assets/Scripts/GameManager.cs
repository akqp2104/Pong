using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private struct PlayerInfo
    {
        public string name;
        public int score;
        public int roundsWon;

        public void ResetInfo()
        {
            score = 0;
            roundsWon = 0;
        }
    }

    [Header("Paddles and ball")]
    [SerializeField] private GameObject paddle1;
    [SerializeField] private GameObject paddle2;
    [SerializeField] private GameObject paddleAI;
    [SerializeField] private GameObject ball;

    [Header("Sound effects")]
    [SerializeField] private AudioManager audioManager;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI player1ScoreTMP;
    [SerializeField] private TextMeshProUGUI player2ScoreTMP;

    [Header("Round")]
    [SerializeField] private TextMeshProUGUI roundTMP;

    [Header("Winner")]
    [SerializeField] private TextMeshProUGUI winnerTMP;

    [Header ("Options")]
    [SerializeField] private GameObject playAgain;
    [SerializeField] private GameObject exit;

    private PlayerInfo player1;
    private PlayerInfo player2;
    private int currentRound;
    private int pointsToWin;
    private int maxRounds;

    private static GameManager instance;

    private void Awake()
    {
        Time.timeScale = 1f;

        if (instance != null)
        {
            Debug.Log("Found more than one GameManager in the scene");
        }
        instance = this;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    private IEnumerator Start()
    {
        player1 = new PlayerInfo();
        player2 = new PlayerInfo();

        int mode = PlayerPrefs.GetInt("mode");
        if (mode == 0)
        {
            player2.name = "AI";
            paddle2.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetString("player2") == "")
                player2.name = "Player 2";
            else
                player2.name = PlayerPrefs.GetString("player2");

            paddleAI.SetActive(false);
        }

        if (PlayerPrefs.GetString("player1") == "")
            player1.name = "Player 1";
        else
            player1.name = PlayerPrefs.GetString("player1");

        currentRound = 0;
        pointsToWin = PlayerPrefs.GetInt("points");
        maxRounds = PlayerPrefs.GetInt("rounds");

        if (audioManager != null)
        {
            audioManager.PlaySFX(0);
        }

        roundTMP.text = "Round " + (currentRound + 1).ToString();
        roundTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        roundTMP.gameObject.SetActive(false);

        Debug.Log("Player 1: " + player1.name);
        Debug.Log("Points to win: " + pointsToWin);
        Debug.Log("Max rounds: " + maxRounds);
    }

    public void AddScore(int player)
    {
        if (player == 1)
        {
            player1.score++;
            player1ScoreTMP.text = player1.score.ToString();
        }
        else
        {
            player2.score++;
            player2ScoreTMP.text = player2.score.ToString();
        }

        StartCoroutine(CheckScores());
    }

    private IEnumerator CheckScores()
    {
        if (player1.score == pointsToWin || player2.score == pointsToWin)
        {
            if (player1.score == pointsToWin)
                player1.roundsWon++;
            else
                player2.roundsWon++;

            currentRound++;
            player1.score = 0;
            player2.score = 0;

            if (getWinner() == 0)
            {
                if (audioManager != null)
                {
                    audioManager.PlaySFX(0);
                }

                roundTMP.text = "Round " + (currentRound+1).ToString();
                roundTMP.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f);
                roundTMP.gameObject.SetActive(false);
                ResetPositions();
            }
            else
            {
                PauseGame();
                if (audioManager != null)
                {
                    audioManager.PlaySFX(1);
                }

                if (getWinner() == 1)
                {
                    winnerTMP.text = player1.name + " wins";
                    winnerTMP.rectTransform.anchoredPosition = new Vector2(-375, 0);
                }
                else if (getWinner() == 2)
                {
                    winnerTMP.text = player2.name + " wins";
                    winnerTMP.rectTransform.anchoredPosition = new Vector2(375, 0);
                }
                winnerTMP.gameObject.SetActive(true);
                playAgain.SetActive(true);
                exit.SetActive(true);
            }
        }
        else 
            ResetPositions();
    }

    private int getWinner()
    {
        if (player1.roundsWon > maxRounds / 2)
        {
            return 1;
        }
        else if (player2.roundsWon > maxRounds / 2)
        {
            return 2;
        }
        else
            return 0;
    }

    public void ResetPositions()
    {
        paddle1.transform.position = new Vector2(-15, 0);
        paddle2.transform.position = new Vector2(15, 0);
        paddleAI.transform.position = new Vector2(15, 0);
        ball.transform.position = Vector2.zero;

        paddleAI.GetComponent<AI>().ResetSpeed();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
