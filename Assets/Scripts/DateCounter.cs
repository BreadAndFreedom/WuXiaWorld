using UnityEngine;
using System;

public class DateCounter : MonoBehaviour
{
    // ��ʼ���ڣ���, ��, �գ�
    public int startYear = 2023;
    public int startMonth = 1;
    public int startDay = 1;

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

    // ʾ������Inspector�е����ť��������
    [ContextMenu("����10��")]
    private void Add10Days()
    {
        AddDays(10);
    }

    [ContextMenu("����30��")]
    private void Add30Days()
    {
        AddDays(30);
    }

    [ContextMenu("����365��")]
    private void Add365Days()
    {
        AddDays(365);
    }

    // ��ȡ��ǰ������Ϣ
    public int GetCurrentYear() => currentDate.Year;
    public int GetCurrentMonth() => currentDate.Month;
    public int GetCurrentDay() => currentDate.Day;
    public int GetTotalDays() => totalDays;
}