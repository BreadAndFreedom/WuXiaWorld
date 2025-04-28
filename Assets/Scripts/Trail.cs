using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trail : MonoBehaviour
{
    public GameObject characterManager;
    public GameObject dateManager;
    public GameObject eventPoolManager;
    public int costDay;//点击按钮后消耗的时间
    public int targertBlockID;//目标地块id
    private int morality;
    private int blockID;
    private DateTime currentTime;
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log("花费了" + costDay+"天");
        dateManager.GetComponent<DateCounter>().AddDays(costDay);//增加天数

        blockID = characterManager.GetComponent<CharacterManager>().blockID;//暂将随机事件抽取放在这里
        currentTime = dateManager.GetComponent<DateCounter>().GetCurrentDate();
        morality = characterManager.GetComponent<CharacterManager>().morality;
        GameEvent event1=eventPoolManager.GetComponent<EventPoolManager>().GetRandomEvent(morality,currentTime,blockID);
        Debug.Log(event1.description);//测试输出

        characterManager.GetComponent<CharacterManager>().blockID = targertBlockID;//将角色的当前地块id更新
    }
}
