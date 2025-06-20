using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--music--")]
    public AudioClip[] Background;

    [Header("--sfx--")]
    public AudioClip MineSound;
    public AudioClip RandomCaveSound;
    public AudioClip Walking;

    private int currentTrackIndex = 0;
    private float timer = 0f;
    
    //function other scripts use for playing refrencing audio
    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }

    //shuffle array & play music
    private void Start()
    {
        ShuffleArray(Background);
        PlayCurrentTrack();

    }

    //if no music playing > next track
    private void Update()
    {

        if (!musicSource.isPlaying)
        {
            timer += Time.deltaTime;
            
            //random cave sound
            if (timer == 2.22f)
            {
                SFXSource.clip = RandomCaveSound;
                SFXSource.Play();

            }

            if (timer > 10f)
            {

                PlayNextTrack();
                timer = 0f;
            }
        }
    }

    private void PlayCurrentTrack()
    {
        if (Background.Length == 0) return;

        musicSource.clip = Background[currentTrackIndex];
        musicSource.Play();
    }

    private void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % Background.Length;
        PlayCurrentTrack();
    }

    void ShuffleArray(AudioClip[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            AudioClip temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}