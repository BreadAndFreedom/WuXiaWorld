using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEvent :  MonoBehaviour
{
    public string eventID;          // �¼�Ψһ��ʶ
    public string description;      // �¼�����
    public int minMorality;        // ��С�ƶ�ֵ
    public int maxMorality;        // ����ƶ�ֵ
    public DateTime eventTime;       // ��ֹ����
    public List<int> validLands; // ��Ч�ĵؿ��б�
    public int startYear; //������
    public int startMonth;
    public int startDay;
    public GameObject dialog;
    // ����¼��Ƿ��������
    public void Awake()
    {
        eventTime = new DateTime(startYear, startMonth, startDay);//�¼���ֹ���ڳ�ʼ��
        //Debug.Log($"�¼�{eventID}��ֹ����: {eventTime.Year}��{eventTime.Month}��{eventTime.Day}��");
    }
    public bool IsValid(int morality, DateTime currentTime, int blockID)
    {
        // ����ƶ�ֵ
        if (morality < minMorality || morality > maxMorality)//����������С����С�������ų�
            return false;

        // ���ؿ�
        if (!validLands.Contains(blockID))//�������ڵؿ�IDȺ�������ų�
            return false;
        // ���ʱ��
        if (currentTime>eventTime)//��ʱ�����ڽ�ֹ�������ų�
        {
            return false;
        }
        return true;
    }

    // �����¼���Ȩ��ֵ
  
}