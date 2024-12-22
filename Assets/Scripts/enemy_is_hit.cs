using UnityEngine;

public class enemy_is_hit : MonoBehaviour
{
    private Animator objAnimator;
    void Start(){
        objAnimator = GetComponent<Animator>();
    }
    
    public bool IsHit(Animator objAnimator){
        ReverseAnimation(objAnimator);
        return true;
    }
    
    void ReverseAnimation(Animator objAnimator){
        objAnimator.SetFloat("Speed", -1f);
    }
}
