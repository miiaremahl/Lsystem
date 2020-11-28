using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * L-system generating simple trees.
 * Tree6: rules 'X -> F-[[X]+X]+F[+FX]-X, F -> FF'
 * Miia Remahl 
 * 23.11.2020
 * mrema003@gold.ac.uk
 * 
 */

public class Tree6 : MonoBehaviour
{
    //Generic tree class
    public Tree tree;

    //rule dictionary
    private Dictionary<char, string> rules;

    //what angle will we turn
    public float angle = 22.5f;

    //how long is each line
    public float length = 3;

    //number of iterations
    public int iterations = 5;

    void Start()
    {
        createTree();
    }

    private void createTree()
    {
        //rules
        rules = new Dictionary<char, string>
        {
            {'F', "FF" },
            {'X', "F-[[X]+X]+F[+FX]-X" }
        };

        //start
        string start = "X";

        //create tree with generic tree class
        tree.CreateTree(rules, start, iterations, length, angle);
    }
}
