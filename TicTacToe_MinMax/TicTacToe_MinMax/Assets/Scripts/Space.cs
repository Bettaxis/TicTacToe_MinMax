using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Space : MonoBehaviour
{
    //Sprite Images of the various Moves
    public Sprite RedX;
    public Sprite RedO;

    public Sprite BlueX;
    public Sprite BlueO;


    public GameObject ScriptObject;
    private TicTacToeLogic logicScript;

    private Color blue = Color.blue;
    private Color red = Color.red;

    // Use this for initialization
    void Start()
    {
        ScriptObject = GameObject.FindWithTag("Script Object");
        logicScript = ScriptObject.GetComponent<TicTacToeLogic>();
    }
          
    // Update is called once per frame
    void Update ()
    {
		
	}

    public void click()
    {
        if(logicScript.playerTurn == true)
        {
             if(logicScript.XO == false)
             {
             this.GetComponent<Image>().sprite = BlueX;
             this.GetComponent<Image>().color = blue;

                    if(this.CompareTag("TopLeft"))
                    {
                    logicScript.positions[0] = 0;
                    }

                    if (this.CompareTag("TopMiddle"))
                    {
                        logicScript.positions[1] = 0;
                    }
                    
                    if (this.CompareTag("TopRight"))
                    {
                        logicScript.positions[2] = 0;
                    }
                    
                    if (this.CompareTag("MiddleLeft"))
                    {
                        logicScript.positions[3] = 0;
                    }
                    
                    if (this.CompareTag("MiddleMiddle"))
                    {
                        logicScript.positions[4] = 0;
                    }
                    
                    if (this.CompareTag("MiddleRight"))
                    {
                        logicScript.positions[5] = 0;
                    }
                    
                    if (this.CompareTag("BottomLeft"))
                    {
                        logicScript.positions[6] = 0;
                    }
                    
                    if (this.CompareTag("BottomMiddle"))
                    {
                        logicScript.positions[7] = 0;
                    
                    }

                    if (this.CompareTag("BottomRight"))
                    {
                        logicScript.positions[8] = 0;
                    }
             }

             if(logicScript.XO == true)
             {
             this.GetComponent<Image>().sprite = BlueO;
             this.GetComponent<Image>().color = blue;

                if (this.CompareTag("TopLeft"))
                {
                    logicScript.positions[0] = 1;
                }

                if (this.CompareTag("TopMiddle"))
                {
                    logicScript.positions[1] = 1;
                }

                if (this.CompareTag("TopRight"))
                {
                    logicScript.positions[2] = 1;
                }

                if (this.CompareTag("MiddleLeft"))
                {
                    logicScript.positions[3] = 1;
                }

                if (this.CompareTag("MiddleMiddle"))
                {
                    logicScript.positions[4] = 1;
                }

                if (this.CompareTag("MiddleRight"))
                {
                    logicScript.positions[5] = 1;
                }

                if (this.CompareTag("BottomLeft"))
                {
                    logicScript.positions[6] = 1;
                }

                if (this.CompareTag("BottomMiddle"))
                {
                    logicScript.positions[7] = 1;
                }

                if (this.CompareTag("BottomRight"))
                {
                    logicScript.positions[8] = 1;
                }
             }

            if (logicScript.win() == 1)
                Debug.Log("X Wins!");

            else if (logicScript.win() == -1)
                Debug.Log("O Wins!");
        }
    }
}
