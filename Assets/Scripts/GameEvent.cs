using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEvent :  MonoBehaviour
{
    public string eventID;          // 事件唯一标识
    public string description;      // 事件描述
    public int minMorality;        // 最小善恶值
    public int maxMorality;        // 最大善恶值
    public DateTime eventTime;       // 截止日期
    public List<int> validLands; // 有效的地块列表
    public int startYear; //年月日
    public int startMonth;
    public int startDay;
    public GameObject dialog;
    // 检查事件是否符合条件
    public void Awake()
    {
        eventTime = new DateTime(startYear, startMonth, startDay);//事件截止日期初始化
        //Debug.Log($"事件{eventID}截止日期: {eventTime.Year}年{eventTime.Month}月{eventTime.Day}日");
    }
    public bool IsValid(int morality, DateTime currentTime, int blockID)
    {
        // 检查善恶值
        if (morality < minMorality || morality > maxMorality)//若大于最大或小于最小区间则排除
            return false;

        // 检查地块
        if (!validLands.Contains(blockID))//若不属于地块ID群组中则排除
            return false;
        // 检查时间
        if (currentTime>eventTime)//若时间晚于截止日期则排除
        {
            return false;
        }
        return true;
    }

    // 计算事件的权重值
  
}