using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    

    public void HandleStartButton()
    {
        //dotween? animation effect 
        StartCoroutine(LevelManager.Instance.LoadScene("MoveDebug"));
        
    }

    public void HandleQuitButton()
    {
        Application.Quit();
    }


}
