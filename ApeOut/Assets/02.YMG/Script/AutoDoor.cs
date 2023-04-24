using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Animator doorAnim;
    public Transform playersence;
    public Transform door;
    //public BoxCollider doorCollider;

    void Start()
    {
        playersence = GameObject.FindGameObjectWithTag("Player").transform;
        //playersence = PlayerManager.Instance.player.transform;
        // 플레이어를 싱글톤 속 인스턴스로 선언해서 find 를 안써도 됨
    }

    void Update()
    {
        float distance = Vector3.Distance(playersence.position, door.position);

        if (distance <= 5)
        {
            doorAnim.SetBool("Near", true);
            //gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else 
        {
            doorAnim.SetBool("Near", false);
            //gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
