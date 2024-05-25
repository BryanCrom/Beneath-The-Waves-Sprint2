using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public Quest chestQuest;
    public GameObject chestLid;
    public float openAngle = -60f;
    public float closeAngle = 0f;
    public float animationDuration = 1f;
    private bool isOpen = false;
    private bool isQuestAccepted = false;
    private bool isPlayerInRange = false;

    void Start()
    {
        // Create a new quest with a description and reward
        chestQuest = new Quest("Defeat the enemy", 50);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInRange())
        {
            if (isOpen)
            {
                CloseChest();
            }
            else
            {
                OpenChest();
                if (!isQuestAccepted)
                {
                    QuestManager.Instance.StartQuest(chestQuest);
                    isQuestAccepted = true;
                }
            }
        }
    }

    void OpenChest()
    {
        StartCoroutine(AnimateChest(openAngle));
        isOpen = true;
        UIManager.Instance.ShowQuestWindow(chestQuest);
    }

    void CloseChest()
    {
        StartCoroutine(AnimateChest(closeAngle));
        isOpen = false;
        UIManager.Instance.HideQuestWindow();
    }

    IEnumerator AnimateChest(float targetAngle)
    {
        float currentAngle = chestLid.transform.localEulerAngles.x;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.LerpAngle(currentAngle, targetAngle, elapsedTime / animationDuration);
            chestLid.transform.localEulerAngles = new Vector3(angle, chestLid.transform.localEulerAngles.y, chestLid.transform.localEulerAngles.z);
            yield return null;
        }

        chestLid.transform.localEulerAngles = new Vector3(targetAngle, chestLid.transform.localEulerAngles.y, chestLid.transform.localEulerAngles.z);
    }

    bool IsPlayerInRange()
    {
        return isPlayerInRange;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
