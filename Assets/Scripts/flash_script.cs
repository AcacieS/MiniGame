using UnityEngine;
using UnityEngine.UI;

public class flash_script : MonoBehaviour
{
    private GameObject flashScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashScreen = GameObject.Find("/Canvas/flash");
    }
    void Update(){
        
        if(flashScreen.GetComponent<Image>().color.a > 0){
            var color = flashScreen.GetComponent<Image>().color;
            color.a-=0.001f;
            flashScreen.GetComponent<Image>().color = color;
        }
    }

    public void flash_canvas(){
        var color = flashScreen.GetComponent<Image>().color;
        color.a = 0.8f;
        flashScreen.GetComponent<Image>().color = color;
        //GotFlashScreen.a = 0.8f;
    }
}
