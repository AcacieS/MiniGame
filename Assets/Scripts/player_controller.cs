using UnityEngine;

public class player_controller : MonoBehaviour
{
    private Transform transform;
    public GameObject game_spawn;
    public GameObject door;
    public goodend_script good_end;
    public GameObject[] enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(good_end.GetIsEnd()){

        }
        if(game_spawn){
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ;
        }
    }
    void OnTriggerEnter(Collider col){
        if(col.tag=="SpawnPos"){
            Transform otherTransform = col.gameObject.GetComponent<Transform>();
            GetComponent<Transform>().position = otherTransform.position;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            Destroy(col.gameObject);
            door.GetComponent<Animator>().enabled = true;//SetFloat("Speed", -1);

            foreach(GameObject eachEnemy in enemy){
                eachEnemy.SetActive(true);
            }
            
        }

    }
}
