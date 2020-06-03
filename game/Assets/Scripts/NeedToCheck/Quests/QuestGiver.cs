using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public GameObject QuestWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    public void OpenQuestWindow() 
    {
        Debug.Log("Opened"); 
        QuestWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }

    public void AcceptQuest() 
    {
        QuestWindow.SetActive(false);
        quest.IsActive = true;
    }
}
