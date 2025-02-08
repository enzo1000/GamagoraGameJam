using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;

    [Header("Menu Musique")]
    public AudioClip MenuMusique;

    [Header("Menu SFX")]
    public AudioClip BoutonMenuSFX;

    [Header("Quand on casse les yeux")]
    public AudioClip ExorcistSFX1;
    public AudioClip ExorcistSFX2;
    public AudioClip ExorcistSFX3;

    [Header("SFX First Enviro")]
    public AudioClip StepsSFX;
    public AudioClip TockingSFX;
    public AudioClip GrincementSFX;

    [Header("All SFX")]
    public AudioClip WhispeingSFX;
    public AudioClip BreathingSFX;
    public AudioClip HeartBeatingSFX;

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

    private void Start()
    {
        PlayMenuMusique();
    }

    public void PlayMenuMusique()
    {
        audioSource.clip = MenuMusique;
        audioSource.Play();
    }

    public void PlayBoutonMenuSFX()
    {
        audioSource.PlayOneShot(BoutonMenuSFX);
    }

    public void PlayExorcistSFX()
    {
        int rand = Mathf.FloorToInt(Random.Range(0, 3));
        if (rand == 0)
        {
            audioSource.PlayOneShot(ExorcistSFX1);
        } else if (rand == 1)
        {
            audioSource.PlayOneShot(ExorcistSFX2);
        } else
        {
            audioSource.PlayOneShot(ExorcistSFX3);
        }
    }
}
