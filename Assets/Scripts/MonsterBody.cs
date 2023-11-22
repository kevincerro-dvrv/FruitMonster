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
        transform.Rotate(Vector3.back, Time.deltaTime * speed);
    }

    public float GetCurrentAngle()
    {
        return transform.eulerAngles.z;
    }
}
