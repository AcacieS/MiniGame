using System;
using UnityEngine;
using UnityEngine.UI;

public class time_script : MonoBehaviour
{
    public text_script Txt_script;
    public TextMesh TimeTxt;
    public GameObject enemy;
    public goodend_script good_end;
    //[SerializeField] private AudioClip breathingSound;
    [SerializeField] private AudioClip heartSound;
    private EnemyAnimationTime_script anim_script;
    private float total_Time = 120f;
    private float remain_Time; 
    private float hand_time = 90f;
    private float phone_time = 60f;
    private bool oneTime_heart = true;
    private float[] time_txt = {105, 20};
    private bool[] oneTime_txt = {true, true};
    //public Image timer_game;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim_script = GameObject.Find("/Mechanism/Enemy").GetComponent<EnemyAnimationTime_script>();
        anim_script.addTypeAction("eye");
        //SoundManagerBG2_script.instance.PlaySound(breathingSound);
        remain_Time = total_Time;
    }

    // Update is called once per frame
    void Update()
    {
        if(remain_Time>0){
            TimeDecount();
        }else{
            Good_End();
        }
    }
   
    private void TimeDecount(){
        SetTxt(0);
        SetTxt(1);
        Set_add_type_action(phone_time, "phone");
        Set_add_type_action(hand_time, "hand");
        Set_max_time_script(105,5);
        Set_max_time_script(75,4);
        Set_max_time_script(45,3);
        Set_max_time_script(30,2);
        Set_max_time_script(15,1);

        if(oneTime_heart){
            if(whichTime_function(90)){
                oneTime_heart = false;
                SoundManagerBG_script.instance.PlaySound(heartSound);
            }
        }else{
            SoundManagerBG_script.instance.SetPitch();
        }
        
        remain_Time -= Time.deltaTime;
        remain_Time = Mathf.Max(remain_Time, 0);
        int displayTime = Mathf.FloorToInt(remain_Time);
        TimeTxt.text = displayTime.ToString();
    }
    private void SetTxt(int index){
        if(!oneTime_txt[index]&&whichTime_function(time_txt[index])){
            oneTime_txt[index] = true;
            Txt_script.NextLine();
        }
        

    }
    private void Set_max_time_script(float whichTime, float max_time_interval){
        if(whichTime_function(whichTime)){
            anim_script.Set_max_time(max_time_interval);
        }
    }
    
    private void Set_add_type_action(float whichTime, string typeAction){
        
        if(whichTime_function(whichTime)){
            anim_script.addTypeAction(typeAction);
        }
    }
    
    private bool whichTime_function(float whichTime){
        if(Mathf.FloorToInt(remain_Time)==whichTime){
            return true;
        }
        return false;
    }
    void Good_End(){
        Debug.Log("Good End triggered!");
        enemy.SetActive(false);
        good_end.SetIsEnd();
    }
    
}
                                         