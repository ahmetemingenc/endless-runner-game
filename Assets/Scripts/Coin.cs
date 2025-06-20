using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float collectionRadius = 2.5f;
    public int coinValue = 10;
    public float animationDuration = 0.1f;
    public float curveHeight = 0.1f;
    
    private GameObject backpack;
    private bool isAnimating = false;

    AudioSource audioSource;
    public AudioClip coinSound;

    public Score score;
    
    void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("Backpack");
        GameObject scoreGameObject = GameObject.FindGameObjectWithTag("Score");
        score = scoreGameObject.GetComponent<Score>();
        audioSource = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        if (!isAnimating && backpack !=null && Vector3.Distance(transform.position, backpack.transform.position) <= collectionRadius)
        {
          audioSource.PlayOneShot(coinSound, Random.Range(0.3f, 0.6f));  
          StartCoroutine(CollectCoinAnimation());
        }
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    private void CollectCoin()
    {
        score.CollectCoin(coinValue);
        Destroy(gameObject);
    }

    private IEnumerator CollectCoinAnimation()
    {
        isAnimating = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = backpack.transform.position;
        
        Vector3 midPos = (startPos + endPos) / 2;
        midPos.y += curveHeight;
        
        float elaspedTime = 0;
        while (elaspedTime < animationDuration)
        {
            elaspedTime += Time.deltaTime;
            float t = elaspedTime / animationDuration;
            
            transform.position = CalculateBezierPoint(t, startPos, midPos, endPos);
            yield return null;
        }
        CollectCoin();
    }
}
