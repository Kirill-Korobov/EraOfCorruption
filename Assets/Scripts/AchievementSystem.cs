using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public List<Achievement> Achievements;

    public void UnlockAchievement(string achievementName)
    {
        foreach(Achievement achievement in Achievements)
        {
            if(achievement.name == achievementName && !achievement.unlocked)
            {
                achievement.unlocked = true;
                Debug.Log("Achievement unlocked" + achievement.name);
            }
        }
    }

    public bool IsAchievementUnlocked(string achievementName)
    {
        foreach(Achievement achievement in Achievements)
        {
            if(achievement.name == achievementName)
            {
                return achievement.unlocked;
            }
        }
        return false;
    }
}
