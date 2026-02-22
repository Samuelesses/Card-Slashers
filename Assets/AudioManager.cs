using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("AudioManager: Instance created");
        }
        else
        {
            Debug.Log("AudioManager: Duplicate instance destroyed");
            Destroy(gameObject);
            return;
        }

        // Try to get existing AudioSource, or add one if none exists
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioManager: Created new AudioSource component");
        }
        else
        {
            Debug.Log("AudioManager: Using existing AudioSource component");
        }
    }

    public void Play(string eventType, string characterName)
    {
        Debug.Log($"AudioManager: Attempting to play {eventType} for {characterName}");
        
        string path = "Audio/" + characterName + "/" + eventType;
        Debug.Log($"AudioManager: Loading clips from path: {path}");

        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);

        if (clips.Length == 0)
        {
            Debug.LogWarning($"AudioManager: No audio clips found at path: {path}");
            return;
        }

        Debug.Log($"AudioManager: Found {clips.Length} clips at path: {path}");
        
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        Debug.Log($"AudioManager: Playing clip: {randomClip.name}");
        
        audioSource.PlayOneShot(randomClip);
    }
}