using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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


// Uses a class to represent each Move/Node of the tree
public class Move
{
    // False = Min Player, True = Max Player
    private bool minMax = false;

    private int score = 0;

    private int depth = 0;

    public int[] positionsTemp = { 0 };

    //Int to keep track of which move has changed
    public int changedIndex = -1;


    public List<Move> boardStates = new List<Move>();

    public Move(int[] p, bool minMaxTemp, int d)
    {
        positionsTemp = p; //Copy the current boardstate
        minMax = minMaxTemp; //Determine if it is a Min or Max player
        depth = d; // Int to hold the depth of the move.

        //Switch to assign scores to a move if it results in a win, loss or draw.
        switch (TicTacToeLogic.win(positionsTemp))
        {
            //Case for X Wins
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

            //Case for O Wins
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

            //Case for Draw or Neither. If draw, return score of 0.
            //If the game is still playable, generate a tree of nodes and evaluate which one to use.
            case 0:
                if (TicTacToeLogic.isDraw(positionsTemp))
                {
                    score = 0;
                }
     
                else
                {
                    for (int i = 0; i < 9; i++) //Iterating through the boardstate positions
                    {
                        if (positionsTemp[i] == -1) //If the space is empty
                        {
                            int[] positionsToSend = new int[9];
                            System.Array.Copy(positionsTemp, positionsToSend, 9); 

                            // False = Player X, 
                            // True = Player O 
                            //Positions -1 is Empty
                            //Positions 0 is an X
                            //Positions 1  is an O

                            //Checks if the CPU is a X or O player to determine what to place in positions[]
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
                            //Creates a new move/node with a new positions[], flipped minMax for the next layer and a depth of +1 as it is going deeper.
                            Move tempMove = new Move(positionsToSend, !minMax, depth+1);
                            tempMove.changedIndex = i;
                            boardStates.Add(tempMove);
                        }
                    }
                    //Assigns the move with the most value to winningMode.
                    Move winningMove = getWinningMove();
                    score = winningMove.score;
                }
                  break; 
        }


    }
    
    //Function to For Loop through BoardStates to Choose Best/Worst Move
    public Move getWinningMove()
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


}

public class TicTacToeLogic : MonoBehaviour
{
    //References to Sprites to Use
    public Sprite RedX;
    public Sprite RedO;

    public Sprite BlueX;
    public Sprite BlueO;

    //Reference to color settings
    private Color blue = Color.blue;
    private Color red = Color.red;

    //Array to represent board positions 
    //Positions -1 is Empty
    //Positions 0 is an X
    //Positions 1  is an O
    public int[] positions = {-1,-1,-1,-1,-1,-1,-1,-1,-1};

    // Bool to track if the human player is X or O
    // False = Player X, 
    // True = Player O 
    public static bool XO = false;

    // True = Player Turn, False = AI Turn 
    public bool playerTurn = true;

    public bool gameStart = false;

    //Reference to the particle to play when placing a X or O
    public ParticleSystem placementParticle;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    //Function that checks if there are any empty spaces in my actual boardstate
    // if there are none then it is a draw
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
        if (!gameStart)
        {
            XO = false;
            playerTurn = true;
            gameStart = true;
        }
    }

    public void SetO()
    {
        if (!gameStart)
        {
            XO = true;
            playerTurn = false;
            gameStart = true;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Function that checks current positions array to determine if there are any winning combinations 
    // and returns an int representing if X(1) or O(-1) won or no winner/draw(0)
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

    //Function that is called when it is the AI's turn.
    //Creates a root move node and then after the tree is built,
    //executes the winning move which is the move with the highest score.
    public void aiAction()
    {
        Move ai = new Move(positions, true, 0);
        Move nextAiMove = ai.getWinningMove();
        int changedBoardIndex = nextAiMove.changedIndex;

        // If statments that determine the actual move chosen by the AI should be an X or O 
        //and reflects that in the positions[].
        if (XO)
        {
            positions[changedBoardIndex] = 0;
        }

        else
        {
            positions[changedBoardIndex] = 1;
        }

        //Game Object to hold the Reference to the physical board space
        GameObject g = null;

        if (XO) //True when AI = X
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


        if (!XO) //True when AI = O
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


