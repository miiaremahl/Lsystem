using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    //length 
    public float length;

    //rotation
    public float rotation;

    //iteration
    public int iteration;

    //ref dropdown length
    public TMP_Dropdown dropdown;

    //ref dropdown rotation
    public TMP_Dropdown rotationDrop;

    //ref dropdown iteration
    public TMP_Dropdown iterationDrop;

    //dictionary for the rotation 
    private Dictionary<float, int> rotationFloatRules;


    private Dictionary<int, float> rotationRules;

    //refrence to main camera
    public Camera camera;


  
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            camera.fieldOfView -= 1;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            camera.fieldOfView += 1;
        }
    }


    //handles input changes for length
    public void HandleInputData(int val)
    {
        length = (float)(val + 1);
    }

    //create rule dictionaries
    public void createDictionaries()
    {
        //give the rules for the rotation dictionary (int,float)
        rotationRules = new Dictionary<int, float>
        {
            {0,10f},
            {1,15f},
            {2,20f},
            {3,22.5f},
            {4,25.7f},
            {5,35f},
            {6,50f},
            {7,60f},
            {8,80f},
            {9,100f},
        };

        //give the rules for the rotation dictionary (float, int)
        rotationFloatRules = new Dictionary<float, int>
        {
            {10f, 0},
            {15f, 1},
            {20f, 2},
            {22.5f, 3},
            {25.7f, 4},
            {35f, 5},
            {50f, 6},
            {60f, 7},
            {80f, 8},
            {100f, 9},
        };
    }

    //handles input changes in iterations
    public void HandleIterationChange(int val)
    {
        iteration = val;
    }


    //handles rotation change
    public void HandleRotationChange(int val)
    {
        if (rotationRules != null)
        {
            rotation = rotationRules.ContainsKey(val) ? rotationRules[val] : 0;
        }
    }


    //for setting length
    public void setLength(float val)
    {
        length = val;
        dropdown.value = (int) (val-1);
    }


    //for setting length
    public void setIteration(int val)
    {
        iteration = val;
        iterationDrop.value = val;
    }


    //for setting rotation value
    public void setRotation(float val)
    {
        createDictionaries();
        //set the dropdown value
        if (rotationFloatRules != null)
        {
            rotationDrop.value = rotationFloatRules.ContainsKey(val) ? rotationFloatRules[val] : 0;
        }
    }
}
