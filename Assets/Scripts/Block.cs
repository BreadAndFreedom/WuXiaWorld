using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject trails;
    public int blockID;
    public string blockName;
    public GameObject characterManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterManager.GetComponent<CharacterManager>().blockID == blockID)//判断当前地块id和主角所在地块id是否一致
        {
            trails.SetActive(true);//一致则呈现路径
        }
        else
        {
            trails.SetActive(false);
        }
    }
    //public void OnClick()
    //{
    //    endArrows.SetActive(true);
    //    startArrows.SetActive(false);
    //}
}
