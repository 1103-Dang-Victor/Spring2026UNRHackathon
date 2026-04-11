using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    

    public void HandleStartButton()
    {
        //dotween? animation effect 
        //scene manager load into the scene of the main gameplay
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //add loading screen?
    }

    public void HandleQuitButton()
    {
        Application.Quit();
    }


}
