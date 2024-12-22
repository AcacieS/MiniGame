using UnityEngine;

public class table_script : MonoBehaviour
{
    private Animator currentAnim;
    private bool isClose = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("kkk"+other.tag);
        if(other.tag=="Player"){//&&Input.GetKeyDown("e")){
            
            isClose = !isClose;
            Debug.Log("Closed?"+isClose);
            currentAnim.SetBool("isClosed", isClose);
            
        }
    }
}
