using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolision : MonoBehaviour
{
    public float delay = 0.5f;
    private float countdown;
    private bool hasExploded = false;

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
            StartCoroutine(ExPlosion());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(RDefine.PLAYER_TAG))
        {
            Debug.Log("Player_in_Area");
        }
    }

    IEnumerator ExPlosion()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        boomerArea.enabled = true;
        yield return new WaitForSeconds(0.4f);
        boomerArea.enabled = false;
        Destroy(gameObject);
    }


}
