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
    public AudioClip GrincementSFX;

    [Header("SFX Second Enviro")]
    public AudioClip GougoutteSFX;

    [Header("SFX Third Enviro")]
    public AudioClip WindSFX;
    public AudioClip GrillonSFX;

    [Header("SFX First & Second Enviro")]
    public AudioClip OpeningDoorSFX;
    public AudioClip ClosingDoorSFX;
    public AudioClip TockingRightSFX;
    public AudioClip TockingLeftSFX;
    public AudioClip KnockingDoorSFX;

    [Header("All Enviro SFX")]
    public AudioClip WhisperingSFX;
    public AudioClip BreathingSFX;
    public AudioClip HeartBeatingSFX;

    [Header("Gameplay SFX")]
    public AudioClip UsingCraySFX;
    public AudioClip BreakingCraySFX;
    public AudioClip LifeDraynSFX;
    public AudioClip DefeateSFX;

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
        audioSource.clip = BoutonMenuSFX;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayExorcistSFX()
    {
        int rand = Mathf.FloorToInt(Random.Range(0, 3));
        if (rand == 0)
        {
            audioSource.PlayOneShot(ExorcistSFX1);
        }
        else if (rand == 1)
        {
            audioSource.PlayOneShot(ExorcistSFX2);
        }
        else
        {
            audioSource.PlayOneShot(ExorcistSFX3);
        }
    }

    public void PlayRandomAudioClipFirstEnviro()
    {
        int rand = Mathf.FloorToInt(Random.Range(0, 7));
        if (rand == 0)
        {
            audioSource.PlayOneShot(OpeningDoorSFX);
        }
        else if (rand == 1)
        {
            audioSource.PlayOneShot(StepsSFX);
        }
        else if (rand == 2)
        {
            audioSource.PlayOneShot(GrincementSFX);
        }
        else if (rand == 3)
        {
            audioSource.PlayOneShot(ClosingDoorSFX);
        }
        else if (rand == 4)
        {
            audioSource.PlayOneShot(TockingRightSFX);
        }
        else if (rand == 5)
        {
            audioSource.PlayOneShot(TockingLeftSFX);
        }
        else if (rand == 6)
        {
            audioSource.PlayOneShot(KnockingDoorSFX);
        }
    }
    public void PlayRandomAudioClipSecondEnviro()
    {
        int rand = Mathf.FloorToInt(Random.Range(0, 6));
        if (rand == 0)
        {
            audioSource.PlayOneShot(OpeningDoorSFX);
        }
        else if (rand == 1)
        {
            audioSource.PlayOneShot(GougoutteSFX);
        }
        else if (rand == 2)
        {
            audioSource.PlayOneShot(ClosingDoorSFX);
        }
        else if (rand == 3)
        {
            audioSource.PlayOneShot(TockingRightSFX);
        }
        else if (rand == 4)
        {
            audioSource.PlayOneShot(TockingLeftSFX);
        }
        else if (rand == 5)
        {
            audioSource.PlayOneShot(KnockingDoorSFX);
        }
    }
    public void PlayRandomAudioClipThirdEnviro()
    {
        int rand = Mathf.FloorToInt(Random.Range(0, 5));
        if (rand == 0)
        {
            audioSource.PlayOneShot(WindSFX);
        }
        else if (rand == 1)
        {
            audioSource.PlayOneShot(GrillonSFX);
        }
        else if (rand == 2)
        {
            audioSource.PlayOneShot(WhisperingSFX);
        }
        else if (rand == 3)
        {
            audioSource.PlayOneShot(BreathingSFX);
        }
        else if (rand == 4)
        {
            audioSource.PlayOneShot(HeartBeatingSFX);
        }
    }

}
