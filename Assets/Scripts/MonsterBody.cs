using UnityEngine;

public class MonsterBody : MonoBehaviour
{
    private float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameOver) {
            return;
        }

        // Each 50 points we add 20 degrees/second to speed
        int difficultySpeed = GameManager.instance.GetTotalScore() / 50 * 20;
        transform.Rotate(Vector3.back, Time.deltaTime * (speed + difficultySpeed));
    }

    public float GetCurrentAngle()
    {
        return transform.eulerAngles.z;
    }
}
