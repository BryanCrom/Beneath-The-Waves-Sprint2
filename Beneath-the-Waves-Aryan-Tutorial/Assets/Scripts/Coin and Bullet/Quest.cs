using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string description;
    public int reward;
    public bool isCompleted;

    public Quest(string description, int reward)
    {
        this.description = description;
        this.reward = reward;
        this.isCompleted = false;
    }
}
