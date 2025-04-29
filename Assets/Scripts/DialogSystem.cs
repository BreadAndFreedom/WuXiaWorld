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
    [Header("�ı�")]
    public TextAsset textFile;
    [Header("��Ϸ����")]
    //public GameObject camera;
    [Header("��ɫ��������Ϸ�����Ӧ")]
    public List<string> names = new List<string>();
    public List<GameObject> characters = new List<GameObject>();
    public List<TMP_Text> texts = new List<TMP_Text>();
    [Header("���ɰ�ťԤ����")]
    public GameObject optionButton;//ѡ�ťԤ����
    public Transform buttonGroup;//��ť���λ��
    public GameObject aiWindow;//����ָ����
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
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)//�ж�Ϊ����ʶΪ#�Լ�����Ƿ��Ӧ
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

            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                foreach (var canva in characters)//�ر����н�ɫ�µ�Ļ��
                {
                    canva.SetActive(false);

                }
                hasEnd = true;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)//����ѡ���
            {
                //Debug.Log("ѡ������");
                GenerateOption(dialogIndex);//������ű�ʶ������
                isSelecting = true;
                break;
            }
            else if (cells[0] == "%")//�л�����
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
            GameObject optButton = Instantiate(optionButton, buttonGroup);//���˰�ť���¼�
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
        public TMP_Text text;//���ڸ����ı�
        public bool isActive;//�����ж϶Ի����Ƿ���ʾ
        public string identify;
        public int index;

    }
    public void SelectScene(int index)//�л������ķ���
    {
        async = SceneManager.LoadSceneAsync(index);
        async.allowSceneActivation = true;
    }

    public void OptionEffect(string effect)//���ڻ���AI���ڵķ���
    {
        if (effect == "��������ָ")
        {
            aiWindow.SetActive(true);
            aiButton.SetActive(false);
        }
    }
}
