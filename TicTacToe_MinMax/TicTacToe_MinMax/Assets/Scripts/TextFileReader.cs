using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using SFB;

//Enums to represent what the index in percentages list corresponds to.
//enum Doors
//{
//    Hot_Noisy_Safe = 0,
//    Hot_Noisy_NotSafe = 1,
//    Hot_NotNoisy_Safe = 2,
//    Hot_NotNoisy_NotSafe = 3,
//    NotHot_Noisy_Safe = 4,
//    NotHot_Noisy_NotSafe = 5,
//    NotHot_NotNoisy_Safe = 6,
//    NotHot_NotNoisy_NotSafe = 7,
//    Door_Selected = 8,
//};

public class TextFileReader : MonoBehaviour
{
    public Text textUI; // Link to textUI element

    public List<float> percentages = new List<float>(); // List of probabilities stored as floats

    public GameObject Doors;
    private DoorScript DoorScript;

    ////Function to open prompt a file dialog on program run and output the text to a UI element
    void Awake()
    {
        var path = StandaloneFileBrowser.OpenFilePanel("Select Probability Text Location!", "", "", false);

        if (path.Length != 0)
        {
            StreamReader reader = new StreamReader(path[0]);

            textUI.text = reader.ReadToEnd();

            reader = new StreamReader(path[0]);

            storeProbabilities(reader.ReadToEnd());
            reader.Close();
        }
    }

    //Function to open a file dialog from button press and output the text to a UI element
    public void LoadInfo()
    {
        var path = StandaloneFileBrowser.OpenFilePanel("Select Probability Text Location!", "", "", false);

        if (path.Length != 0)
        {
            StreamReader reader = new StreamReader(path[0]);

            textUI.text = reader.ReadToEnd();

            reader = new StreamReader(path[0]);

            storeProbabilities(reader.ReadToEnd());
            reader.Close();

            // For loop to make each door calculate probabilities again
            for (int i = 1; i <= 20; i++)
            {
                Doors = GameObject.Find("Door" + i);
                DoorScript = Doors.GetComponent<DoorScript>();
                DoorScript.calculateProbabilities();
            }
        }
    }

    //Function that parses the string obtained from the file and stores a list of the probabilities as floats in percentages list.
    public void storeProbabilities(string probs)
    {
        percentages.Clear(); //Deletes any previous entries in percentages list

        string probabilities = probs;

        // Splits the data into substrings with ' ' and newlines as parameters to split
        string[] subStrings = probabilities.Split(new char[] { ' ', '\n' },  StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < subStrings.Length; i++)
        {
            float temp;
            bool result = float.TryParse(subStrings[i], out temp);

            if (result)
            {
                percentages.Add(temp);
            }

        }
    }

    private void Update()
    {
       if (Input.GetKey("escape"))
           Application.Quit();
    }
}
