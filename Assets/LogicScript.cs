using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int level = 1;
    public GameObject bird;
    public SpawnerScript spawner;
    private System.Array pipeMovers;
    private System.Array ringMovers;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI levelText;
    public GameObject gameOverScreen;
    public AudioSource bgMusic;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerScript>();
        pipeMovers = GameObject.FindGameObjectsWithTag("PipeMover");
        ringMovers = GameObject.FindGameObjectsWithTag("Bonus");

        // Check if multiple monitors exist, if so force client to run on the main screen
        if (Display.displays.Length > 1)
        {
            Display.displays[0].Activate();
        }

        levelText.text = "1";
    }

    private void Update()
    {
        
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        if (bird.GetComponent<BirdScript>().birdIsAlive)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();

            int[] difficulty = {10, 20, 30};
            int[] speed = { 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140 };

            int index = 0;
            foreach (int d in difficulty)
            {
                if (playerScore == d)
                {
                    double newRate = spawner.spawnRate - (spawner.spawnRate * 0.1);
                    spawner.spawnRate = (float)newRate;
                    
                    Debug.Log("Current spawn rate: " + spawner.spawnRate);
                    level += 1;
                    levelText.text = level.ToString();

                    break;
                }
                else
                {
                    if (playerScore > 30)
                    {
                        foreach (int s in speed)
                        {
                            if (playerScore == s)
                            {
                                double newRate = spawner.spawnRate - (spawner.spawnRate * 0.03);
                                spawner.spawnRate = (float)newRate;

                                float speedChange = 1f;

                                PipeMoveScript.staticMoveSpeed += speedChange;
                                foreach (GameObject pipe in pipeMovers)
                                {
                                    pipe.GetComponent<PipeMoveScript>().dynamicMoveSpeed += speedChange;
                                }

                                BonusRingMoveScript.staticMoveSpeed += speedChange;
                                foreach (GameObject ring in ringMovers)
                                {
                                    ring.GetComponent<BonusRingMoveScript>().dynamicMoveSpeed += speedChange;
                                }

                                Debug.Log("Speed increased");
                                level += 1;
                                levelText.text = level.ToString();

                                break;
                            }
                        }
                    }
                }

                index += 1;
            }
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        bgMusic.Stop();
        gameOverScreen.SetActive(true);
    }
}