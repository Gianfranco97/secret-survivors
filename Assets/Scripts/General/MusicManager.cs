using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip creditsMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayMusic(AudioClip musicClip)
    {
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    public void PlayGameMusic() => PlayMusic(gameMusic);

    public void PlayMenuMusic() => PlayMusic(menuMusic);

    public void PlayBossMusic() => PlayMusic(bossMusic);

    public void PlayCreditsMusic() => PlayMusic(creditsMusic);

    public void StopMusic() => audioSource.Stop();

    public void SetVolume(float volume) => audioSource.volume = volume;

    public void ReduceVolume() => SetVolume(0.5f);

    public void NormalVolume() => SetVolume(1f);
}