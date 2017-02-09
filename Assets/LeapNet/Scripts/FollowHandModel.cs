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
        transform.localPosition = hand.PalmPosition.ToVector3();
        Vector3 newDirection = hand.PalmNormal.ToVector3() + hand.Direction.ToVector3();
        transform.localRotation = Quaternion.FromToRotation(direction, newDirection);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void Update()
    {
        hand = handModel.GetLeapHand();
        transform.localPosition = hand.PalmPosition.ToVector3();
        Vector3 newDirection = hand.PalmNormal.ToVector3() + hand.Direction.ToVector3();
        transform.localRotation = Quaternion.FromToRotation(direction, newDirection);
    }
}
