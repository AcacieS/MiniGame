using System;
using UnityEngine;
using UnityEngine.UI;

public class time_script : MonoBehaviour
{
    public TextMesh TimeTxt;
    private EnemyAnimarionTime_script anim_script;
    private float total_Time = 100f;
    private float remain_Time;
    private float hand_time = 50f;
    private float phone_time = 70f;
    //public Image timer_game;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim_script = GameObject.Find("Enemy").GetComponent<EnemyAnimarionTime_script>();
        anim_script.addTypeAction("eye");
        remain_Time = total_Time;
    }

    // Update is called once per frame
    void Update()
    {
        if(remain_Time>0){
            TimeDecount();
        }else{
            Set_add_type_action(phone_time, "phone");
            Set_add_type_action(hand_time, "hand");
            Set_max_time_script(85,4);
            Set_max_time_script(70,3);
        }

        //total_Time -= Time.deltaTime;
        if(total_Time<=0){
            Good_End();
        }
       
        
    }
   
    private void TimeDecount(){
        remain_Time -= Time.deltaTime;
        int displayTime = Mathf.FloorToInt(remain_Time);
        TimeTxt.text = displayTime.ToString();
    }
    private void Set_max_time_script(float whichTime, float max_time_interval){
        if(whichTime_function(whichTime)==true){
            anim_script.Set_max_time(max_time_interval);
        }
    }
    private void Set_add_type_action(float whichTime, string typeAction){
        if(whichTime_function(whichTime)==true){
            anim_script.addTypeAction(typeAction);
        }
    }
    
    private bool whichTime_function(float whichTime){
        if(remain_Time==whichTime){
            return true;
        }
        return false;
    }
    void Good_End(){
        Debug.Log("Good End triggered!");
    }
}
                                         