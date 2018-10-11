using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 1.0f;
    public GameObject explosionPrefab;
    public GameObject instigator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward);
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
	}

    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("COLLIDING!!!!! " + col.gameObject.ToString());
        if (col.gameObject.tag == "Player" && col.gameObject != instigator)
        {
            Tower t = col.gameObject.GetComponent<Tower>();
            t.OnHitByBullet(this);
            GameObject explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
            Destroy(gameObject);
        }
    }
}
