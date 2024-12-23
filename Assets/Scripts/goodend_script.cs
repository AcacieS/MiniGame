using UnityEngine;
using UnityEngine.SceneManagement;


public class goodend_script : MonoBehaviour
{
    private bool isEnd = false;
    

    void OnCollisionEnter(Collision col){
        if(isEnd){
            SceneManager.LoadScene("Scenes/Victory");
        }else{
            Debug.Log("I don't have a lot of time");
        }

    void Update(){
        if(isEnd){

        }
    }
    }
    public void SetIsEnd(){
        isEnd = true;
    }
    public bool GetIsEnd(){
        return isEnd;
    }
     

}
