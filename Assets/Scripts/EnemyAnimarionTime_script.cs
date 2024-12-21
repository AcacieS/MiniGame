using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class EnemyAnimarionTime_script : MonoBehaviour
{
    [SerializeField] private Animator[] eyes;
    [SerializeField] private Animator[] hands;
    [SerializeField] private Animator[] phones; 


    private Dictionary<string, Animator[]> my_choice = new Dictionary<string, Animator[]>();
   

    private float action_Time = 5f;
    private float action_Time_max = 5f;
    
    
    //GameObject[] eyessss = my_choice["eye"];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }
    public void addTypeAction(string Obj){
        if (Obj=="eye"){
            my_choice.Add(Obj, eyes);
        }else if(Obj=="hand"){
            my_choice.Add(Obj, hands);
        }else{
            my_choice.Add(Obj, phones);
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
        string actionKey = my_choice.Keys.ElementAt(choiceAction_rand);

        Animator[] element_action = my_choice[actionKey];

        //Select random animator within the chosen array
        if(element_action.Length > 0){
            int choiceObj_rand = Random.Range(0,element_action.Length);
            Animator obj_choice = element_action[choiceObj_rand];
            obj_choice.SetTrigger("Animate");
        }
    }
    

}
