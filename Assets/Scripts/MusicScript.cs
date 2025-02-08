using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    [Header("Menu SFX")]
    public AudioClip BoutonMenuSFX;

    [Header("Quand on casse les yeux")]
    public AudioClip ExorcistSFX1;
    public AudioClip ExorcistSFX2;
    public AudioClip ExorcistSFX3;

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

    public void PlayExorcistSFX()
    {
        int rand = Mathf.FloorToInt(Random.Range(0, 3));
        if (rand == 0)
        {
            audioSource.clip = ExorcistSFX1;
        } else if (rand == 1)
        {
            audioSource.clip = ExorcistSFX2;
        } else
        {
            audioSource.clip = ExorcistSFX3;
        }
        audioSource.Play();
    }
}
