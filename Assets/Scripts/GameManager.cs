using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] levelSections;
    public Transform player;
    public List<GameObject> activeSection = new List<GameObject>();
    public float spawnDistance = 30f;
    private Vector3 lastSpawnPos;

    [Header("Score System Settings")]
    public TMP_Text score;
    int scoreInt = 0;
    int scoreDispInt = 0;
    public TMP_Text dispScore;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
            for (int i = 0; i < 3; i++)
            {
                SpawnSection();
            }
    }
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            UIManager.instance.OnPause();
        }
    }
    private void FixedUpdate()
    {
        if((player.position.x + 6f) > lastSpawnPos.x - spawnDistance)
        {
            SpawnSection();
            RemoveOldSection();
        }
    }

    void SpawnSection()
    {
        GameObject newSection = Instantiate(levelSections[Random.Range(0, levelSections.Length)], lastSpawnPos , Quaternion.identity);
        lastSpawnPos.x += spawnDistance;
        activeSection.Add(newSection);
    }

    void RemoveOldSection()
    {
        if (activeSection.Count > 3)
        {
            Destroy(activeSection[0]);
            activeSection.RemoveAt(0);
        }
    }

    public void UpdateScore()
    {
        if (PlayerController.instance.isAlive)
        {
            scoreInt += 1;
            score.text = scoreInt.ToString();
        }
       
    }

    public void DecreaseScore()
    {
        if (PlayerController.instance.isAlive)
        {
            scoreInt = scoreInt - 1;
            score.text = scoreInt.ToString();
        }
            
    }

    public void IncreasePlusFiveScore()
    {
        if (PlayerController.instance.isAlive)
        {
            scoreInt += 5;
            score.text = scoreInt.ToString();
        }
    }
    public void DisplayScore()
    {
        scoreDispInt = scoreInt;
        dispScore.text = scoreDispInt.ToString();
    }

}
