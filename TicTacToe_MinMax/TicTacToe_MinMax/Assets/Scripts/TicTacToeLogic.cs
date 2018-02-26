using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

enum TicTacToe
{
    /* Enum to represent the sectors on the board
     * 0|1|2
     * 3|4|5
     * 6|7|8
     */

    Top_Left = 0,
    Top_Middle= 1,
    Top_Right = 2,
    Middle_Left = 3,
    Middle_Middle = 4,
    Middle_Right = 5,
    Bottom_Left  = 6,
    Bottom_Middle = 7,
    Bottom_Right = 8,
};


// Uses a class to represent each Move
public class Move
{
    // False = Min, True = Max
    private bool minMax = false;

    private int score = 0;

    private int depth = 0;
    private int depthLevel = 0;

    public int[] positionsTemp = { 0 };

    //Int to keep track of which node changed
    public int changedIndex = -1;


    public List<Move> boardStates = new List<Move>();

    public Move(int[] p, bool minMaxTemp, int d)
    {
        positionsTemp = p;
        minMax = minMaxTemp;

        depth = d;
        // False = Player X, 
        // True = Player O 
        switch (TicTacToeLogic.win(positionsTemp))
        {
            //X Wins
             case 1:
                if (TicTacToeLogic.XO == false)
                {
                    score = -10 + depth;
                    break;
                }
   
                else
                {
                    score = 10 - depth;
                    break;
                }

            //O Wins
            case -1:
                if (TicTacToeLogic.XO == true)
                {
                    score = -10 + depth;
                    break;
                }

                else
                {
                    score = 10 - depth;
                    break;
                }

            // Draw or Neither
            case 0:
                if (TicTacToeLogic.isDraw(positionsTemp))
                {
                    score = 0;
                }
     
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (positionsTemp[i] == -1)
                        {
                            int[] positionsToSend = new int[9];
                            System.Array.Copy(positionsTemp, positionsToSend, 9);

                            // False = Player X, 
                            // True = Player O 
                      
                            //Positions -1 is Empty
                            //Positions 0 is an X
                            //Positions 1  is an O

            
                            if (TicTacToeLogic.XO)
                            {
                         
                                if (minMax)
                                {
                                    positionsToSend[i] = 0;
                                }

                                else
                                {
                                    positionsToSend[i] = 1;
                                }
                            }

                            else 
                            {

                                if (minMax)
                                {
                                    positionsToSend[i] = 1;
                                }

                                else
                                {
                                    positionsToSend[i] = 0;
                                }
                            }

                            Move tempMove = new Move(positionsToSend, !minMax, depth+1);
                            tempMove.changedIndex = i;
                            boardStates.Add(tempMove);
                        }
                    }

                    Move winningNode = getWinningMove();
                    score = winningNode.score;
                    depthLevel = winningNode.depth;
                }
                  break; 
        }


    }
    
    //Function to For Loop through BoardStates to Choose Best/Worst Move
    public Move getWinningMove()//bool lastLevel)
    {
        Move winningMove = boardStates[0];

        for (int i = 1; i < boardStates.Count; i++)
        {

            if (minMax) // False = Min, True = Max
            {
                if (boardStates[i].getScore() > winningMove.getScore())
                {
                    winningMove = boardStates[i];
                }
            }

            else
            {
                if (boardStates[i].getScore() < winningMove.getScore())
                {
                    winningMove = boardStates[i];
                }
            }
        }
        return winningMove;
    }

    public int getScore()
    {
        return score;
    }

    public int getFinalScore()
    {
        return score - depthLevel;
    }

}

public class TicTacToeLogic : MonoBehaviour
{
    //References to Sprites to Use
    public Sprite RedX;
    public Sprite RedO;

    public Sprite BlueX;
    public Sprite BlueO;

    private Color blue = Color.blue;
    private Color red = Color.red;

    //Array to represent board positions
    public int[] positions = {-1,-1,-1,-1,-1,-1,-1,-1,-1};

    // False = Player X, 
    // True = Player O 
    public static bool XO = false;

    // 1 = Player Turn, 0 = AI Turn 
    public bool playerTurn = true;

    public bool gameStart = false;


    public ParticleSystem placementParticle;

    //public GameObject ScriptObject;
    //private TextFileReader probabilitiesScript;

    //Doors = GameObject.Find("Door" + i);

    // Use this for initialization
    void Start()
    {
        //ScriptObject = GameObject.FindWithTag("Script Object");
        //probabilitiesScript = ScriptObject.GetComponent<TextFileReader>();
        //calculateProbabilities();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public static bool isDraw(int[] p)
    {
        //Positions -1 is Empty
        //Positions 0 is an X
        //Positions 1  is an O
        for (int i = 0; i < 9; i++)
        {
            if (p[i] == -1)
            {
                return false;
            }
        }
    
        return true;
    }

    public void SetX()
    {
        XO = false;
        playerTurn = true;
        gameStart = true;
    }

    public void SetO()
    {
        XO = true;
        playerTurn = false;
        gameStart = true;
    }

    //Function to calculate which move is the best
    public static int utilityBoardState(Move m)
    {
        //Results in X Winning
        if (win(m.positionsTemp) == 1)
        {
            return 10;
        }

        //Results in O Winning
        else if (win(m.positionsTemp) == -1)
        {
            return -10;
        }

        //Results in Draw or Nothing Happening
        else if (win(m.positionsTemp) == 0)
        {
            return 0;
        }

        else
            return 0;
    }
    /*
   public int win(Move m)
    {
        // Horizontal X Wins
        if (positions[(int)TicTacToe.Top_Left] == 0 && positions[(int)TicTacToe.Top_Middle] == 0 && positions[(int)TicTacToe.Top_Right] == 0)
            return 1;

        else if (positions[(int)TicTacToe.Middle_Left] == 0 && positions[(int)TicTacToe.Middle_Middle] == 0 && positions[(int)TicTacToe.Middle_Right] == 0)
            return 1;

        else if (positions[(int)TicTacToe.Bottom_Left] == 0 && positions[(int)TicTacToe.Bottom_Middle] == 0 && positions[(int)TicTacToe.Bottom_Right] == 0)
            return 1;

        // Diagonal X Wins
        else if (positions[(int)TicTacToe.Top_Left] == 0 && positions[(int)TicTacToe.Middle_Middle] == 0 && positions[(int)TicTacToe.Bottom_Right] == 0)
            return 1;

        else if (positions[(int)TicTacToe.Top_Right] == 0 && positions[(int)TicTacToe.Middle_Middle] == 0 && positions[(int)TicTacToe.Bottom_Left] == 0)
            return 1;


        // Vertical X Wins
        else if (positions[(int)TicTacToe.Top_Left] == 0 && positions[(int)TicTacToe.Middle_Left] == 0 && positions[(int)TicTacToe.Bottom_Left] == 0)
            return 1;

        else if (positions[(int)TicTacToe.Top_Middle] == 0 && positions[(int)TicTacToe.Middle_Middle] == 0 && positions[(int)TicTacToe.Bottom_Middle] == 0)
            return 1;

        else if (positions[(int)TicTacToe.Top_Right] == 0 && positions[(int)TicTacToe.Middle_Right] == 0 && positions[(int)TicTacToe.Bottom_Right] == 0)
            return 1;

        // Horizontal O Wins
        else if (positions[(int)TicTacToe.Top_Left] == 1 && positions[(int)TicTacToe.Top_Middle] == 1 && positions[(int)TicTacToe.Top_Right] == 1)
            return -1;

        else if (positions[(int)TicTacToe.Middle_Left] == 1 && positions[(int)TicTacToe.Middle_Middle] == 1 && positions[(int)TicTacToe.Middle_Right] == 1)
            return -1;

        else if (positions[(int)TicTacToe.Bottom_Left] == 1 && positions[(int)TicTacToe.Bottom_Middle] == 1 && positions[(int)TicTacToe.Bottom_Right] == 1)
            return -1;

        // Diagonal O Wins
        else if (positions[(int)TicTacToe.Top_Left] == 1 && positions[(int)TicTacToe.Middle_Middle] == 1 && positions[(int)TicTacToe.Bottom_Right] == 1)
            return -1;

        else if (positions[(int)TicTacToe.Top_Right] == 1 && positions[(int)TicTacToe.Middle_Middle] == 1 && positions[(int)TicTacToe.Bottom_Left] == 1)
            return -1;


        // Vertical O Wins
        else if (positions[(int)TicTacToe.Top_Left] == 1 && positions[(int)TicTacToe.Middle_Left] == 1 && positions[(int)TicTacToe.Bottom_Left] == 1)
            return -1;

        else if (positions[(int)TicTacToe.Top_Middle] == 1 && positions[(int)TicTacToe.Middle_Middle] == 1 && positions[(int)TicTacToe.Bottom_Middle] == 1)
            return -1;

        else if (positions[(int)TicTacToe.Top_Right] == 1 && positions[(int)TicTacToe.Middle_Right] == 1 && positions[(int)TicTacToe.Bottom_Right] == 1)
            return -1;

        
        else return 0;
    }
    */

    public static int win(int[] pos)
    {
        // Horizontal X Wins
        if (pos[(int)TicTacToe.Top_Left] == 0 && pos[(int)TicTacToe.Top_Middle] == 0 && pos[(int)TicTacToe.Top_Right] == 0)
            return 1;

        else if (pos[(int)TicTacToe.Middle_Left] == 0 && pos[(int)TicTacToe.Middle_Middle] == 0 && pos[(int)TicTacToe.Middle_Right] == 0)
            return 1;

        else if (pos[(int)TicTacToe.Bottom_Left] == 0 && pos[(int)TicTacToe.Bottom_Middle] == 0 && pos[(int)TicTacToe.Bottom_Right] == 0)
            return 1;

        // Diagonal X Wins
        else if (pos[(int)TicTacToe.Top_Left] == 0 && pos[(int)TicTacToe.Middle_Middle] == 0 && pos[(int)TicTacToe.Bottom_Right] == 0)
            return 1;

        else if (pos[(int)TicTacToe.Top_Right] == 0 && pos[(int)TicTacToe.Middle_Middle] == 0 && pos[(int)TicTacToe.Bottom_Left] == 0)
            return 1;


        // Vertical X Wins
        else if (pos[(int)TicTacToe.Top_Left] == 0 && pos[(int)TicTacToe.Middle_Left] == 0 && pos[(int)TicTacToe.Bottom_Left] == 0)
            return 1;

        else if (pos[(int)TicTacToe.Top_Middle] == 0 && pos[(int)TicTacToe.Middle_Middle] == 0 && pos[(int)TicTacToe.Bottom_Middle] == 0)
            return 1;

        else if (pos[(int)TicTacToe.Top_Right] == 0 && pos[(int)TicTacToe.Middle_Right] == 0 && pos[(int)TicTacToe.Bottom_Right] == 0)
            return 1;

        // Horizontal O Wins
        else if (pos[(int)TicTacToe.Top_Left] == 1 && pos[(int)TicTacToe.Top_Middle] == 1 && pos[(int)TicTacToe.Top_Right] == 1)
            return -1;

        else if (pos[(int)TicTacToe.Middle_Left] == 1 && pos[(int)TicTacToe.Middle_Middle] == 1 && pos[(int)TicTacToe.Middle_Right] == 1)
            return -1;

        else if (pos[(int)TicTacToe.Bottom_Left] == 1 && pos[(int)TicTacToe.Bottom_Middle] == 1 && pos[(int)TicTacToe.Bottom_Right] == 1)
            return -1;

        // Diagonal O Wins
        else if (pos[(int)TicTacToe.Top_Left] == 1 && pos[(int)TicTacToe.Middle_Middle] == 1 && pos[(int)TicTacToe.Bottom_Right] == 1)
            return -1;

        else if (pos[(int)TicTacToe.Top_Right] == 1 && pos[(int)TicTacToe.Middle_Middle] == 1 && pos[(int)TicTacToe.Bottom_Left] == 1)
            return -1;


        // Vertical O Wins
        else if (pos[(int)TicTacToe.Top_Left] == 1 && pos[(int)TicTacToe.Middle_Left] == 1 && pos[(int)TicTacToe.Bottom_Left] == 1)
            return -1;

        else if (pos[(int)TicTacToe.Top_Middle] == 1 && pos[(int)TicTacToe.Middle_Middle] == 1 && pos[(int)TicTacToe.Bottom_Middle] == 1)
            return -1;

        else if (pos[(int)TicTacToe.Top_Right] == 1 && pos[(int)TicTacToe.Middle_Right] == 1 && pos[(int)TicTacToe.Bottom_Right] == 1)
            return -1;


        else return 0;
    }

    public void aiAction()
    {
        Move ai = new Move(positions, true, 0);
        Move nextAiMove = ai.getWinningMove();
        int changedBoardIndex = nextAiMove.changedIndex;

        if (XO)
        {
            positions[changedBoardIndex] = 0;
        }

        else
        {
            positions[changedBoardIndex] = 1;
        }

        GameObject g = null;
        if (XO) //AI = X
        {
            switch (changedBoardIndex)
            {
                case 0:
                    g = GameObject.FindGameObjectWithTag("TopLeft");
                    break;
                case 1:
                    g = GameObject.FindGameObjectWithTag("TopMiddle");
                    break;
                case 2:
                    g = GameObject.FindGameObjectWithTag("TopRight");
                    break;
                case 3:
                    g = GameObject.FindGameObjectWithTag("MiddleLeft");
                    break;
                case 4:
                    g = GameObject.FindGameObjectWithTag("MiddleMiddle");
                    break;
                case 5:
                    g = GameObject.FindGameObjectWithTag("MiddleRight");
                    break;
                case 6:
                    g = GameObject.FindGameObjectWithTag("BottomLeft");
                    break;
                case 7:
                    g = GameObject.FindGameObjectWithTag("BottomMiddle");
                    break;
                case 8:
                    g = GameObject.FindGameObjectWithTag("BottomRight");
                    break;
            }

            g.GetComponent<Image>().sprite = RedX;
            g.GetComponent<Image>().color = red;
        }


        if (!XO) //AI = O
        {
            switch (changedBoardIndex)
            {
                case 0:
                    g = GameObject.FindGameObjectWithTag("TopLeft");
                    break;
                case 1:
                    g = GameObject.FindGameObjectWithTag("TopMiddle");
                    break;
                case 2:
                    g = GameObject.FindGameObjectWithTag("TopRight");
                    break;
                case 3:
                    g = GameObject.FindGameObjectWithTag("MiddleLeft");
                    break;
                case 4:
                    g = GameObject.FindGameObjectWithTag("MiddleMiddle");
                    break;
                case 5:
                    g = GameObject.FindGameObjectWithTag("MiddleRight");
                    break;
                case 6:
                    g = GameObject.FindGameObjectWithTag("BottomLeft");
                    break;
                case 7:
                    g = GameObject.FindGameObjectWithTag("BottomMiddle");
                    break;
                case 8:
                    g = GameObject.FindGameObjectWithTag("BottomRight");
                    break;
            }

            g.GetComponent<Image>().sprite = RedO;
            g.GetComponent<Image>().color = red;
        }
    }
}


