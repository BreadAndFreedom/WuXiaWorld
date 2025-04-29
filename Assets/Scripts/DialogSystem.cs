using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("文本")]
    public TextAsset textFile;
    [Header("游戏物体")]
    //public GameObject camera;
    [Header("角色名称与游戏物体对应")]
    public List<string> names = new List<string>();
    public List<GameObject> characters = new List<GameObject>();
    public List<TMP_Text> texts = new List<TMP_Text>();
    [Header("生成按钮预制体")]
    public GameObject optionButton;//选项按钮预制体
    public Transform buttonGroup;//按钮组的位置
    public GameObject aiWindow;//金手指窗口
    public GameObject aiButton;//

    private string[] dialogRows;
    public int dialogIndex = 1;
    private List<Dialog> dialogs = new List<Dialog>();
    private bool isSettled = false;
    public bool hasEnd = false;
    public bool isSelecting = false;
    AsyncOperation async;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hasEnd == false && isSelecting == false)
        {
            OnClickNext();
        }
    }
    public void DialogModule()
    {
        InitializeObjects();
        GetTextFromFile();
        ShowDialogRow();
    }
    void GetTextFromFile()
    {
        dialogRows = textFile.text.Split('\n');
        Debug.Log(dialogRows[dialogIndex]);
    }

    public void ShowDialogRow()
    {
        foreach (var row in dialogRows)
        {
            //textList.Add(cell);
            string[] cells = row.Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)//判断为语句标识为#以及编号是否对应
            {
                foreach (var dialog in dialogs)
                {
                    if (cells[2] == dialog.identify)//通过id寻址，找到对应的对象
                    {
                        if (dialog.isActive == false)//判定是否生成过对话框和文本，生成完毕后设为true
                        {
                            characters[dialog.index].SetActive(true);
                            dialogs[dialog.index].isActive = true;
                        }
                        dialogs[dialog.index].text.text = cells[3];//更新文本
                    }

                }
                dialogIndex = int.Parse(cells[4]);
                break;
            }

            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                foreach (var canva in characters)//关闭所有角色下的幕布
                {
                    canva.SetActive(false);

                }
                hasEnd = true;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)//弹出选项窗口
            {
                //Debug.Log("选项来咯");
                GenerateOption(dialogIndex);//先用序号标识符试试
                isSelecting = true;
                break;
            }
            else if (cells[0] == "%")//切换场景
            {
                int index = int.Parse(cells[5]);
                SelectScene(index);
            }
        }
    }
    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(',');
        if (cells[0] == "&")
        {
            GameObject optButton = Instantiate(optionButton, buttonGroup);//绑定了按钮的事件
            optButton.GetComponentInChildren<TMP_Text>().text = cells[3];
            optButton.GetComponent<Button>().onClick.AddListener
                (
                    delegate
                    {
                        OnOptionClick(int.Parse(cells[4]));
                        isSelecting = false;
                        Debug.Log(cells[5]);
                        if (cells[5]!= "")
                        {
                            string[] effects = cells[5].Split('@');
                            Debug.Log(effects[0]);
                            OptionEffect(effects[0]);
                        }
                    }
                );
            GenerateOption(_index + 1);
        }
    }
    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }

    public void OnClickNext()
    {
        if (isSettled == false)
        {
            DialogModule();
            isSettled = true;
        }
        else
        {
            ShowDialogRow();
        }

    }

    public void InitializeObjects()
    {
        int index = 0;
        foreach (var name in names)
        {
            Dialog dialog = new Dialog();
            dialog.identify = name;
            dialog.index = index;
            dialog.isActive = false;
            dialog.text = texts[index];
            index++;
            dialogs.Add(dialog);
        }

    }


    public class Dialog
    {
        public TMP_Text text;//用于更新文本
        public bool isActive;//用于判断对话框是否显示
        public string identify;
        public int index;

    }
    public void SelectScene(int index)//切换场景的方法
    {
        async = SceneManager.LoadSceneAsync(index);
        async.allowSceneActivation = true;
    }

    public void OptionEffect(string effect)//用于唤出AI窗口的方法
    {
        if (effect == "呼出金手指")
        {
            aiWindow.SetActive(true);
            aiButton.SetActive(false);
        }
    }
}
