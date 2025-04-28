using UnityEngine;
using System;
using TMPro;

public class DateCounter : MonoBehaviour
{
    // ��ʼ���ڣ���, ��, �գ�
    public int startYear;
    public int startMonth;
    public int startDay;

    //���Ͻ��������
    public GameObject datePanel;

    // ��ǰ�ۼ�����
    private int totalDays = 0;

    // ��ǰ����
    private DateTime currentDate;

    void Start()
    {
        // ��ʼ������
        currentDate = new DateTime(startYear, startMonth, startDay);
        UpdateDateDisplay();
    }
    private void Update()
    {
        datePanel.GetComponent<TMP_Text>().text = currentDate.Year+"��"+currentDate.Month+"��"+currentDate.Day+"��";
    }
    // ��������
    public void AddDays(int daysToAdd)
    {
        totalDays += daysToAdd;
        currentDate = new DateTime(startYear, startMonth, startDay).AddDays(totalDays);
        UpdateDateDisplay();
    }
        
    // ����������ʾ��������UI����ʾ��
    private void UpdateDateDisplay()
    {
        Debug.Log($"��ǰ����: {currentDate.Year}��{currentDate.Month}��{currentDate.Day}�� (������: {totalDays})");
    }
    //��ȡ��ǰ����
    public DateTime GetCurrentDate()
    {
        return currentDate;
    }
    // ��ȡ��ǰ������Ϣ
    public int GetCurrentYear() => currentDate.Year;
    public int GetCurrentMonth() => currentDate.Month;
    public int GetCurrentDay() => currentDate.Day;
    public int GetTotalDays() => totalDays;
}