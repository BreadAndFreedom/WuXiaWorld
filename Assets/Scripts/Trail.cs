using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trail : MonoBehaviour
{
    public GameObject characterManager;
    public GameObject dateManager;
    public GameObject eventPoolManager;
    public int costDay;//�����ť�����ĵ�ʱ��
    public int targertBlockID;//Ŀ��ؿ�id
    private int morality;
    private int blockID;
    private DateTime currentTime;
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log("������" + costDay+"��");
        dateManager.GetComponent<DateCounter>().AddDays(costDay);//��������

        blockID = characterManager.GetComponent<CharacterManager>().blockID;//�ݽ�����¼���ȡ��������
        currentTime = dateManager.GetComponent<DateCounter>().GetCurrentDate();
        morality = characterManager.GetComponent<CharacterManager>().morality;
        GameEvent event1=eventPoolManager.GetComponent<EventPoolManager>().GetRandomEvent(morality,currentTime,blockID);
        Debug.Log(event1.description);//�������

        characterManager.GetComponent<CharacterManager>().blockID = targertBlockID;//����ɫ�ĵ�ǰ�ؿ�id����
    }
}
