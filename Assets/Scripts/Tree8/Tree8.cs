using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * L-system generating simple trees.
 * Tree8: V -> "[+++W][---W]YV", W -> "+X[-W]Z", X -> "-W[+X]Z", Y -> "YZ", Z -> "[-FFF][+FFF]F"
 * Miia Remahl 
 * 23.11.2020
 * mrema003@gold.ac.uk
 * 
 */

public class Tree8 : MonoBehaviour
{
    //Generic tree class
    public TreeMain tree;

    //rule dictionary
    private Dictionary<char, string[]> rules;

    //axiom
    private string start;

    //what angle will we turn
    public float angle = 20f;

    //how long is each line
    public float length = 5;

    //number of iterations
    public int iterations = 10;

    //max iterations
    public int iterationMax;

    void Start()
    {
        createTree();
    }

    //creates a tree by calling the general tree class
    private void createTree()
    {
        //rules for V
        string[] vRules = { "[+++W][---W]YV"};

        //rules for W
        string[] wRules = {"+X[-W]Z"};

        //rules for X
        string[] xRules = { "-W[+X]Z" };

        //rules for Y
        string[] yRules = { "YZ" };

        //rules for Z
        string[] zRules = { "[-FFF][+FFF]F" };

        //rules
        rules = new Dictionary<char, string[]>
        {
            {'V', vRules },
            {'W', wRules },
            {'X', xRules },
            {'Y', yRules },
            {'Z', zRules },
        };

        //set max iterations
        iterationMax = 11;

        //start
        start = "VZFFF";

        //create tree with generic tree class
        tree.CreateTree(rules, start, iterations, length, angle, iterationMax);
    }

    
}
