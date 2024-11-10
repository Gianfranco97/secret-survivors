using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    public string name;
    public AudioClip clip;
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioData[] audioData;
    public AudioSource audioSource;

    private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (AudioData audio in audioData)
        {
            audioDictionary.Add(audio.name, audio.clip);
        }
    }

    public void PlaySound(string name)
    {
        if (audioDictionary.ContainsKey(name))
        {
            audioSource.PlayOneShot(audioDictionary[name]);
        }
        else
        {
            Debug.LogWarning("Sound " + name + " not found!");
        }
    }
}
