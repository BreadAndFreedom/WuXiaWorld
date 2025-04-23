using UnityEngine;
using System;

public class DateCounter : MonoBehaviour
{
    // 起始日期（年, 月, 日）
    public int startYear = 2023;
    public int startMonth = 1;
    public int startDay = 1;

    // 当前累计天数
    private int totalDays = 0;

    // 当前日期
    private DateTime currentDate;

    void Start()
    {
        // 初始化日期
        currentDate = new DateTime(startYear, startMonth, startDay);
        UpdateDateDisplay();
    }

    // 增加天数
    public void AddDays(int daysToAdd)
    {
        totalDays += daysToAdd;
        currentDate = new DateTime(startYear, startMonth, startDay).AddDays(totalDays);
        UpdateDateDisplay();
    }

    // 更新日期显示（可以在UI中显示）
    private void UpdateDateDisplay()
    {
        Debug.Log($"当前日期: {currentDate.Year}年{currentDate.Month}月{currentDate.Day}日 (总天数: {totalDays})");
    }

    // 示例：在Inspector中点击按钮增加天数
    [ContextMenu("增加10天")]
    private void Add10Days()
    {
        AddDays(10);
    }

    [ContextMenu("增加30天")]
    private void Add30Days()
    {
        AddDays(30);
    }

    [ContextMenu("增加365天")]
    private void Add365Days()
    {
        AddDays(365);
    }

    // 获取当前日期信息
    public int GetCurrentYear() => currentDate.Year;
    public int GetCurrentMonth() => currentDate.Month;
    public int GetCurrentDay() => currentDate.Day;
    public int GetTotalDays() => totalDays;
}