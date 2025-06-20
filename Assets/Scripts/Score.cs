using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    public Transform player;
    public Text scoreText;
    public Text highScoreText;
    public float scoreMultiplier = 1.0f;
    public int timeBonusInterval = 10;
    public int timeBousPoints = 50;
    AudioSource audioSource;
    public AudioClip newHighScoreSound;
    public AudioClip timeBonusSound;
    
    float score = 0;
    int lastHighScore = 0;
    
    PlayerController playerController;
    bool newHighScore = false;
    string highScoreFile;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        highScoreFile = Path.Combine(Application.persistentDataPath, "endlessRunnerHighScore.txt");
        playerController = player.GetComponent<PlayerController>();
        
        highScoreText.text = LoadHighScore().ToString();
        StartCoroutine(Coroutine_TimeBonusScore(timeBonusInterval));
    }

    public int LoadHighScore()
    {
        if (File.Exists(highScoreFile))
        {
            string savedHighScore = File.ReadAllText(highScoreFile);
            if (int.TryParse(savedHighScore, out int highScore))
            {
                lastHighScore = highScore;
                return highScore;
            }
        }

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController == null) return;
        if (!playerController.IsPlayerAlive) return;
        
        score += Time.deltaTime * scoreMultiplier;
        scoreMultiplier += 0.001f;
        
        if (score > 0.0f)
        {
            scoreText.text = Mathf.FloorToInt(score).ToString();
        }
        
        if(!newHighScore && score > lastHighScore)
        {
            newHighScore = true;
            audioSource.PlayOneShot(newHighScoreSound);
        }
    }

    IEnumerator Coroutine_TimeBonusScore(int interval)
    {
        while (playerController.IsPlayerAlive)
        {
          yield return new WaitForSeconds(interval);  
          score += timeBousPoints;
          audioSource.PlayOneShot(timeBonusSound);
        }
    }

    public void SaveHighScore()
    {
        if (newHighScore)
        {
            File.WriteAllText(highScoreFile, Mathf.FloorToInt(score).ToString());
        }
    }

    public void CollectCoin(int coinScore)
    {
        score += coinScore;
    }
}
