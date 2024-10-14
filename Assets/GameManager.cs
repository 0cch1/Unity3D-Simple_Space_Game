using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject spaceship;
    public GameObject asteroid;
    public int currentGameLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(0, 30, 0);
        // The camera views the game world from above, with the z-axis displayed as the vertival axis on the screen
        Camera.main.transform.LookAt(Vector3.zero,Vector3.forward);

        CreatePlayerSpaceship();
        StartNextLevel();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartNextLevel()
    {
       int asteroidCount = 3 + currentGameLevel;
        for(int i = 0; i < asteroidCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            Instantiate(asteroid,spawnPosition,Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        // Randomly along the edges of the visible area

        Vector3 viewportPoint = Vector3.zero;
        int edge = Random.Range(0, 4); // 0 = left, 1 = right, 2 = up, 3 = bottom;

        switch (edge) {
            case 0:
                viewportPoint.x = 0f;
                viewportPoint.y = Random.value;
                break;

            case 1:
                viewportPoint.x = 1f;
                viewportPoint.y = Random.value;
                break;

            case 2:
                viewportPoint.x = Random.value;
                viewportPoint.y = 1f;
                break;
                
            case 3:
                viewportPoint.x = Random.value;
                viewportPoint.y = 0f;
                break;
        }

        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(viewportPoint.x, viewportPoint.y, 30f));
        worldPoint.y = 0; // Set y position to 0 to keep asteroids on the same plane.

        return worldPoint;

    }

    void CreatePlayerSpaceship()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        Instantiate(spaceship, Vector3.zero, rotation);
    }
}
