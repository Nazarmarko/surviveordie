using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeObjectOnLoad : MonoBehaviour
{
    private static GameObject instance;
    void Start()
    {
        
        if(instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(instance);
        }
    }
}
