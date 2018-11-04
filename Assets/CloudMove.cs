using UnityEngine;

public class CloudMove : MonoBehaviour {

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + 0.5f * Time.deltaTime, transform.position.y);
    }
}
