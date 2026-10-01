using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour
{
    [Header("Meat pieces (Level 1, 2, 3)")]
    public Image[] meatPieces;

    [Header("Enemies")]
    public Image[] enemyIcons;
    public int[] enemyLevels;   
    public int[] enemyIDs;

    public Color lockedColor = Color.black;
    public Color unlockedColor = Color.white;

    void Start()
    {
        UpdateMeatAchievements();
        UpdateEnemyAchievements();
    }

    // ---------------- MEATS ----------------
    void UpdateMeatAchievements()
    {
        int lastCompleted = GameManager.Instance.lastLevelCompleted;

        for (int i = 0; i < meatPieces.Length; i++)
        {
            bool unlocked = (i + 1) <= lastCompleted;
            meatPieces[i].color = unlocked ? unlockedColor : lockedColor;
        }
    }

    // ---------------- ENEMIES ----------------
    void UpdateEnemyAchievements()
    {
        for (int i = 0; i < enemyIcons.Length; i++)
        {
            string key = $"Enemy_{enemyLevels[i]}_{enemyIDs[i]}";
            bool unlocked = PlayerPrefs.GetInt(key, 0) == 1;

            enemyIcons[i].color = unlocked ? unlockedColor : lockedColor;
        }
    }
}



