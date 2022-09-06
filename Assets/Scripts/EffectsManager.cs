using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectsManager : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem finishEffect;
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private ParticleSystem dustTrail;
    [SerializeField] private float delayTimer = 1f;

    [Header("Audio")] 
    [SerializeField] private AudioClip finishClip;
    [SerializeField] private AudioClip crashClip;
    
  
    private CircleCollider2D headCollider;
    private AudioSource audioSource;

    private bool hasCrashed = false;
    

    private void Start()
    {
        headCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            finishEffect.Play();
            audioSource.PlayOneShot(finishClip);
            Invoke(nameof(ReloadScene), delayTimer);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && headCollider.IsTouching(other.collider) && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffect.Play();
            audioSource.PlayOneShot(crashClip);
            Invoke(nameof(ReloadScene), delayTimer);
            
        }

        if (other.gameObject.CompareTag("Ground") && !headCollider.IsTouching(other.collider))
        {
            dustTrail.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            dustTrail.Stop();
        }
    }


    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    
}
