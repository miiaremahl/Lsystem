using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * MainMenu interaction.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 */

public class MainMenuController : MonoBehaviour
{
    public void Update()
    {
        //get active scene
        int active = SceneManager.GetActiveScene().buildIndex;
        //going between tree scenes
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(active < 8)
            {
                SceneManager.LoadScene(active + 1);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (active > 0)
            {
                SceneManager.LoadScene(active - 1);
            }
        }
    }

    //loads tree1 
    public void Tree1()
    {
        SceneManager.LoadScene("Tree1");
    }

    //loads tree2 
    public void Tree2()
    {
        SceneManager.LoadScene("Tree2");
    }

    //loads tree3 
    public void Tree3()
    {
        SceneManager.LoadScene("Tree3");
    }

    //loads tree4 
    public void Tree4()
    {
        SceneManager.LoadScene("Tree4");
    }

    //loads tree1 
    public void Tree5()
    {
        SceneManager.LoadScene("Tree5");
    }

    //loads tree6 
    public void Tree6()
    {
        SceneManager.LoadScene("Tree6");
    }

    //loads tree7
    public void Tree7()
    {
        SceneManager.LoadScene("Tree7");
    }

    //loads tree8
    public void Tree8()
    {
        SceneManager.LoadScene("Tree8");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
