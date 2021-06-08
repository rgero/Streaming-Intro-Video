using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCollector : MonoBehaviour
{
    public float goalOrbitals = 20;
    public GameObject[] OrbitalPrefabs;
    public float delay = 0.5f;
    private float currentDelay = 0;

    public bool canSpawn = true;

    void Start()
    {
    }

    void addOrbital()
    {
        GameObject selectedPrefab = OrbitalPrefabs[Random.Range(0, OrbitalPrefabs.Length)];
        
        GameObject childObject = Instantiate(selectedPrefab) as GameObject;
        childObject.transform.parent = this.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.childCount < goalOrbitals && currentDelay > delay)
        {
            addOrbital();
            currentDelay = 0;
        }
        currentDelay += Time.deltaTime;
    }
}
