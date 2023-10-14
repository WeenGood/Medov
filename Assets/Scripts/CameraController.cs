using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }

    //[SerializeField] private Transform player;

    private void Update()
    {
        //transform.position = new Vector3(player.position.x, 2, transform.position.z); 

    }
}
