using UnityEngine;

public class Fish_Initialize : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public int numObjects = 10;
    public Vector2 areaSize = new Vector2(10f, 10f);
    
    public GameObject spwn_pos;

    private GameObject[] gameObjects;

    

    private void Start()
    {
        gameObjects = new GameObject[numObjects];
        for (int i = 0; i < numObjects; i++)
        {
            GameObject randomPrefab = objectPrefabs[Random.Range(0, 7)];


            Vector3 randomPosition = new Vector3(Random.Range(-areaSize.x / 2, areaSize.x / 2),
                                                 0f,
                                                 Random.Range(-areaSize.y / 2, areaSize.y / 2));
            randomPosition += spwn_pos.transform.position;

            gameObjects[i] = Instantiate(randomPrefab, randomPosition, Quaternion.identity, spwn_pos.transform);

            gameObjects[i].GetComponent<PrefabMOV>().Initialize(Random.Range(6f, 40f),
                                                                     Random.Range(0.75f, 7f),
                                                                     Random.Range(0f, 180f),
                                                                     Random.Range(0, 8));
        }
        for (int i = 0; i < 2; i++)
        {

            GameObject shark = objectPrefabs[7];

            Vector3 randomPosition = new Vector3(Random.Range(-areaSize.x, areaSize.x),
                                                 Random.Range(-10f, 5f),
                                                 Random.Range(-areaSize.y, areaSize.y));
            randomPosition += spwn_pos.transform.position;


            gameObjects[i] = Instantiate(shark, randomPosition, Quaternion.identity, spwn_pos.transform);
            gameObjects[i].GetComponent<PrefabMOV>().Initialize(Random.Range(10f, 90f),
                                                                     Random.Range(3f, 10f),
                                                                     Random.Range(0f, 180f),
                                                                     Random.Range(0, 4));
        }

        for (int i = 0; i < 10; i++)
        {

            GameObject Bird = objectPrefabs[8];

            Vector3 randomPosition = new Vector3(Random.Range(-areaSize.x/4, areaSize.x/4),
                                                 Random.Range(12f,17f),
                                                 Random.Range(-areaSize.y/4, areaSize.y / 4));
            randomPosition += spwn_pos.transform.position;


            gameObjects[i] = Instantiate(Bird, randomPosition, Quaternion.identity, spwn_pos.transform);
            gameObjects[i].GetComponent<PrefabMOV>().Initialize(Random.Range(10f, 90f),
                                                                     Random.Range(1f, 8f),
                                                                     Random.Range(0f, 180f),
                                                                     Random.Range(0f, 2));
        }
    }
    




    
}