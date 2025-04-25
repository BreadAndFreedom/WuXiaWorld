using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject startArrows;
    public GameObject endArrows;
    public int blockID;
    public string blockName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        endArrows.SetActive(true);
        startArrows.SetActive(false);
    }
}
