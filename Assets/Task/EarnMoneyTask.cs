using UnityEngine;

[CreateAssetMenu(fileName = "EarnMoneyTask", menuName = "Task/EarnMoneyTask")]
public class EarnMoneyTask : TaskItem
{
    public int money=1;
    public override bool CheckCompletion()
    {
        return money > 0;
    }

    public override void Reward()
    {
        Debug.Log("dsf");
    }
}
