using UnityEngine;
using System.Collections;

public class FollowHandModel : MonoBehaviour
{
    public LeapHand handModel;

    public Vector3 direction;

    private NetHand hand;

    void Start()
    {
        hand = handModel.GetLeapHand();
        if (hand != null)
        {
            transform.localPosition = hand.PalmPosition.ToVector3();
            transform.localRotation = hand.Rotation.ToQuaternion();
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void Update()
    {
        hand = handModel.GetLeapHand();
        if (hand != null)
        {
            transform.localPosition = hand.PalmPosition.ToVector3();
            transform.localRotation = hand.Rotation.ToQuaternion();
        }
    }
}
