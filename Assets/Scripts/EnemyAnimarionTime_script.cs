using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class EnemyAnimarionTime_script : MonoBehaviour
{
    [SerializeField] private Animator[] eyes;
    [SerializeField] private Animator[] hands;
    [SerializeField] private Animator[] phones; 


    private Dictionary<string, Animator[]> my_choice = new Dictionary<string, Animator[]>();
   

    private float action_Time;
    private float action_Time_max;
    private float total_Time;
    
    //GameObject[] eyessss = my_choice["eye"];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        my_choice.Add("eye", eyes);
        my_choice.Add("hand", hands);
        my_choice.Add("phone", phones);

        action_Time = 5f;
        action_Time_max = 5f;
        total_Time = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        total_Time -= Time.deltaTime;
        action_Time -= Time.deltaTime;

        if(total_Time<=0){
            Good_End();
        }
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
    void Good_End(){
        Debug.Log("Good End triggered!");
    }

}
