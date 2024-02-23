using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ȷ��������UnityEngine.UI

public class TaskSystem : MonoBehaviour
{
    private List<GameObject> tasks = new List<GameObject>();
    [Header("����so")]
    public TaskItem[] Taskitems;
    [Header("uiģ��")]
    public GameObject Taskprefab;
    [Header("��λbtλ��")]
    public GameObject TaskStateBtton;
    [Header("�������")]
    public Text Title;
    [Header("����λ��")]
    public Transform pos;
    [Header("����basicԤ����")]
    public Image Rewardimage;
    [Header("���������ı�")]
    public Text text;
    public int numIndex = 0;

    // ����һ���ֵ����洢����ť�Ͷ�Ӧ��״̬��ť�Ĺ���
    private Dictionary<GameObject, GameObject> taskToStateButtonMap = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
        GameObject content = GameObject.Find("Canvas/Panel/Scroll View/Viewport/Content");

        foreach (var taskItem in Taskitems)
        {
            GameObject taskButton = Instantiate(Taskprefab.transform.GetChild(0).gameObject, content.transform);
            GameObject stateButton = Instantiate(Taskprefab.transform.GetChild(1).gameObject, TaskStateBtton.transform);
            stateButton.transform.position = TaskStateBtton.transform.position;
            stateButton.SetActive(false); // ��ʼʱ����״̬��ť

            // �洢����ť��״̬��ť�Ĺ���
            taskToStateButtonMap[taskButton] = stateButton;

            // Ϊ����ť��ӵ���¼�������
            taskButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                OnTaskButtonClicked(taskButton, taskItem);
             } );
            stateButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                BtState(taskButton,taskToStateButtonMap[taskButton], taskItem);
            });

            tasks.Add(taskButton); // �洢����UI
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
       
        // ������������ť
        foreach (var taskButton in taskToStateButtonMap.Keys)
        {
            // ����ǵ��������ť������ʾ��Ӧ��״̬��ť
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

                //ɾ������������
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
            else // ��������״̬��ť
            {
                taskToStateButtonMap[taskButton].SetActive(false);  
            }
        }
    }

}
