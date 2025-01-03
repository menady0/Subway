using UnityEngine;

public class CmeraController : MonoBehaviour
{
    public Transform target;
    public float smoothTransition = 125f;
    public Vector3 offset;
    GameObject player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void LateUpdate()
    {
        if (PlayerController.m_animator.GetBool("isRunning") && offset.z <= -8)
            offset.z += 0.1f;

        Vector3 desiredPosition;
        desiredPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        //if (player.transform.position.y >= 4)
            //desiredPosition = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z);
        //else
            //desiredPosition = new Vector3(transform.position.x, target.position.y - offset.y, target.position.z + offset.z);
        if(!PlayerController.m_animator.GetBool("isJumping") && player.transform.position.y >= 4)
            desiredPosition = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z);
        else if(PlayerController.m_animator.GetBool("isJumping") && player.transform.position.y < 4)
            desiredPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        else if (player.transform.position.y < 4 && !PlayerController.m_animator.GetBool("isJumping"))
            desiredPosition = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z);
        else if (player.transform.position.y < 4 && (PlayerController.m_animator.GetBool("isJumping") || PlayerController.m_animator.GetBool("isRolling")))
            desiredPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTransition * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
