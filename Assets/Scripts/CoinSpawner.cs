using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int[] coinCounts = { 10, 15, 20 };
    public float spacing = 0.5f;
    public string axis = "X";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCoins()
    {
        int coinCount = coinCounts[Random.Range(0, coinCounts.Length)];
        Vector3 position = transform.position;
        Quaternion coinRotation = Quaternion.identity;
            
        if (axis.ToLower() == "Z")
        {
            coinRotation = Quaternion.Euler(0, 90, 0);
        }
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, position, coinRotation);
            if (axis.ToUpper() == "X")
            {
                position.x += spacing;
            }
            else
            {
                position.z += spacing;
            }
        }
    }
}
