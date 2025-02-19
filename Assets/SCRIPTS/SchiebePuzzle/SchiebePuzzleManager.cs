using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class SchiebePuzzleManager : MonoBehaviour
{
    public GameObject[] puzzlePrefabs;
    public Transform spawnPoint;
    private List<int> usedIndexes = new List<int>();
    public float delayBeforeDestroy = 1f;
    public static SchiebePuzzleManager Instance; //Singelton
    private int solvedPuzzles = 0; //Zählt die gelösten Puzzle
    public int limitsolvedPuzzles = 4; //Anzahl die gelösten werden müssen
    public GameObject timerCanva;
    private int maxlife = 2;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject EmptyHeart2;
    public GameObject EmptyHeart3;



    //GIANNI Variables
    public GameObject hamster5;
    public GameObject hamster6;
    public GameObject hamster7;
    public GameObject hamster8;
    public GameObject antSpawner2;
    public GameObject horsePuzzle;

    public void Awake()
    {
        if (Instance == null) 
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void spawnfirstPuzzle(){
        GameObject puzzleInstance = Instantiate(puzzlePrefabs[0], spawnPoint.position, Quaternion.identity);
        puzzleInstance.name = "Puzzle";

        usedIndexes.Add(0);
    }
    // Methode wird nach lösen aufgerufen
    public void OnPuzzleSolved(GameObject puzzle, bool timeOver)
    {
        StartCoroutine(DestroyAndSpawnPuzzle(puzzle, timeOver));
    }

    private IEnumerator DestroyAndSpawnPuzzle(GameObject puzzleObject, bool timeOver)
    {
        // Zerstörung des alten Puzzles
        Destroy(puzzleObject);



        //GIANNI WIP
        if (timeOver == true)  //verloren
        {
            lifeDecrease();
            solvedPuzzles = 0;
            timerCanva.gameObject.SetActive(false);
            hamster5.gameObject.SetActive(false);
            hamster7.gameObject.SetActive(true);
            yield break;
        }


        //puzzle geloest...?
        solvedPuzzles = solvedPuzzles + 1;


        if (solvedPuzzles == limitsolvedPuzzles) //gewonnen
        {
            timerCanva.gameObject.SetActive(false);
            hamster5.gameObject.SetActive(false);
            hamster7.gameObject.SetActive(false);
            hamster6.gameObject.SetActive(true);
            hamster8.gameObject.SetActive(true);
            antSpawner2.gameObject.SetActive(false);
            horsePuzzle.gameObject.SetActive(true);
            yield break;
        }



        // Verzögerung bevor neues Puzzle spawnt
        yield return new WaitForSeconds(delayBeforeDestroy);

        // Spawnt ein Puzzle
        SpawnRandomPuzzle();
    }

    private void SpawnRandomPuzzle()
    {
        // Überprüfen, ob alle Puzzles bereits verwendet wurden
        if (usedIndexes.Count == puzzlePrefabs.Length)
        {
            // Wenn alle Puzzle verwendet liste zurücksetzten
            usedIndexes.Clear();
        }

        int randomIndex;

        // random Puzzle
        do
        {
            randomIndex = Random.Range(0, puzzlePrefabs.Length);
        }
        while (usedIndexes.Contains(randomIndex));

        // Puzzle instanziieren
        GameObject puzzleInstance = Instantiate(puzzlePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        puzzleInstance.name = "Puzzle";

        // Füge den verwendeten Index der Liste hinzu
        usedIndexes.Add(randomIndex);
    }

    public void lifeDecrease()
    {
        if (SceneManager.GetActiveScene().name == "Nuketown CHALLENGE")
        {
            switch (maxlife)
            {
                case 0:
                    {
                        foreach (var obj in FindObjectsOfType<GameObject>())
                        {
                            if (obj.scene.buildIndex == -1) // Objekte, die nicht zur Szene gehören
                            {
                                Destroy(obj);
                            }
                        }
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        break;

                    }
                case 1:
                    {
                        Heart2.gameObject.SetActive(false);
                        EmptyHeart2.gameObject.SetActive(true);
                        maxlife = maxlife - 1;
                        break;
                    }
                case 2:
                    {
                        Heart3.gameObject.SetActive(false);
                        EmptyHeart3.gameObject.SetActive(true);
                        maxlife = maxlife - 1;
                        break;
                    }
            }
        }
    }
}
