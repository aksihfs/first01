using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "kill", menuName = "Task/kill")]

public class killTaks : TaskItem
{
    public int money = 1;
    public override bool CheckCompletion()
    {
        return money > 0;
    }

    public override void Reward()
    {
        Debug.Log("dsf");
    }
}
