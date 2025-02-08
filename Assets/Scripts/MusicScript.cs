using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    public AudioClip BoutonMenuSFX;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            audioSource = GetComponent<AudioSource>();
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBoutonMenuSFX()
    {
        audioSource.clip = BoutonMenuSFX;
        audioSource.Play();
    }
}
