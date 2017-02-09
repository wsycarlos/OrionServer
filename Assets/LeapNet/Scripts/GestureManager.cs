using UnityEngine;
using System.Collections;

public class GestureManager : MonoBehaviour
{
    public int handness = 0;

    GameObject _gesPos = null;
    GameObject gesPos
    {
        get
        {
            if (_gesPos == null)
            {
                _gesPos = GameObject.Find("StaticGesturePos");
            }
            return _gesPos;
        }
    }

    LeapPlayer _player = null;
    private LeapPlayer player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<LeapPlayer>();
            }
            return _player;
        }
    }

    private LeapHand handModel
    {
        get
        {
            if (player != null)
            {
                if(handness == 0)
                {
                    return player.leftHand;
                }
                else
                {
                    return player.rightHand;
                }
            }
            return null;
        }
    }

    GameObject model;

    public GameObject gestureModel1;
    public GameObject gestureModel2;
    public GameObject gestureModel3;
    public GameObject gestureModel4;

    public void OnEnable()
    {
        Messenger.AddListener("EndGesture_" + handness, EndHand);
        Debug.Log("Gesture_" + handness);
        Messenger.AddListener<int>("Gesture_" + handness, Gesture);
    }

    public void OnDisable()
    {
        Messenger.RemoveListener("EndGesture_" + handness, EndHand);
        Messenger.RemoveListener<int>("Gesture_" + handness, Gesture);
    }
    
    public void EndHand()
    {
        if(model != null)
        {
            Destroy(model);
        }
    }

    public void Gesture(int gesture)
    {
        if(gesture == 1)
        {
            Gesture1();
        }
        else if (gesture == 2)
        {
            Gesture2();
        }
        else if (gesture == 3)
        {
            Gesture3();
        }
        else if (gesture == 4)
        {
            Gesture4();
        }
    }

    public void Gesture1()
    {
        EndHand();
        model = Instantiate(gestureModel1) as GameObject;
        model.transform.SetParent(gesPos.transform);
        FollowHandModel fhm = model.AddComponent<FollowHandModel>();
        fhm.handModel = handModel;
        fhm.direction = new Vector3(0, -1, 1);
        model.SetActive(true);
    }

    public void Gesture2()
    {
        EndHand();
        model = Instantiate(gestureModel2) as GameObject;
        model.transform.SetParent(gesPos.transform);
        FollowHandModel fhm = model.AddComponent<FollowHandModel>();
        fhm.handModel = handModel;
        fhm.direction = new Vector3(0, -1, 1);
        model.SetActive(true);
    }

    public void Gesture3()
    {
        EndHand();
        model = Instantiate(gestureModel3) as GameObject;
        model.transform.SetParent(gesPos.transform);
        FollowHandModel fhm = model.AddComponent<FollowHandModel>();
        fhm.handModel = handModel;
        fhm.direction = new Vector3(0, 1, 1);
        model.SetActive(true);
    }

    public void Gesture4()
    {
        EndHand();
        model = Instantiate(gestureModel4) as GameObject;
        model.transform.SetParent(gesPos.transform);
        FollowHandModel fhm = model.AddComponent<FollowHandModel>();
        fhm.handModel = handModel;
        fhm.direction = new Vector3(0, 1, 1);
        model.SetActive(true);
    }
}