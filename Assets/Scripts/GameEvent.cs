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
    public List<string> validLands; // 有效的地块列表
    // 检查事件是否符合条件
    public bool IsValid(int morality, DateTime currentTime, string land)
    {
        // 检查善恶值
        if (morality < minMorality || morality > maxMorality)
            return false;

        // 检查地块
        if (!validLands.Contains(land))
            return false;
        // 检查时间是否晚于截止日期
        if (currentTime>eventTime)
        {
            return false;
        }
        return true;
    }

    // 计算事件的权重值
  
}