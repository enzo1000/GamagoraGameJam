using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip BoutonMenuSFX;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBoutonMenuSFX()
    {
        audioSource.clip = BoutonMenuSFX;
        audioSource.Play();
    }
}
