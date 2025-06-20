using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Animator animator;
    public AudioSource audioSource;
    public AudioClip clipCollide;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier") && playerController.IsPlayerAlive)
        {
            Debug.Log("Body collision with barrier");
            animator.SetTrigger("hit_body");
            audioSource.PlayOneShot(clipCollide);
            playerController.Dead();
        }
    }
}