using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator anim;
    public float cooldown = 0;
    private float timer;

    private void Update(){
        if(timer > 0){
            timer -= Time.deltaTime;
        }
    }

    public void Attack(){
        if(timer<=0){
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }
    public void FinishAttacking(){
        anim.SetBool("isAttacking", false);
    }
}
