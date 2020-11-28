using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

/*
 * L-system generating simple trees. General class for creating trees.
 * Takes 5 parameters: axiom, rules, rotation angle, iteration count, step length
 * Miia Remahl 
 * 23.11.2020
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Peter Philips - L-Systems Unity Tutorial [2018.1] : https://www.youtube.com/watch?v=tUbTGWl-qus
 * 
 */

//class for transform information
public class PlaceInfo
{
    //postion 
    public Vector3 position;

    //rotation
    public Quaternion rotation;
}

public class Tree : MonoBehaviour
{
    //reference to UI script
    public UI ui;

    //prefab of the branch
    public GameObject branch;

    //last amount of iterations made
    private float lastIteration = 5;

    //stack for all the place information
    private Stack<PlaceInfo> placeStack;

    //stack for all the branches (prefabs)
    private Stack<GameObject> branches;

    //dictionary for the rules 
    private Dictionary<char, string> rules;

    //starting character
    private string start = "X";

    //how many prefabs we have
    private int branchCount = 0;

    //does tree have rules set
    private bool rulesSet = false;

    //number of iterations
    public int iterations = 5;

    //how long is each line
    public float length = 10;

    //what was the last length used
    public float lastLength;

    //what angle will we turn
    public float angle = 25.7f;

    //what angle was last used
    public float lastAngle;

    //start position of the tree
    public Vector3 startingPoint = new Vector3(0.0f, 0.0f, 0.0f);

    //current string being processed
    private string currentString = string.Empty;


    void Start()
    {
        lastAngle = 0;
        lastLength = 0;
    }

    public void CreateTree(Dictionary<char, string> newRules, string axiom, int iterationVal, float lengthVal, float angleVal)
    {
        //create new stack for places
        placeStack = new Stack<PlaceInfo>();

        //create stack for prefabs
        branches = new Stack<GameObject>();

        //set values for rules, axiom, rotation, length, iterations
        rules = newRules;
        start = axiom;
        rulesSet = true;
        iterations = iterationVal;
        length = lengthVal;
        angle = angleVal;

        //set ui values
        SetUIValues();

        //generate the tree
        Generate();
    }

    //set UI values
    private void SetUIValues()
    {
        ui.setRotation(angle);
        ui.setIteration(iterations);
        ui.setLength(length);
    }

    void Update()
    {
        if (rulesSet)
        {
            //change steplength
            if (ui.length != length)
            {
                changeLength();
            }

            //change rotation angle
            if (ui.rotation != angle)
            {
                changeRotation();
            }

            //change iterations
            if (ui.iteration != iterations)
            {
                changeIterations();
            }
        }
    }

    //change length of step length
    private void changeLength()
    {
        length = ui.length;
        resetTree();
        Generate();

    }

    //change angle of the rotation
    private void changeRotation()
    {
        angle = ui.rotation;
        resetTree();
        Generate();
    }

    //change iterations
    private void changeIterations()
    {
        iterations = ui.iteration;
        resetTree();
        Generate();
    }


    private void Generate()
    {
        if (rules != null)
        {
            //set last iterations to the latest
            lastIteration = iterations;

            //set last angle to current angle
            lastAngle = angle;

            //set last length to current length
            lastLength = length;

            //set the current string to starting string
            currentString = start;

            //make a new string builder
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < iterations; i++)
            {
                foreach (char c in currentString)
                {
                    //check the rules and changes the current string based on that
                    sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                }
                currentString = sb.ToString();
                sb = new StringBuilder();
            }
            NextIteration(currentString);
        }
    }

    public void NextIteration(string currentString)
    {
        //go throught each character in currentString
        foreach (char c in currentString)
        {
            switch (c)
            {
                //F adds a branch
                case 'F':
                    addBranch();
                    break;

                //rotates to backwards
                case '+':
                    rotateBack();
                    break;

                //rotates to forward
                case '-':
                    rotateForward();
                    break;

                //pushes new information to stack
                case '[':
                    pushToStack();
                    break;

                //previously saved values
                case ']':
                    popFromStack();
                    break;

                //do nothing
                case 'X':
                    break;
                default:
                    giveError();
                    break;
            }
        }
    }

    //removes the old tree and makes a new one
    public void resetTree()
    {
        //reset position
        transform.position = startingPoint;

        //reset rotation
        transform.rotation = Quaternion.identity;

        //remove all the prefabs
        for (int i = 0; i < branchCount; i++)
        {
            GameObject branch = branches.Pop();
            branch.GetComponent<Branch>().Destroy();
        }

        //reset branch count
        branchCount = 0;
    }

    //rotates back using the global attribute angle
    public void rotateBack()
    {
        transform.Rotate(Vector3.back * angle);
    }

    //rotates forward using the global attribute angle
    public void rotateForward()
    {
        transform.Rotate(Vector3.forward * angle);
    }

    //pushes new information to stack
    public void pushToStack()
    {
        placeStack.Push(new PlaceInfo()
        {
            position = transform.position,
            rotation = transform.rotation
        });
    }

    //pops from stack information
    public void popFromStack()
    {
        PlaceInfo ti = placeStack.Pop();
        transform.position = ti.position;
        transform.rotation = ti.rotation;
    }

    //error for unknown values
    public void giveError()
    {
        Debug.Log("Invalid values given");
    }

    //adds a branch to the tree
    public void addBranch()
    {
        //take the position for the prefab
        Vector3 initialPosition = transform.position;

        //make a new position for the treespawn
        transform.Translate(Vector3.up * length);

        //creates a branch from prefab
        GameObject treeSegment = Instantiate(branch);

        //subposition
        treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);

        //final position --> current position of the treespawn
        treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);

        branchCount += 1;
        branches.Push(treeSegment);
    }
}
