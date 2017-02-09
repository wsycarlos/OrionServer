using UnityEngine;
using System.Collections;

public class SyncPos : MonoBehaviour
{
    public string targetGameObject;

    private Transform pos;
    // Use this for initialization
    void Start()
    {
        pos = GameObject.Find(targetGameObject).transform;
        Sync();
    }

    // Update is called once per frame
    public void Sync()
    {
        transform.position = pos.position;
        transform.rotation = pos.rotation;
        //transform.localScale = pos.localScale;
    }
}
