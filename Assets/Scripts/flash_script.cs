using UnityEngine;
using UnityEngine.UI;

public class flash_script : MonoBehaviour
{
    //[SerializeField] private AudioClip flashSound;
    
    private GameObject flashScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashScreen = GameObject.Find("/Canvas/flash");
        //SoundManagerScript.instance.PlaySound(flashSound);
    }
    void Update(){
        
        updateFlash();
    }
    public void updateFlash(){
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
    public void set_flash_0(){
        var color = flashScreen.GetComponent<Image>().color;
        color.a = 0f;
        flashScreen.GetComponent<Image>().color = color;
    }
}
