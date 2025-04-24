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
    public List<string> validLands; // ��Ч�ĵؿ��б�
    // ����¼��Ƿ��������
    public bool IsValid(int morality, DateTime currentTime, string land)
    {
        // ����ƶ�ֵ
        if (morality < minMorality || morality > maxMorality)
            return false;

        // ���ؿ�
        if (!validLands.Contains(land))
            return false;
        // ���ʱ���Ƿ����ڽ�ֹ����
        if (currentTime>eventTime)
        {
            return false;
        }
        return true;
    }

    // �����¼���Ȩ��ֵ
  
}