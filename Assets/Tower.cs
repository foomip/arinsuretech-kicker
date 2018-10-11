using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public int player;

    public int maxHitPoints;
    public int hitPoints;

    public bool isActive = false;
    public bool isAlive = true;

    public GameObject explosionPrefab;

    // Use this for initialization
    void Start () {
        hitPoints = maxHitPoints;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        this.isActive = true;
    }

    public void Deactivate()
    {
        this.isActive = false;
    }

    public void OnHitByBullet(Bullet b)
    {
        this.hitPoints -= 1;
        if (hitPoints <= 0)
        {
            this.Die();
        }
    }

    public void Die()
    {
        if (!this.isAlive)
        {
            return;
        }
        this.isAlive = false;
        GetComponent<Renderer>().material.color = Color.grey;
        GameObject explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
    }
}
