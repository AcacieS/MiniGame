using UnityEngine;

public class detect_melee_script : MonoBehaviour
{
    public Animator objAnimator;
    
    void Start(){
        //objAnimator = GetComponent<Animator>();
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Melee"+other.tag);
        if(other.tag=="hitMelee"){
            Debug.Log("ooooo");
            IsHit(objAnimator);
        }
    }
    
    public bool IsHit(Animator objAnimator){
        ReverseAnimation(objAnimator);
        return true;
    }
    
    void ReverseAnimation(Animator objAnimator){
        
        objAnimator.SetFloat("Speed", -3);
    }

}
