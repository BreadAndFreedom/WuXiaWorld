using System;
using System.Collections.Generic;
using UnityEngine;

public class EventPoolManager : MonoBehaviour
{
    public List<GameEvent> allEvents = new List<GameEvent>();
    public DateCounter DateManager;
    public GameObject CharacterManager;
    //�����ã���ʱ��ȡ���ñ���
    private int morality;
    private int blockID;
    public DateTime currentTime;

    public void Start()
    {
        currentTime = DateManager.GetCurrentDate();//�¼���ֹ���ڳ�ʼ��
        
    }
    // ���¼����������ȡһ�������������¼�
    public GameEvent GetRandomEvent(int morality, DateTime currentTime, int blockID)
    {

        List<GameEvent> validEvents = new List<GameEvent>();
        List<float> weights = new List<float>();

        // 1. ɸѡ�����з����������¼�
        foreach (var gameEvent in allEvents)
        {
            if (gameEvent.IsValid(morality, currentTime, blockID))
            {
                validEvents.Add(gameEvent);
            }
        }

        if (validEvents.Count == 0)
        {
            Debug.LogWarning("û���ҵ������������¼�!");
            return null;
        }

        int randomValue = UnityEngine.Random.Range(0, validEvents.Count);
        //Debug.Log(randomValue);
        validEvents[randomValue].dialog.SetActive(true);//����Ի�
        return validEvents[randomValue];
    }
    public void OnClick()
    {
        morality = CharacterManager.GetComponent<CharacterManager>().morality;
        blockID = CharacterManager.GetComponent<CharacterManager>().blockID;
        GameEvent event1 = GetRandomEvent(morality, currentTime, blockID);
        Debug.Log(event1.description);//�������
        event1.dialog.SetActive(true);
    }

}
