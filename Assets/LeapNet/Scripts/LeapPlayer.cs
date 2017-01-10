using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

[NetworkSettings(sendInterval = 0.01f)]
public class LeapPlayer : NetworkBehaviour
{
    public void Start()
    {
        Debug.Log("Leap Player Start");
    }

    public LeapHand leftHand;
    public LeapHand rightHand;

    public LeapAudio leapAudio;

    [Command(channel = 0)]
    public void CmdBeginHand(int hand, byte[] arrHand)
    {
        try
        {
            byte[] newHand = CLZF.Decompress(arrHand);
            if (hand == 0)
            {
                leftHand.BeginHand(newHand);
            }
            else
            {
                rightHand.BeginHand(newHand);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [Command(channel = 0)]
    public void CmdFinishHand(int hand)
    {
        try
        {
            if (hand == 0)
            {
                leftHand.FinishHand();
            }
            else
            {
                rightHand.FinishHand();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [Command(channel = 1)]
    public void CmdSetLeapHand(int hand, byte[] arrHand)
    {
        try
        {
            byte[] newHand = CLZF.Decompress(arrHand);
            if (hand == 0)
            {
                leftHand.SetLeapHand(newHand);
            }
            else
            {
                rightHand.SetLeapHand(newHand);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [Command(channel = 2)]
    public void CmdAudioSend(byte[] f, int chan)
    {
        try
        {
            float[] newAudio = CLZF.DecompressAudio(f);
            leapAudio.Set(newAudio, chan);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
}