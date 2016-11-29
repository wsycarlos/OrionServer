using UnityEngine;
using System.Collections;

public class LeapAudio : MonoBehaviour
{
    AudioSource source;
    private const int FREQUENCY = 11025;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Set(float[] f, int chan)
    {
        if (chan > 0)
        {
            source.clip = AudioClip.Create("test", f.Length, chan, FREQUENCY, false);
            source.clip.SetData(f, 0);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }
}
