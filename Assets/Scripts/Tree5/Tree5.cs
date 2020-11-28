using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * L-system generating simple trees.
 * Tree5: rules 'X -> F[+X][-X]FX, F -> FF'
 * Miia Remahl 
 * 23.11.2020
 * mrema003@gold.ac.uk
 * 
 */

public class Tree5 : MonoBehaviour
{
    //Generic tree class
    public Tree tree;

    //rule dictionary
    private Dictionary<char, string> rules;

    //what angle will we turn
    public float angle = 25.7f;

    //how long is each line
    public float length = 1;

    //number of iterations
    public int iterations = 7;

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
            {'X', "F[+X][-X]FX" }
        };

        //start
        string start = "X";

        //create tree with generic tree class
        tree.CreateTree(rules, start, iterations, length, angle);
    }
}
