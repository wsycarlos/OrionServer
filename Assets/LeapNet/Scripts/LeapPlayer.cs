using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.IO;

[NetworkSettings(sendInterval = 0.01f)]
public class LeapPlayer : NetworkBehaviour
{
    public void Start()
    {
        Debug.Log("Leap Player Start");
    }

    public LeapHand leftHand;
    public LeapHand rightHand;

    [Command(channel = 0)]
    public void CmdBeginHand(int hand, byte[] arrHand)
    {
        try
        {
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
    public void CmdGesture(int hand, int gesture)
    {
        try
        {
            Messenger.Broadcast<int>("Gesture_" + hand, gesture);
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
            Messenger.Broadcast("EndGesture_" + hand);
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
}