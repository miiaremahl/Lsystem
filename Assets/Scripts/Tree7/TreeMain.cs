using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;


/*
 * L-system generating simple trees. General class for creating trees.
 * Takes 5 parameters: axiom, rules, rotation angle, iteration count, step length
 * Miia Remahl 
 * 23.11.2020
 * mrema003@gold.ac.uk
 * 
 * 
 */

public class TreeMain : MonoBehaviour
{
    //reference to UI script
    public UI ui;

    //stores different iterations
    public string[] iterationTable;

    //prefab of the branch
    public GameObject branch;

    //Trree is generated once -> rules for the tree are known
    private bool generatedOnce;

    //last amount of iterations made
    private float lastIteration = 5;

    //stack for all the place information
    private Stack<PlaceInfo> placeStack;

    //stack for all the branches (prefabs)
    private Stack<GameObject> branches;

    //dictionary for the rules 
    private Dictionary<char, string[]> rules;

    //reference to mixing button
    public Button mixButton;

    //starting character
    private string start = "X";

    //how many prefabs we have
    private int branchCount = 0;

    //does tree have rules set
    private bool rulesSet = false;

    //number of iterations
    public int iterations = 5;

    //max iterations
    public int maxIteration;

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
        //set last length and angle to 0
        lastAngle = 0;
        lastLength = 0;
    }

    public void CreateTree(Dictionary<char, string[]> newRules, string axiom, int iterationVal, float lengthVal, float angleVal, int iterationMax)
    {
        //reset location
        resetTree();
        generatedOnce = false;

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
        maxIteration = iterationMax;

        //set ui values
        SetUIValues();

        //generate the tree
        Generate();

    }

    //setting the UI values
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

            //set last angle to current angle
            lastAngle = angle;

            //set last length to current length
            lastLength = length;

            //set the current string to starting string
            currentString = start;

            //make a new string builder
            StringBuilder sb = new StringBuilder();

            //check if tree was generated once (are there rules allready)
            if (generatedOnce)
            {
                //check if we want less iterations
                if (lastIteration > iterations)
                {
                    if (iterations==0)
                    {
                        currentString = start;
                    }
                    else if(iterationTable[iterations - 1] != null)
                    {
                        //take from stored iterations
                        currentString = iterationTable[iterations - 1];
                        NextIteration(currentString);
                    }
                }
                else
                {
                    //take stored iteration and add the rest 
                    currentString = iterationTable[(int)lastIteration - 1];
                    for (int i = (int)lastIteration; i < iterations; i++)
                    {
                        foreach (char c in currentString)
                        {
                            //check if has a rule in dictionary
                            if (rules.ContainsKey(c))
                            {
                                //does it have more than 1 rule
                                if (rules[c].Length > 0)
                                {
                                    //every rule has an equal opportunity to be picked
                                    sb.Append(rules[c][giveRandom(0, rules[c].Length)]);
                                }
                                else
                                {
                                    sb.Append(rules[c][0]);
                                }
                            }
                            else
                            {
                                sb.Append(c.ToString());
                            }

                        }
                        currentString = sb.ToString();
                        //store the iteration
                        iterationTable[i] = currentString;
                        sb = new StringBuilder();
                    }
                    NextIteration(currentString);
                    generatedOnce = true;

                    //set last iterations to the latest
                    lastIteration = iterations;
                }
            }
            else
            {
                string[] newTable = new string[maxIteration];
                iterationTable = newTable;
                for (int i = 0; i < iterations; i++)
                {
                    foreach (char c in currentString)
                    {
                        //check if has a rule in dictionary
                        if (rules.ContainsKey(c))
                        {
                            //does it have more than 1 rule
                            if (rules[c].Length > 0)
                            {
                                //every rule has an equal opportunity to be picked
                                sb.Append(rules[c][giveRandom(0, rules[c].Length)]);
                            }
                            else
                            {
                                sb.Append(rules[c][0]);
                            }
                        }
                        else
                        {
                            sb.Append(c.ToString());
                        }

                    }
                    currentString = sb.ToString();
                    //store the iteration
                    iterationTable[i] = currentString;
                    sb = new StringBuilder();
                }
                NextIteration(currentString);
                generatedOnce = true;

                //set last iterations to the latest
                lastIteration = iterations;
            }
          
        }
    }

    //for counting probavilities
    private int giveRandom(int first,int second)
    {
        System.Random random = new System.Random();
        return random.Next(first, second);
    }

    //Reset tree -> creates a new one with new random values
    public void MixTree()
    {
        generatedOnce = false;
        resetTree();
        Generate();
        StartCoroutine(buttonCoolDown());
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

                //do nothing
                case 'W':
                    break;

                //do nothing
                case 'V':
                    break;

                //do nothing
                case 'Y':
                    break;

                //do nothing
                case 'Z':
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

    //coroutine to cooldown the mixing button
    IEnumerator buttonCoolDown()
    {
        mixButton.interactable = false;
        yield return new WaitForSeconds(2);
        mixButton.interactable = true;
    }
    
}
