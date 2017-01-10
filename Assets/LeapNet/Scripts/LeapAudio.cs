using UnityEngine;
using System.Collections;

public class LeapAudio : MonoBehaviour
{
    AudioSource source;
    private const int FREQUENCY = 11025;

    void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }

    public void Set(float[] f)
    {
        source.clip = AudioClip.Create("test", f.Length, 1, FREQUENCY, false);
        source.clip.SetData(f, 0);
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}
