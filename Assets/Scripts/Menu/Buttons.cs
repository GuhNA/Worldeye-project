using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject Cred;
    [SerializeField] GameObject Menu;
    public bool active;
    public void animPlay()
    {
        GameObject.Find("Play").GetComponent<Animator>().SetTrigger("pressed");
    }

    public void animExit()
    {
        GameObject.Find("Exit").GetComponent<Animator>().SetTrigger("pressed");
    }

    public void animCredits()
    {
        GameObject.Find("Credits").GetComponent<Animator>().SetTrigger("pressed");
    }

    public void exitAnimCredits()
    {
        GameObject.Find("ExitCredits").GetComponent<Animator>().SetTrigger("pressed2");
    }
    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void credits()
    {
        if(!active)
        {
            Menu.SetActive(false);
            Cred.SetActive(true);
        }
        if(active)
        {
            Menu.SetActive(true);
            Cred.SetActive(false);
        }
    }

    public void exit()
    {
        Application.Quit();
    }
}
