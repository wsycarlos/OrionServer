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
            //byte[] newHand = CLZF.Decompress(arrHand);
            if (hand == 0)
            {
                leftHand.BeginHand(arrHand);
            }
            else
            {
                rightHand.BeginHand(arrHand);
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
            //byte[] newHand = CLZF.Decompress(arrHand);
            if (hand == 0)
            {
                leftHand.SetLeapHand(arrHand);
            }
            else
            {
                rightHand.SetLeapHand(arrHand);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    private bool receiving = false;
    private byte[] received = null;
    [Command(channel = 2)]
    public void CmdAudioSend(byte[] f, int chan)
    {
        try
        {
            if (chan > 0 && !receiving)
            {
                receiving = true;
                Debug.Log(f.Length);
                //received = CLZF.Decompress(f);
                received = f;
            }
            else if (chan > 0 && receiving)
            {
                Debug.Log(f.Length);
                //var newBytes = CLZF.Decompress(f);
                byte[] tmp = received;
                received = new byte[tmp.Length + f.Length];
                System.Buffer.BlockCopy(tmp, 0, received, 0, tmp.Length);
                System.Buffer.BlockCopy(f, 0, received, tmp.Length, f.Length);
            }
            else if (chan < 0 && receiving)
            {
                receiving = false;
                Debug.Log("Final Length:" + received.Length);
                leapAudio.Set(CLZF.ToFloatArray(received));
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
}