using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int bomb1;
    public int bomb2;
    public int bomb3;


    //timeText.text = string.Format
    public Text bomb1_text;
    public Text bomb2_text;
    public Text bomb3_text;


    int startingAmt = 3;

    void Awake()
    {
        bomb1 = startingAmt;
        bomb2 = startingAmt;
        bomb3 = startingAmt;

        bomb1_text.text = bomb1.ToString();
        bomb2_text.text = bomb2.ToString();
        bomb3_text.text = bomb3.ToString();

    }
    // Start is called before the first frame update
    void Start()
    {
        bomb1 = startingAmt;
        bomb2 = startingAmt;
        bomb3 = startingAmt;

        bomb1_text.text = bomb1.ToString();
        bomb2_text.text = bomb2.ToString();
        bomb3_text.text = bomb3.ToString();

    }

    public void AddBomb1()
    {
        bomb1 += 1;
        bomb1_text.text = bomb1.ToString();
    }
    public void SubtractBomb1()
    {
        bomb1 -= 1;
        bomb1_text.text = bomb1.ToString();
    }
    public int GetBomb1()
    {
        return bomb1;
    }

    public void AddBomb2()
    {
        bomb2 += 1;
        bomb2_text.text = bomb2.ToString();
    }
    public void SubtractBomb2()
    {
        bomb2 -= 1;
        bomb2_text.text = bomb2.ToString();
    }
    public int GetBomb2()
    {
        return bomb2;
    }


    public void AddBomb3()
    {
        bomb3 += 1;
        bomb3_text.text = bomb3.ToString();
    }
    public void SubtractBomb3()
    {
        bomb3 -= 1;
        bomb3_text.text = bomb3.ToString();
    }
    public int GetBomb3()
    {
        return bomb3;
    }


}
