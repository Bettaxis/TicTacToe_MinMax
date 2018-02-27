using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class Space : MonoBehaviour
{
    //Sprite Images of the various Moves
    public Sprite RedX;
    public Sprite RedO;
    public Sprite BlueX;
    public Sprite BlueO;

    //Reference to UI Text to declare winner
    public Text winText;

    public GameObject ScriptObject;
    private TicTacToeLogic logicScript;
    public ParticleSystem particleToPlay;

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
        //Win condition checks
        if (TicTacToeLogic.win(logicScript.positions) == 1)
        {
            winText.text = "X Wins!";
            logicScript.playerTurn = false;
            logicScript.gameStart = false;
        }

        else if (TicTacToeLogic.win(logicScript.positions) == -1)
        {
            winText.text = "O Wins!";
            logicScript.playerTurn = false;
            logicScript.gameStart = false;
        }

        else if (TicTacToeLogic.isDraw(logicScript.positions))
        {
            winText.text = "It's a Draw!";
            logicScript.playerTurn = false;
            logicScript.gameStart = false;
        }
    }

    void LateUpdate()
    {
        //Check to see if it is the player's turn and if the game has started yet.
        //If true, let the AI player take the turn.
        if (logicScript.playerTurn == false && logicScript.gameStart == true)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            logicScript.aiAction();
            timer.Stop();
            UnityEngine.Debug.Log("Run Time of AI Function " + timer.ElapsedMilliseconds + "ms");
            logicScript.playerTurn = true;
        }
    }

    //Function called on board position click that determines what to display
    // and also updates the logic for the board in the positions[]
    public void click()
    {
        if(logicScript.playerTurn == true)
        {
             if(TicTacToeLogic.XO == false)
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

                logicScript.playerTurn = false;
            }

             if(TicTacToeLogic.XO == true)
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

                logicScript.playerTurn = false;
            }

            particleToPlay.Play();
        }
    }
}
