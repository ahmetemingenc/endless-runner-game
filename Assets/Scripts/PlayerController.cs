using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator mAnimator;
    
    static public float[] LanePositionsInitial = { -1.6f, 0.9f }; 
    private float[] lanePositions;
    private int currentLane = 1;

    public bool IsPlayerAlive = true;
    public float timeScale = 1.0f;
    
    public AudioSource audioSource;
    public AudioClip slidingAudioClip;
    public AudioClip jumpAudioClip;
    public AudioClip changeLaneAudioClip;

    public Score score;

    void Start()
    {
        Time.timeScale = timeScale;
        
        lanePositions = (float[])LanePositionsInitial.Clone();

        mAnimator = GetComponent<Animator>();

        transform.position = new Vector3(
            lanePositions[currentLane],
            transform.position.y,
            transform.position.z
        );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mAnimator.SetTrigger("jump");
            audioSource.PlayOneShot(jumpAudioClip);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mAnimator.SetTrigger("slide");
            audioSource.PlayOneShot(slidingAudioClip);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            StartCoroutine(ChangeLane(currentLane - 1));
            audioSource.PlayOneShot(changeLaneAudioClip);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < lanePositions.Length - 1)
        {
            StartCoroutine(ChangeLane(currentLane + 1));
            audioSource.PlayOneShot(changeLaneAudioClip);
        }
    }

    private IEnumerator ChangeLane(int targetLane)
    {
        if (targetLane == currentLane)
        {
            yield break;
        }

        currentLane = targetLane;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition;
        targetPosition.x = lanePositions[currentLane];
        targetPosition.z += 2.0f;

        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(
            targetPosition.x,
            startPosition.y,
            targetPosition.z
        );

        Debug.Log($"Şerit değiştirildi: Hedef Şerit = {currentLane}, Yeni Pozisyon = {transform.position}");
    }

    public void Dead()
    {
        score.SaveHighScore();
        IsPlayerAlive = false;
        Invoke("Restart", 2.0f);
    }

    void Restart()
    {
        SceneManager.LoadScene("Runner");
    }
}