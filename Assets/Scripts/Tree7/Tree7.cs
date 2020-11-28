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
 * Tree7: randomly generated trees and 0.33% probability trees
 * Miia Remahl 
 * 23.11.2020
 * mrema003@gold.ac.uk
 * 
 */

public class Tree7 : MonoBehaviour
{
    //Generic tree class
    public TreeMain tree;

    //reference to random tree button
    public Button randomButton;

    //reference to the rule text
    [SerializeField] TextMeshProUGUI ruleText;

    //reference to the axiom text
    [SerializeField] TextMeshProUGUI axiomText;

    //rule dictionary
    private Dictionary<char, string[]> rules;

    //contains tree rules for different trees
    private Dictionary<char, string[]>[] randomRules;

    //start rules for random trees
    private string[] startRules;

    //axiom
    private string start;

    //what angle will we turn
    public float angle = 20f;

    //how long is each line
    public float length = 1;

    //max iterations
    public int iterationMax;

    //number of iterations
    public int iterations = 5;

    void Start()
    {
        //max iterations
        iterationMax = 5;

        //create tree
        createTree();

        //greate random tree storage
        string[] fRules = { "F[+F]F[+F][F]", "F[+F]F[-F][F]", "F[-F]F[-F][F]" };

        Dictionary<char, string[]> d1= new Dictionary<char, string[]>
        {
            {'F', fRules }
        };

        string[] fRules2 = { "FF" };
        string[] XRules2 = { "F-[[X]+X]+F[+FX]-X", "F-[[X]-X]-F[+FX]-X", "F-[[X]+X]+F[+FX]+X" };

        Dictionary<char, string[]> d2 = new Dictionary<char, string[]>
        {
            { 'F', fRules2 },
            { 'X', XRules2 }
        };

        string[] XRules3 = { "F[+X][-X]FX", "F[+X][+X]FX", "F[-X][-X]FX" };

        Dictionary<char, string[]> d3 = new Dictionary<char, string[]>
        {
            {'F', fRules2 },
            {'X', XRules3 }
        };

        string[] fRules4 = { "FF-[-F+F+F]+[+F-F-F]", "FF-[-F-F-F]+[+F-F-F]" , "FF-[+F+F+F]+[+F+F-F]" };

        Dictionary<char, string[]> d4 = new Dictionary<char, string[]>
        {
            {'F', fRules4 }
        };

        Dictionary<char, string[]>[] trees = { d1,d2,d3,d4};
        randomRules = trees;

        //create axioms for the random trees
        string[] axioms = { "F", "X", "X", "F" };
        startRules = axioms;

    }

    //creates a tree by calling the general tree class
    private void createTree()
    {
        //rules for F (33.33~ change of choosing the rule)
        string[] fRules = { "F[+F]F[-F]F","F[+F]F", "F[-F]F" };

        //rules
        rules = new Dictionary<char, string[]>
        {
            {'F', fRules }
        };

        //start
        start = "F";

        //create tree with generic tree class
        tree.CreateTree(rules, start, iterations, length, angle, iterationMax);
    }

    //generates a random tree (from the trees in the store)
    public void randomtree()
    {
        //reset the old tree
        tree.resetTree();

        //get random tree number
        int index = getRandomInt(0,randomRules.Length);
        rules = randomRules[index];
        start = startRules[index];
        iterations = 5;

        //create random tree
        tree.CreateTree(rules, start, iterations, length, angle, iterationMax);

        //change the rule text and axiom text
        ruleText.text = "Random tree rules applied";
        axiomText.text= "Random tree generated";

        //cooldown
        StartCoroutine(buttonCoolDown());
    }

    // gives a random int between start and end
    private int getRandomInt(int start,int end)
    {
        System.Random random = new System.Random();
        int index=random.Next(start, end);
        return index;
    }


    //coroutine to cooldown the random tree button
    IEnumerator buttonCoolDown()
    {
        randomButton.interactable = false;
        yield return new WaitForSeconds(2);
        randomButton.interactable = true;
    }
}
