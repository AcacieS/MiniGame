using UnityEngine;

public class youHit : MonoBehaviour
{
    private enemy_is_hit enemy_hit_script;
    private Animator enemy_animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag=="enemy"){
            Debug.Log("kahfbkasfbdkashd");
            enemy_hit_script = col.gameObject.GetComponent<enemy_is_hit>();
            enemy_animator = col.gameObject.GetComponent<Animator>();
            enemy_hit_script.IsHit(enemy_animator);
        }
    }
}
