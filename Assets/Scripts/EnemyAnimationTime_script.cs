using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class EnemyAnimationTime_script : MonoBehaviour
{
    [SerializeField] private Animator[] eyes;
    [SerializeField] private Animator[] hands;
    [SerializeField] private Animator[] phones; 


    private Dictionary<string, Animator[]> my_choice = new Dictionary<string, Animator[]>();
   
    private float action_Time_max = 6f;

    private float action_Time;
    
    
    void Start(){
        action_Time = action_Time_max;
    }
   
    public void addTypeAction(string Obj){
        if(!my_choice.ContainsKey(Obj)){
            if (Obj=="eye"){
                my_choice.Add(Obj, eyes);
            }else if(Obj=="hand"){
                
                my_choice.Add(Obj, hands);
            }else{
                my_choice.Add(Obj, phones);
            }  
        }
        
    }
    public void Set_max_time(float new_max_time){
        action_Time_max = new_max_time;
    }
    // Update is called once per frame
    void Update()
    {
        
        action_Time -= Time.deltaTime;

        
        if(action_Time <= 0)
        {
            Do_Animation();
            action_Time = action_Time_max;
        }
        //anim = eyes[0].GetComponent<Animator>();
        
    }
    void Do_Animation(){
        //Random rand = new Random();

        //Select random animation type("eye","hand","phone")
        int choiceAction_rand = Random.Range(0, my_choice.Count);
        //Debug.Log(choiceAction_rand);
        Debug.Log("choice: "+my_choice.Keys.ElementAt(choiceAction_rand));
        string actionKey = my_choice.Keys.ElementAt(choiceAction_rand);

        Animator[] element_action = my_choice[actionKey];

        //Select random animator within the chosen array
        if(element_action.Length > 0){
            int choiceObj_rand = Random.Range(0,element_action.Length);
            Debug.Log("obj"+choiceObj_rand);
            Animator obj_choice = element_action[choiceObj_rand];
            //obj_choice.SetTrigger("Animate");
            //Debug.Log("what:"+obj_choice);
            var stateInfo = obj_choice.GetCurrentAnimatorStateInfo(0);

            // If at the start, explicitly restart the animation
            if (stateInfo.normalizedTime <= 0)
            {
                obj_choice.Play(stateInfo.fullPathHash, -1, 0.0f); // Reset to the start
            }
            obj_choice.SetFloat("Speed", 0.7f);
        }
    }
    

}
