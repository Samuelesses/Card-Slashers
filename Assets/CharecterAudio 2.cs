using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public string characterName; // Example: "Character1"

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayPain();
    }

    void PlayPain()
    {
        string path = "Audio/" + characterName + "/Pain";
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);

        if (clips.Length == 0)
            return;

        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(randomClip);
    }
}