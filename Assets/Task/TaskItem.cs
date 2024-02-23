using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TaskItem : ScriptableObject
{   public string taskName;
    public int taskTarget;
    public int taskNow;
    public int taskState;
    public int taskAward;
    public item[] CurrTaskGoods;
    public int[] RewardNum;
    [TextArea]
    public string CurrTaskDescription;
    public abstract bool CheckCompletion();
    public abstract void Reward();
}
