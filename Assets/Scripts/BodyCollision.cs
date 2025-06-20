using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip clipCollide;
    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && playerController.IsPlayerAlive)
        {
            Debug.Log("Body collision with obstacle");
            animator.SetTrigger("hit_body");
            audioSource.PlayOneShot(clipCollide);
            playerController.Dead();
        }
    }
}