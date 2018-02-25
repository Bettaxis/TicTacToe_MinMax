using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using UnityEngine.UI;
using System;

//Enums to represent what the index in percentages list corresponds to.
enum Doors
{
    Hot_Noisy_Safe = 0,
    Hot_Noisy_NotSafe = 1,
    Hot_NotNoisy_Safe = 2,
    Hot_NotNoisy_NotSafe = 3,
    NotHot_Noisy_Safe = 4,
    NotHot_Noisy_NotSafe = 5,
    NotHot_NotNoisy_Safe = 6,
    NotHot_NotNoisy_NotSafe = 7,
    Door_Selected = 8,
};

public class DoorScript : MonoBehaviour {

    bool safeDoor; // Bool to track the safety of a door

    //Sprite Images of the various doors
    public Sprite safe;
    public Sprite dead;

    public Sprite hot;
    public Sprite noisy;
    public Sprite hotnoisy;
    public Sprite nothot_notnoisy;

    public ParticleSystem musicParticle;

    public GameObject ScriptObject;
    private TextFileReader probabilitiesScript;


    // Use this for initialization
    void Start()
    {
        ScriptObject = GameObject.FindWithTag("Script Object");
        probabilitiesScript = ScriptObject.GetComponent<TextFileReader>();


        calculateProbabilities();
    }

    //Function to calculate which door is generated using
    //Unity's Random function and comparing it to the percentages provided
    public void calculateProbabilities()
    {
        ScriptObject = GameObject.FindWithTag("Script Object");
        probabilitiesScript = ScriptObject.GetComponent<TextFileReader>();

        float chance = UnityEngine.Random.value; //Generates the rng chance of that door.
        Debug.Log("Chance " + chance);

        float[] ranges = {0,0,0,0,0,0,0,0}; // Setting initial values of the ranges to 0

        //Loop to sum the probabilities into ranges[] correctly
        for (int r = 0; r < 8; r++)
        {
            if (r == 0)
            {
                ranges[r] += probabilitiesScript.percentages[r];
            }

            else if (r > 0) // Adds probability to previous range.
            {
               ranges[r] = probabilitiesScript.percentages[r] + ranges[r - 1];
            }

            Debug.Log("Ranges " + r + " is " + ranges[r]);
        }

        for (int i = 0; i < 8; i++)
        {
            float probability = probabilitiesScript.percentages[i];
            Debug.Log("Probability " + probability);
       

                if (i == (int)Doors.Hot_Noisy_Safe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = hotnoisy;
                    this.GetComponent<ParticleSystem>().Play();
                    safeDoor = true;
                    break;
                }

                else if (i == (int)Doors.Hot_Noisy_NotSafe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = hotnoisy;
                    this.GetComponent<ParticleSystem>().Play();
                    safeDoor = false;
                    break;
                }

                else if (i == (int)Doors.Hot_NotNoisy_Safe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = hot;
                    this.GetComponent<ParticleSystem>().Stop();
                    safeDoor = true;
                    break;
                }

                else if (i == (int)Doors.Hot_NotNoisy_NotSafe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = hot;
                    this.GetComponent<ParticleSystem>().Stop();
                    safeDoor = false;
                    break;
                }

                else if (i == (int)Doors.NotHot_Noisy_Safe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = noisy;
                    this.GetComponent<ParticleSystem>().Play();
                    safeDoor = true;
                    break;
                }

                else if (i == (int)Doors.NotHot_Noisy_NotSafe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = noisy;
                    this.GetComponent<ParticleSystem>().Play();
                    safeDoor = false;
                    break;
                }

                else if (i == (int)Doors.NotHot_NotNoisy_Safe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = nothot_notnoisy;
                    this.GetComponent<ParticleSystem>().Stop();
                    safeDoor = true;
                    break;
                }

                else if (i == (int)Doors.NotHot_NotNoisy_NotSafe && (chance < ranges[i]))
                {
                    this.GetComponent<Image>().sprite = nothot_notnoisy;
                    this.GetComponent<ParticleSystem>().Stop();
                    safeDoor = false;
                    break;
                }
            
        }

    //if (chance <= probability)
    //     this.GetComponent<Image>().sprite = safe;

    // else
    //     this.GetComponent<Image>().sprite = dead;


    //Debug.Log(dead);
    // Debug.Log(gameObject.GetComponent<Image>()); 
    //this.GetComponent<Image>().sprite = dead;
    }

    public void isDoorSafe()
    {
        if (safeDoor)
        {
            this.GetComponent<Image>().sprite = safe;
        }

        else if (!safeDoor)
        {
            this.GetComponent<Image>().sprite = dead;
        }
    }
}
