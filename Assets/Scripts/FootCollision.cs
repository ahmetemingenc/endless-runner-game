  using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip clipCollide;
    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && playerController.IsPlayerAlive)
        {
            Debug.Log("Foot collision with obstacle");
            animator.SetTrigger("hit_foot");
            audioSource.PlayOneShot(clipCollide);
            playerController.Dead();
        }
    }
}