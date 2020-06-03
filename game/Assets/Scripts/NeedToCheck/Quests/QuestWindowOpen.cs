using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWindowOpen : MonoBehaviour
{
    public GameObject questWindow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            questWindow.SetActive(true);
          //  IsOpened = true;
        }
    }
}
