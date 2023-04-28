using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 3f;
    private float countdown;
    private bool hasExploded = false;
    
    //public GameObject explosionEffect;
    public ParticleSystem explosionEffect;
    public SphereCollider boomerArea;

    
    void Start()
    {
        countdown = delay;
        boomerArea = GetComponentInChildren<SphereCollider>();
        boomerArea.enabled = false;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded) 
        {
            hasExploded = true;
            StartCoroutine(Explode());
        }

        
    }

    //void Explode() 
    //{
    //    Instantiate(explosionEffect, transform.position, transform.rotation);
    //    boomerArea.enabled = true;
    //    //Debug.Log(boomerArea);
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(RDefine.PLAYER_TAG)) 
        {
            Debug.Log("Player_in_Area");
            PlayerManager.Instance.player.playerHp--;
        }
    }

    IEnumerator Explode() 
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        boomerArea.enabled = true;
        yield return new WaitForSeconds(0.4f);
        //Debug.Log(boomerArea);
        boomerArea.enabled = false;
        Destroy(gameObject);

    }
}
