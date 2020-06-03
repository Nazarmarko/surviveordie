using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButton : MonoBehaviour
{
    public GameObject questWindow;

    void CloSeQuestWindow()
    {
        questWindow.SetActive(false);
    }
        

}
