using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
}
