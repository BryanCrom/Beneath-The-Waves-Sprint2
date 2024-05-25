using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    private Quest currentQuest;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        EnemyDeath.OnEnemyKilled += OnEnemyKilledHandler;
    }

    void OnDisable()
    {
        EnemyDeath.OnEnemyKilled -= OnEnemyKilledHandler;
    }

    public void StartQuest(Quest quest)
    {
        currentQuest = quest;
        UIManager.Instance.ShowQuestWindow(quest);
    }

    private void OnEnemyKilledHandler()
    {
        if (currentQuest != null && !currentQuest.isCompleted)
        {
            // Assuming the quest is to kill one enemy, we mark it as completed
            currentQuest.isCompleted = true;
            CompleteQuest();
        }
    }

    public void CompleteQuest()
    {
        if (currentQuest != null && !currentQuest.isCompleted)
        {
            currentQuest.isCompleted = true;
            PlayerAccount.Instance.AddCoins(currentQuest.reward);
            UIManager.Instance.HideQuestWindow();
            Debug.Log($"Quest completed: {currentQuest.description}. Reward: {currentQuest.reward} coins.");
        }
    }


    public bool IsQuestCompleted()
    {
        return currentQuest != null && currentQuest.isCompleted;
    }
}