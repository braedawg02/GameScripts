using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using OpenCover.Framework.Model;
using UnityEngine;

public class FoilageGenerator : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public GameObject[] cliffPrefabs;
    public GameObject[] rockPrefabs;
    public GameObject[] SmallRockPrefabs;
    public GameObject[] plantPrefabs;
    public GameObject[] woodPrefabs;
    public int Count;
    public int worldsize;

    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(StartSpawning());
    }
    IEnumerator StartSpawning()
    {

        // Wait until WorldGen has finished
        WorldGen worldGen = FindObjectOfType<WorldGen>();
        while (worldGen != null && !worldGen.isGenerationComplete)
        {
            yield return null;
        }

        // Now that WorldGen has finished, we can proceed with the rest of the code
        if (worldGen != null)
        {
            worldsize = worldGen.worldSize * (25 / 2);
        }
        else
        {
            Debug.LogError("WorldGen script not found! Setting default world size to 256");
            worldsize = 256 * (25 / 2);
        }
        int layerMask = LayerMask.GetMask("Terrain");
        buildProps(20, layerMask, cliffPrefabs);
        buildProps(1000, layerMask, treePrefabs);
        buildProps(800, layerMask, rockPrefabs);
        buildProps(1500, layerMask, SmallRockPrefabs);
        buildProps(1500, layerMask, woodPrefabs);
        buildProps(2000, layerMask, plantPrefabs);



    }
    void buildProps(int Count, LayerMask layerMask, GameObject[] prefabToInstantiate)
    {
        for (int i = 0; i < Count; i++)
        {
            float x = Random.Range(0, worldsize) - 193.75f;
            float z = Random.Range(0, worldsize) - 193.75f;
            Vector3 position = new Vector3(x, 0, z);
            Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);


            Ray ray = new Ray(new Vector3(x, Count, z), Vector3.down);
            RaycastHit hit;
            if (prefabToInstantiate == cliffPrefabs)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    // If the raycast hits a water tile, skip this iteration
                    if (hit.collider.gameObject.tag == "Water" || hit.collider.gameObject.tag == "Foilage" || hit.collider.gameObject.tag == "Tree" || hit.collider.gameObject.tag == "Rock" || hit.collider.gameObject.tag == "Hill")
                    {
                        continue;
                    }
                    position = hit.point;
                    Collider[] colliders = Physics.OverlapSphere(position, 1f);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject.tag != "Terrain")
                        {
                            continue;
                        }
                    }
                }
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // If the raycast hits a water tile, skip this iteration
                if (hit.collider.gameObject.tag == "Water" || hit.collider.gameObject.tag == "Foilage" || hit.collider.gameObject.tag == "Tree" || hit.collider.gameObject.tag == "Rock")
                {
                    continue;
                }
                position = hit.point;
                Collider[] colliders = Physics.OverlapSphere(position, 1f);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag != "Terrain")
                    {
                        continue;
                    }
                }
            }
            int foilageIndex = Random.Range(0, prefabToInstantiate.Length);
            GameObject foilage = Instantiate(prefabToInstantiate[foilageIndex], position, rotation);
            foilage.transform.parent = transform;
            float randomSize;
            Vector3 newSize;
            if (prefabToInstantiate == plantPrefabs)   //if the prefab is a plant, randomize the size
            {
                randomSize = UnityEngine.Random.Range(2f, 2.5f);
                newSize = new Vector3(randomSize, randomSize, randomSize);
                foilage.transform.localScale = newSize;
            }
            else
            {
                randomSize = UnityEngine.Random.Range(0.9f, 1.1f);
                newSize = new Vector3(randomSize, randomSize, randomSize);
                foilage.transform.localScale = newSize;
            }

            foilage.transform.localScale = newSize;



        }

    }

}
