using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header ("---- Audio Variables ----")]
    public AudioClip[] hurtClips;
    public AudioClip[] attackClips;
    public AudioClip[] dieClips;
    public AudioClip[] powerUpClips;
    public AudioSource P1SFX;
    public AudioSource P2SFX;
    public AudioSource P3SFX;
    public AudioSource P4SFX;

    [Header ("---- Indexes ----")]
    public int joyceIndex = 0;
    public int emilyIndex = 1;
    public int bulletIndex = 2;
    public int polyMarsIndex = 3;
    public int backyardIndex = 4;
    public int allenIndex = 5;
    public int williamIndex = 6;

    
}
