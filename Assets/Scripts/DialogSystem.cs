using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("�ı�")]
    public TextAsset textFile;
    [Header("��Ϸ����")]
    //public GameObject camera;
    [Header("��ɫ��������Ϸ�����Ӧ")]
    public List<string> names = new List<string>();
    public List<GameObject> characters = new List<GameObject>();
    public List<TMP_Text> texts = new List<TMP_Text>();


    private string[] dialogRows;
    private int dialogIndex = 1;
    private List<Dialog> dialogs = new List<Dialog>();
    private bool isSettled = false;
    private bool hasEnd = false;
    AsyncOperation async;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hasEnd == false)
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
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                foreach (var dialog in dialogs)
                {
                    if (cells[2] == dialog.identify)//ͨ��idѰַ���ҵ���Ӧ�Ķ���
                    {
                        if (dialog.isActive == false)//�ж��Ƿ����ɹ��Ի�����ı���������Ϻ���Ϊtrue
                        {
                            characters[dialog.index].SetActive(true);
                            dialogs[dialog.index].isActive = true;
                        }
                        dialogs[dialog.index].text.text = cells[3];//�����ı�
                    }

                }
                dialogIndex = int.Parse(cells[4]);
                break;
            }

            else if (cells[0] == "END")
            {
                foreach (var canva in characters)
                {
                    canva.SetActive(false);

                }
                hasEnd = true;
            }
            else if (cells[0] == "&")//��֣��л�ͼƬ
            {

            }
            else if (cells[0] == "%")//�л�����
            {
                int index = int.Parse(cells[5]);
                SelectScene(index);
            }
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
        public TMP_Text text;//���ڸ����ı�
        public bool isActive;//�����ж϶Ի����Ƿ���ʾ
        public string identify;
        public int index;

    }
    public void SelectScene(int index)
    {
        async = SceneManager.LoadSceneAsync(index);
        async.allowSceneActivation = true;
    }
}
