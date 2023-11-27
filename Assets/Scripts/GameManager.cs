using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MonsterBody monsterBody;
    public GameObject fruitCarousel;
    public GameObject spawnPoint;
    public GameObject[] fruitPrefabs;
    public AudioClip winClip;
    public AudioClip failClip;

    public static GameManager instance;

    private AudioSource audioPlayer;

    private GameObject spawnedFruit;
    private float lastFruitEatenAt = -10;

    private const float CAPTURE_PRECISION = 10f;

    private int hp = 3;
    public int totalScore = 0;

    private bool bonusHpGiven = false;

    private bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnFruit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            EatFruit();
        }
    }

    private void EatFruit()
    {
        if (!IsFruitInMouth()) {
            audioPlayer.PlayOneShot(failClip);
            hp--;

            // Set gameover when no lives left
            if (hp <= 0) {
                isGameOver = true;
            }

            return;
        }

        Destroy(spawnedFruit);
        audioPlayer.PlayOneShot(winClip);

        // Reward user
        int points = spawnedFruit.GetComponent<Fruit>().points;
        if ((Time.time - lastFruitEatenAt) < 0.8f) {
            totalScore += points * 2;
            Debug.Log("Double points");
        } else {
            totalScore += points;
        }

        lastFruitEatenAt = Time.time;

        // Reward extra live once
        if (!bonusHpGiven && totalScore >= 100 ) {
            bonusHpGiven = true;
            hp++;
        }

        SpawnFruit();
    }

    private bool IsFruitInMouth()
    {
        float fruitAngle = fruitCarousel.transform.eulerAngles.z;
        float mouthAngle = monsterBody.GetCurrentAngle();

        return Mathf.Abs(Mathf.DeltaAngle(fruitAngle, mouthAngle)) < CAPTURE_PRECISION;
    }

    private void SpawnFruit()
    {
        // Rotate carousel
        float spawnedDegrees = Random.Range(0, 360);
        fruitCarousel.transform.Rotate(Vector3.forward, spawnedDegrees);
        
        // Spawn fruit in position
        int randomFruit = Random.Range(0, fruitPrefabs.Length);
        spawnedFruit = Instantiate(fruitPrefabs[randomFruit], spawnPoint.transform.position, Quaternion.identity);
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    void OnGUI()
    {
        // Set style
        GUIStyle style = new GUIStyle();
        style.fontSize = 25;
        
        // Draw stats
        GUI.Label(new Rect(10, 10, 200, 25), "TOTAL SCORE: " + totalScore, style);
        GUI.Label(new Rect(10, 50, 200, 25), "HP: " + hp, style);

        // Draw GameOver
        if (isGameOver) {
            style.fontSize = 50;
            GUI.Label(new Rect(Screen.width / 2 - 100, 250, 200, 25), "GameOver", style);
        }
    }
}
