using UnityEngine;
using UnityEngine.UI;
public class cam_script : MonoBehaviour
{
    public GameObject bad_end_scene;
    public GameObject mechanism;
    public Camera endCamera;
    private bool end = false;
    private GameObject flashScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashScreen = GameObject.Find("/Canvas/flash");
        //SoundManagerScript.instance.PlaySound(flashSound);
    }
    
    public void ShowEndCamera(){
        end = true;
        bad_end_scene.SetActive(true);
        
        mechanism.SetActive(false);
        
        endCamera.enabled = true;
    }
    void Update(){
        if (!end){
            updateFlash();
        }
    }
    public void updateFlash(){
        if(flashScreen.GetComponent<Image>().color.a > 0){
            var color = flashScreen.GetComponent<Image>().color;
            color.a-=0.001f;
            flashScreen.GetComponent<Image>().color = color;
        }
    }
    

}
