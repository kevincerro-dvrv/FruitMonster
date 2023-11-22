using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MonsterBody monsterBody;
    public GameObject fruitCarousel;
    public GameObject spawnPoint;
    public GameObject[] fruitPrefabs;

    public static GameManager instance;

    private GameObject spawnedFruit;

    void Awake()
    {
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
        if (Mathf.Abs(fruitCarousel.transform.eulerAngles.z - monsterBody.GetCurrentAngle()) < 4) {
            // Destroy fruit
            Destroy(spawnedFruit);

            // @TODO Play ok sound
            // @TODO Add points

            // Spawn new fruit
            SpawnFruit();

            return;
        }

        // @TODO Play ko sound
        // @TODO Substract 1 point to player
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
}
