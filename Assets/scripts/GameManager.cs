using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public float sensorValue;
    [SerializeField]
    public int gameLevel;

    public bool gameStarted;
    public GameObject prefab1;
    public GameObject prefab2;
    public SpawnerController[] spawnerControllers;
    public GameObject spawnersParent;

    public GameObject[] UIScreens;
    public int currentScreen;


    public float MinRs1, MaxRs1, MinUs1, MaxUs1;
    public float MinRs2, MaxRs2, MinUs2, MaxUs2;
    public float MinRs3, MaxRs3, MinUs3, MaxUs3;
    public float MinRs4, MaxRs4, MinUs4, MaxUs4;

    // Start is called before the first frame update
    void Start()
    {
        spawnersParent.SetActive(false);
        changeAtomPrefab(prefab1);
        InvokeRepeating(nameof(checkSensor),0f,3f);
        UIScreens[0].SetActive(true);
    }

    void nextScreen() 
    {
        currentScreen++;
        foreach (var screen in UIScreens) 
        {
            screen.SetActive(false);
        }
        UIScreens[currentScreen].SetActive(true);
    }

    void checkSensor()
    {
        if (!gameStarted)
        {
            if (sensorValue > 0 && sensorValue <= 1 && gameLevel == 0)
            {
                //Start game here
                Debug.Log("Game Started!");
                gameStarted = true;
                spawnersParent.SetActive(true);
                changeAtomPrefab(prefab1);
                changeAtomsProperties(MinRs1, MaxRs1, MinUs1, MaxUs1);
                gameLevel = 1;
                return;
            }
            return;
        }

        else if (sensorValue > 1 && sensorValue <= 2 && gameLevel == 1)
        {
            //Change first two arrays to molecule
            changeAtomPrefab(prefab2);
            changeAtomsProperties(MinRs2, MaxRs2, MinUs2, MaxUs2);
            gameLevel = 2;
        }

        else if (sensorValue > 2 && sensorValue <= 3 && gameLevel == 2)
        {
            //Step fast to speed up reaction
            //changeAtomPrefab(prefab2);
            changeAtomsProperties(MinRs3, MaxRs3, MinUs3, MaxUs3);
            gameLevel = 3;
        }

        else if (sensorValue > 3 && sensorValue <= 4 && gameLevel == 3)
        {
            //Can you step faster?
            //changeAtomPrefab(prefab2);
            changeAtomsProperties(MinRs4, MaxRs4, MinUs4, MaxUs4);
            gameLevel = 4;
        }

        else if (sensorValue > 1 && sensorValue <= 2 && gameLevel == 4)
        {
            //Stop!!
            //changeAtomPrefab(prefab2);
            changeAtomsProperties(MinRs1, MaxRs1, MinUs1, MaxUs1);
            gameLevel = 5;
        }
    }

    void changeAtomPrefab(GameObject desiredPrefab) 
    {
        foreach (SpawnerController spawner in spawnerControllers)
        {
            spawner.prefab = desiredPrefab;
            if (gameStarted) 
            {
                spawner.init();
            }
        }
    }

    void changeAtomsProperties(float MinRs, float MaxRs, float MinUs, float MaxUs)
    {
        foreach (SpawnerController spawner in spawnerControllers)
        {
            spawner.minRotationSpeed = MinRs;
            spawner.maxRotationSpeed = MaxRs;
            spawner.minUpwardSpeed = MinUs;
            spawner.maxUpwardSpeed = MaxUs;
        }
    }
}
