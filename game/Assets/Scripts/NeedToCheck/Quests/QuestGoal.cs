using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public Text kills;
    public bool IsReached() 
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled() 
    {
        
        if(goalType == GoalType.Kill)
        currentAmount++;
        // kills.text = currentAmount.ToString();

        Debug.Log("Enemy Killed");
    }

    public void ItemGathered()
    {
        if (goalType == GoalType.Gathering)
            currentAmount++;
    }


}

public enum GoalType 
{
    Kill,
    Gathering
}