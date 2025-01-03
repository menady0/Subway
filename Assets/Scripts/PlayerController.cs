using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum position
{
    left, center, right
}
public class PlayerController : MonoBehaviour
{
    public static CharacterController controller;
    Vector3 movement;
    public static float runningSpeed;
    float maxRunningSpeed;

    position currentPosition = position.center;
    public float distancePosition = 5f;
    public float jumpForce;
    float gravity = -20f;

    public static Animator m_animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        runningSpeed = 10f;
        maxRunningSpeed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.gameStarted)
            return;

        if (runningSpeed <= maxRunningSpeed)
            runningSpeed += 0.01f * Time.deltaTime;

        if(!m_animator.GetBool("isRunning"))
            StartCoroutine(gameStart());
        else 
            movement.z = runningSpeed;


        Jump();

        if (SwipeManager.swipeRight)
        {
            if (currentPosition == position.left)
                currentPosition = position.center;
            else if (currentPosition == position.center)
                currentPosition = position.right;
            m_animator.Play("dodgeRight");
            FindObjectOfType<AudioManager>().playSound("dodge");

        }
        if (SwipeManager.swipeLeft)
        {
            if (currentPosition == position.right)
                currentPosition = position.center;
            else if (currentPosition == position.center)
                currentPosition = position.left;
            m_animator.Play("dodgeLeft");
            FindObjectOfType<AudioManager>().playSound("dodge");


        }
        if (SwipeManager.swipeDown)
        {
            StartCoroutine(Rolling());
        }


        Vector3 targetPosition = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;
        if (currentPosition == position.left)
        {
            targetPosition += Vector3.left * distancePosition;
        }
        else if (currentPosition == position.right)
        {
            targetPosition += Vector3.right * distancePosition;
        }
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(movement * Time.deltaTime);
    }
    void Jump()
    {
        if (controller.isGrounded)
        {
            movement.y = -1;
            if (SwipeManager.swipeUp)
            {
                movement.y = jumpForce;
                StartCoroutine(Jumping());
            }
        }
        else
        {
            movement.y += gravity * Time.deltaTime;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
            StartCoroutine(death_bounce());
        else if (hit.transform.tag == "Train")
            StartCoroutine(death_movingTrain());

    }
    IEnumerator death_movingTrain()
    {
        m_animator.SetBool("isTrainHit", true);
        yield return new WaitForSeconds(1.440f);
        FindObjectOfType<AudioManager>().playSound("death");
        m_animator.SetBool("isTrainHit", false);
        PlayerManager.gameOver = true;

    }
    IEnumerator death_bounce()
    {
        m_animator.SetBool("isDead", true);
        yield return new WaitForSeconds(0.640f);
        FindObjectOfType<AudioManager>().playSound("death");
        m_animator.SetBool("isDead", false);
        PlayerManager.gameOver = true;

    }
    IEnumerator Rolling()
    {
        FindObjectOfType<AudioManager>().playSound("roll");
        m_animator.SetBool("isRolling", true);
        controller.height = 0.5f;
        controller.radius = 0.2f;
        controller.center = new Vector3(0, .5f, .2f);
        yield return new WaitForSeconds(0.520f);
        controller.height = 2f;
        controller.radius = 0.5f;
        controller.center = new Vector3(0, 1, .2f);
        m_animator.SetBool("isRolling", false);
    }
    IEnumerator gameStart()
    {
        m_animator.SetBool("isGameStarted", true);
        yield return new WaitForSeconds(1.2f);
        m_animator.SetBool("isGameStarted", false);
        m_animator.SetBool("isRunning", true);
    }
    IEnumerator Jumping()
    {
        FindObjectOfType<AudioManager>().playSound("jump");
        m_animator.SetBool("isJumping", true);
        controller.center = new Vector3(0, 1.2f, .2f);
        controller.height = 1.6f;
        yield return new WaitForSeconds(1);
        controller.center = new Vector3(0, 1, .2f);
        controller.height = 2f;
        m_animator.SetBool("isJumping", false);
    }
}
