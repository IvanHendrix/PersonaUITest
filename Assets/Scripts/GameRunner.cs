using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{
    private const  string NextSceneName = "TitleMenu";
    
    private void Start()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}