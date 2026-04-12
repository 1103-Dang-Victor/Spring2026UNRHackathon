using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject RegularAttackCollider;
    public Animator anim;
    public float cooldown = 0;
    private float timer;

    private void Start() {
        RegularAttackCollider.SetActive(false);
    }

    private void Update(){
        if(timer > 0){
            timer -= Time.deltaTime;
        }
    }

    public void Attack(){
        if(timer<=0){
            anim.SetBool("isAttacking", true);
            RegularAttackCollider.SetActive(true);
            //Debug.Log("Player is attacking");
            timer = cooldown;
        }
    }
    public void FinishAttacking(){
        anim.SetBool("isAttacking", false);
        RegularAttackCollider.SetActive(false);
        //Debug.Log("Player finished attacking");
    }
}
