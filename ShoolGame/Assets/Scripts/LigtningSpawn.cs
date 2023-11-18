using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigtningSpawn : MonoBehaviour
{
    public float range, spawnRate, lifeTime;
    private float startSpawnRate;
    [SerializeField] bool isCarsSpawn;

    public GameObject ligtning, ligtning2;

    void Start()
    {
        startSpawnRate = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnRate<= 0)
        {
            if (!isCarsSpawn)
            {
                if (Random.Range(1f, 2f) < 1.5f)
                {
                    Destroy(Instantiate(ligtning, new Vector3(Random.RandomRange(-range, range) + transform.position.x, transform.position.y, Random.RandomRange(-range, range) + transform.position.z), GameObject.Find("Main Camera").transform.rotation), lifeTime);
                }
                else
                {
                    Destroy(Instantiate(ligtning2, new Vector3(Random.RandomRange(-range, range) + transform.position.x, transform.position.y, Random.RandomRange(-range, range) + transform.position.z), GameObject.Find("Main Camera").transform.rotation), lifeTime);
                }
            }
            else
            {
                Destroy(Instantiate(ligtning, transform.position, Quaternion.identity), lifeTime);
                ligtning.transform.GetChild(Random.RandomRange(1, 4)).gameObject.SetActive(true);

            }

            spawnRate = startSpawnRate;
        }
        else
        {
            spawnRate-= Time.deltaTime;
        }
    }
    
}