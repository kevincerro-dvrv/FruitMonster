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

    private const float CAPTURE_PRECISION = 10f;

    private int hp = 3;
    public int totalScore = 0;

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
        if (Input.GetKeyDown(KeyCode.Space)) {
            EatFruit();
        }
    }

    private void EatFruit()
    {
        if (!IsFruitInMouth()) {
            audioPlayer.PlayOneShot(failClip);
            hp--;
            return;
        }

        Destroy(spawnedFruit);
        audioPlayer.PlayOneShot(winClip);
        totalScore += spawnedFruit.GetComponent<Fruit>().points;
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
}
