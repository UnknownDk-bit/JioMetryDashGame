using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("AudioSources")]
    public AudioSource bgMusicSource;
    public AudioSource sfxSource;


    [Header("AudioClips")]
    public AudioClip backgroundMusic;
    public AudioClip coinCollectClip;
    public AudioClip redCoinCollectClip;
    public AudioClip dieClip;
    public AudioClip jumpClip;
    public AudioClip gameStartClip;
    public AudioClip buttonPressClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        bgMusicSource.clip = backgroundMusic;
        bgMusicSource.loop = true;
        bgMusicSource.playOnAwake = false;

        if (!bgMusicSource.isPlaying)
        {
            bgMusicSource.Play();
        }
    }

    void Start()
    {
        PlayGameStart(); 
    }

    public void PlayBGMusic()
    {
        if (!bgMusicSource.isPlaying)
        {
            bgMusicSource.Play();
        }
    }

    public void PlayCoinCollect()
    {
        sfxSource.PlayOneShot(coinCollectClip);
    }

    public void PlayRedCoinCollect()
    {
        sfxSource.PlayOneShot(redCoinCollectClip);
    }

    public void PlayDie()
    {
        sfxSource.PlayOneShot(dieClip);
    }

    public void PlayJump()
    {
        sfxSource.PlayOneShot(jumpClip);
    }

    public void PlayGameStart()
    {
        sfxSource.PlayOneShot(gameStartClip);
    }

    public void PlayButtonPress()
    {
        sfxSource.PlayOneShot(buttonPressClip);
    }
}
