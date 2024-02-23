using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 确保导入了UnityEngine.UI

public class TaskSystem : MonoBehaviour
{
    private List<GameObject> tasks = new List<GameObject>();
    [Header("任务so")]
    public TaskItem[] Taskitems;
    [Header("ui模板")]
    public GameObject Taskprefab;
    [Header("定位bt位置")]
    public GameObject TaskStateBtton;
    [Header("任务标题")]
    public Text Title;
    [Header("奖励位置")]
    public Transform pos;
    [Header("奖励basic预制体")]
    public Image Rewardimage;
    [Header("任务描述文本")]
    public Text text;
    public int numIndex = 0;

    // 新增一个字典来存储任务按钮和对应的状态按钮的关联
    private Dictionary<GameObject, GameObject> taskToStateButtonMap = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
        GameObject content = GameObject.Find("Canvas/Panel/Scroll View/Viewport/Content");

        foreach (var taskItem in Taskitems)
        {
            GameObject taskButton = Instantiate(Taskprefab.transform.GetChild(0).gameObject, content.transform);
            GameObject stateButton = Instantiate(Taskprefab.transform.GetChild(1).gameObject, TaskStateBtton.transform);
            stateButton.transform.position = TaskStateBtton.transform.position;
            stateButton.SetActive(false); // 初始时隐藏状态按钮

            // 存储任务按钮和状态按钮的关联
            taskToStateButtonMap[taskButton] = stateButton;

            // 为任务按钮添加点击事件监听器
            taskButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                OnTaskButtonClicked(taskButton, taskItem);
             } );
            stateButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                BtState(taskButton,taskToStateButtonMap[taskButton], taskItem);
            });

            tasks.Add(taskButton); // 存储任务UI
            taskButton.transform.GetChild(0).GetComponent<Text>().text = taskItem.taskName;
        }
    }
    private void Update()
    {
       
        foreach (TaskItem task in Taskitems)
        {
            if (task.CheckCompletion())
            {
                task.Reward();
            }
        }

    }

    private void BtState(GameObject clickedTaskButton, GameObject taskButton,TaskItem item)
    {
        if (item.taskState == 0)
        {
            item.taskState = 1;
            taskButton.transform.GetChild(0).GetComponent<Text>().text = "waiting";

        }
        else if (item.taskState == 2)
        {

            Destroy(clickedTaskButton);
        }
    }
    private void OnTaskButtonClicked(GameObject clickedTaskButton,TaskItem item)
    {
       
        // 遍历所有任务按钮
        foreach (var taskButton in taskToStateButtonMap.Keys)
        {
            // 如果是点击的任务按钮，则显示对应的状态按钮
            if (taskButton == clickedTaskButton)
            {

                if (item.taskState == 0)
                {
                    taskToStateButtonMap[taskButton].transform.GetChild(0).GetComponent<Text>().text = "receive";
                }
                if (item.taskState == 1)
                {
                    taskToStateButtonMap[taskButton].transform.GetChild(0).GetComponent<Text>().text = "waiting";
                }
                if (item.taskState == 2)
                {
                    taskToStateButtonMap[taskButton].transform.GetChild(0).GetComponent<Text>().text = "finish";
                }
                 Title.text = item.taskName;
                text.text = item.CurrTaskDescription;

                //删除所有子物体
                for (int i = 0; i < pos.childCount; i++)
                {
                    Destroy(pos.GetChild(i).gameObject);
                }
                foreach (var reward in item.CurrTaskGoods)
                {
                    Image imageTemp = Instantiate(Rewardimage, pos);                
                    imageTemp.sprite = reward.ItemImage;
                    imageTemp.transform.GetChild(0).GetComponentInChildren<Text>().text = item.RewardNum[numIndex++].ToString();

                }
                numIndex = 0;
                taskToStateButtonMap[taskButton].SetActive(true);   
            }
            else // 否则隐藏状态按钮
            {
                taskToStateButtonMap[taskButton].SetActive(false);  
            }
        }
    }

}
