using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

    public void Next_scene(){
        SceneManager.LoadScene(nextSceneIndex);
    }
}
