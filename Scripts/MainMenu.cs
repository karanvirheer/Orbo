using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField GravityInputField;
    public InputField PlanetInputField;
    public InputField RadiusInputField;

    static public int planetnumb;
    static public int gravnumb;
    static public int radiusnumb;

    public GameObject OrbitDone;
    public GameObject PlanetDone;
    public GameObject GravDone;



    //Changes the scene from the Menu screen to the Simulation screen
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Makes the Quit button actually quit the application
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    //Changes the scene from the Simulation screen to the Menu screen
    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /* When the enter button is clicked for any of the input fields, the text in the 
     * field is then converted into and integer.
     * It also sets the text underneath the inputfields that says "Done" to be seen when clicked.*/
    public void GravEnter()
    {
        gravnumb = int.Parse(GravityInputField.text);
        GravDone.SetActive(true);
    }   
    public void PlanetEnter()
    {
        planetnumb = int.Parse(PlanetInputField.text);
        PlanetDone.SetActive(true);
    }
   
    public void RadEnter()
    {
        radiusnumb = int.Parse(RadiusInputField.text);
        OrbitDone.SetActive(true);
    }
}
