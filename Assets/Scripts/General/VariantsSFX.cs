using System.Collections;
using UnityEngine;

public class VariantsSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    public float combinedSoundInterval = 0.1f;
    private Coroutine playCombinedSoundCoroutine;

    private void ChangePitch()
    {
        audioSource.pitch = Random.Range(1f, 1.3f);
    }

    public void PlaySound()
    {
        ChangePitch();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    private IEnumerator PlayCombinedSound()
    {
        yield return new WaitForSeconds(combinedSoundInterval);
        ChangePitch();
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        playCombinedSoundCoroutine = null;
    }

    public void PlayWithSoundGrouping()
    {
        if (playCombinedSoundCoroutine == null)
        {
            playCombinedSoundCoroutine = StartCoroutine(PlayCombinedSound());
        }
    }
}
