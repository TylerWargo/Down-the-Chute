using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberGenerator : MonoBehaviour
{
    public GameObject trash1, trash2, trash3, trash4;
    public GameObject smallSlime, largeSlime;

    //Min & Max Slimes To Be Determined / Spawned
    public int minSlimes;
    public int maxSlimes;

    //Slime Loop Variables
    private int slimesToSpawn;
    private int currentSlimesSpawned = 0;


    //Min & Max Trash To Be Determined / Spawned
    public int minTrash;
    public int maxTrash;

    //Trash Loop Variables
    private int trashToSpawn;
    private int currentTrashSpawned = 0;

    //The Spawning Area
    public int minXSpawn;
    public int maxXSpawn;
    public int minYSpawn;
    public int maxYSpawn;

    private void Awake()
    {
        slimesToSpawn = Random.Range(minSlimes, maxSlimes);
        trashToSpawn = Random.Range(minTrash, maxTrash);
    }

    public void Update()
    {
        GenSlimes();
        GenTrash();
    }

    public void GenSlimes()
    {
        while (currentSlimesSpawned < slimesToSpawn)
        {
            Vector2 spawnToPosition = new Vector2(Random.Range(minXSpawn, maxXSpawn), Random.Range(minYSpawn, maxYSpawn));
            int slimeChoice = Random.Range(1, 3);

            switch (slimeChoice)
            {
                case 2:
                    Instantiate(smallSlime, spawnToPosition, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(largeSlime, spawnToPosition, Quaternion.identity);
                    break;
            }

            currentSlimesSpawned++;
        }
    }

    public void GenTrash()
    {
        while (currentTrashSpawned < trashToSpawn)
        {
            Vector2 spawnToPosition = new Vector2(Random.Range(minXSpawn, maxXSpawn), Random.Range(minYSpawn, maxYSpawn));
            int trashChoice = Random.Range(1, 5);

            switch (trashChoice)
            {
                case 4:
                    Instantiate(trash4, spawnToPosition, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(trash3, spawnToPosition, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(trash2, spawnToPosition, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(trash1, spawnToPosition, Quaternion.identity);
                    break;
            }

            currentTrashSpawned++;
        }
    }
}
