using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip[] Background;

    private int currentTrackIndex = 0;
    private float timer = 0f;

    private void Start()
    {
        ShuffleArray(Background);
        PlayCurrentTrack();
    }

    private void Update()
    {

        if (!musicSource.isPlaying)
        {
            timer += Time.deltaTime;
            if (timer > 10f)
            {

                PlayNextTrack();
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