using UnityEngine;

public class bad_end : MonoBehaviour
{
   
    private cam_script cam_script;
    void Start(){
        cam_script = GameObject.Find("/cam_logic").GetComponent<cam_script>();
    }
    public void BadEnd(){
        
        cam_script.ShowEndCamera();
    }
}
