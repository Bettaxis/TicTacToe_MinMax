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
    // 0 = Min, 1 = Max
    private bool minMax = false;

    private int score = 0;

    //False = X, True = O
    public bool XorO = false;

    public Move()
    {

    }

    public Move(int s)
    {
        score = s;
    }



}

public class TicTacToeLogic : MonoBehaviour
{
    //Create initial array for boardstate
    Move[] boardStates = new Move[8];

    //Array to represent board positions
    public int[] positions = {-1,-1,-1,-1,-1,-1,-1,-1,-1};

    // 0 = Player X, 1 = Player O 
    public bool XO = false;

    // 1 = Player Turn, 0 = AI Turn 
    public bool playerTurn = true;


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

    public void SetX()
    {
        XO = false;
        playerTurn = true;
    }

    public void SetO()
    {
        XO = true;
        playerTurn = true;
    }

    //Function to calculate which move is the best
    public int utilityBoardState(/*Move m*/)
    {
        //Results in X Winning
        if (win() == 1)
        {
            return 10;
        }

        //Results in O Winning
        else if (win() == -1)
        {
            return -10;
        }

        //Results in Draw or Nothing Happening
        else if (win() == 0)
        {
            return 0;
        }

        else
            return 0;
    }

    public int win(/*Move m*/)
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

    //if (chance <= probability)
    //     this.GetComponent<Image>().sprite = safe;

    // else
    //     this.GetComponent<Image>().sprite = dead;


    //Debug.Log(dead);
    // Debug.Log(gameObject.GetComponent<Image>()); 
    //this.GetComponent<Image>().sprite = dead;
}


