using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public float fireTime = 1.0f;
    public float rotationSpeed = 10.0f;

    public GameObject bulletPrefab;
    public GameObject turretEnd;

    protected Tower tower;

    private float lastFire = float.NegativeInfinity;

	// Use this for initialization
	void Start () {
        tower = gameObject.GetComponent<Tower>();
	}
	
	// Update is called once per frame
	void Update () {
		if (tower.isActive && tower.isAlive)
        {
            GameObject closestEnemyTower = FindClosestEnemy();
            if (closestEnemyTower != null)
            {
                RotateTowardsEnemy(closestEnemyTower);
                AttemptToFireAt(closestEnemyTower);
            }
        }
	}

    private GameObject FindClosestEnemy()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Player");

        float closestDistance = float.PositiveInfinity;
        GameObject closestEnemyTower = null;

        for (int i = 0; i < towers.Length; i++)
        {
            GameObject t = towers[i];
            Tower tc = t.GetComponent<Tower>();
            if (t == gameObject || tc.player == tower.player || tc.isActive == false || tc.isAlive == false)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, t.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemyTower = t;
            }
        }
        return closestEnemyTower;
    }

    private void RotateTowardsEnemy(GameObject closestEnemyTower)
    {
        Vector3 targetDir = closestEnemyTower.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void AttemptToFireAt(GameObject closestEnemyTower)
    {
        if (Time.time - lastFire < fireTime)
        {
            return;
        }

        Vector3 targetDir = closestEnemyTower.transform.position - transform.position;

        float angle = Vector3.Angle(transform.forward, targetDir);

        if (angle < 1.0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, turretEnd.transform.position, turretEnd.transform.rotation);
            Bullet b = bullet.GetComponent<Bullet>();
            b.instigator = gameObject;
            bullet.GetComponentInChildren<Renderer>().material.color = GetComponent<Renderer>().material.color;

            lastFire = Time.time;
        }
    }
}
