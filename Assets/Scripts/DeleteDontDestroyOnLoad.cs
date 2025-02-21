using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("InGameUI"));
        Destroy(GameObject.FindGameObjectWithTag("InGameCam"));
    }
}
