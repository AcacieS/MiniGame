using UnityEngine;

public class cam_script : MonoBehaviour
{
    public GameObject bad_end_scene;
    public GameObject mechanism;
    public Camera endCamera;
    
    
    public void ShowEndCamera(){
        bad_end_scene.SetActive(true);
        mechanism.SetActive(false);
        endCamera.enabled = true;
    }

}
