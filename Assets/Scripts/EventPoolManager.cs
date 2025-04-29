using System;
using System.Collections.Generic;
using UnityEngine;

public class EventPoolManager : MonoBehaviour
{
    public List<GameEvent> allEvents = new List<GameEvent>();
    public DateCounter DateManager;
    public GameObject CharacterManager;
    //测试用，临时获取公用变量
    private int morality;
    private int blockID;
    public DateTime currentTime;

    public void Start()
    {
        currentTime = DateManager.GetCurrentDate();//事件截止日期初始化
        
    }
    // 从事件池中随机获取一个符合条件的事件
    public GameEvent GetRandomEvent(int morality, DateTime currentTime, int blockID)
    {

        List<GameEvent> validEvents = new List<GameEvent>();
        List<float> weights = new List<float>();

        // 1. 筛选出所有符合条件的事件
        foreach (var gameEvent in allEvents)
        {
            if (gameEvent.IsValid(morality, currentTime, blockID))
            {
                validEvents.Add(gameEvent);
            }
        }

        if (validEvents.Count == 0)
        {
            Debug.LogWarning("没有找到符合条件的事件!");
            return null;
        }

        int randomValue = UnityEngine.Random.Range(0, validEvents.Count);
        //Debug.Log(randomValue);
        validEvents[randomValue].dialog.SetActive(true);//激活对话
        return validEvents[randomValue];
    }
    public void OnClick()
    {
        morality = CharacterManager.GetComponent<CharacterManager>().morality;
        blockID = CharacterManager.GetComponent<CharacterManager>().blockID;
        GameEvent event1 = GetRandomEvent(morality, currentTime, blockID);
        Debug.Log(event1.description);//测试输出
        event1.dialog.SetActive(true);
    }

}
