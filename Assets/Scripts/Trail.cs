using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public int costDay;
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log("花费了" + costDay+"天");
    }
}
