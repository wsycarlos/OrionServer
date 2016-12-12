﻿using UnityEngine;
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

    [Command(channel = 0)]
    public void CmdFinishHand(int hand)
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

    [Command(channel = 1)]
    public void CmdSetLeapHand(int hand, byte[] arrHand)
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

    [Command(channel = 2)]
    public void CmdAudioSend(float[] f, int chan)
    {
        leapAudio.Set(f, chan);
    }
}