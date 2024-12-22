using UnityEngine;

public class detect_spray_script : MonoBehaviour
{
    private Animator objAnimator;
    
    void Start(){
        objAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaaaa"+gameObject.tag);
        if(other.tag=="Hits"&&gameObject.tag!="hand"){
            Debug.Log("hit: "+gameObject.tag);
            IsHit(objAnimator);
        }
    }
    
    public bool IsHit(Animator objAnimator){
        ReverseAnimation(objAnimator);
        return true;
    }
    
    void ReverseAnimation(Animator objAnimator){
        //objAnimator.SetTrigger("Hit");
        objAnimator.SetFloat("Speed", -3);
    }

}
