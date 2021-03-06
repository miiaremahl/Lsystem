﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * L-system generating simple trees.
 * Tree3: rule 'F -> FF-[-F+F+F]+[+F-F-F]'
 * Miia Remahl 
 * 23.11.2020
 * mrema003@gold.ac.uk
 * 
 */

public class Tree3 : MonoBehaviour
{
    //Generic tree class
    public Tree tree;

    //rule dictionary
    private Dictionary<char, string> rules;

    //what angle will we turn
    public float angle = 22.5f;

    //how long is each line
    public float length = 1;

    //number of iterations
    public int iterations = 4;

    void Start()
    {
        createTree();
    }

    private void createTree()
    {
        //rules
        rules = new Dictionary<char, string>
        {
            {'F', "FF-[-F+F+F]+[+F-F-F]" }
        };

        //start
        string start = "F";

        //create tree with generic tree class
        tree.CreateTree(rules, start, iterations, length, angle);
    }
}
