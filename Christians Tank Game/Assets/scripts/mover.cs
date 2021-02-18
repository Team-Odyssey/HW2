using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public GameObject projectile;
    public int bulletCounter = 5;
    public float bulletSpeed = 100f;
    [SerializeField] public float speed = 6f;

    [SerializeField] public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(-horizontal, 0f, -vertical).normalized;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 6;
        }

        //Shoot Bullet
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instBullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            Rigidbody instbulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instbulletRigidbody.AddForce(Vector3.forward * bulletSpeed);
        }

        if (direction.magnitude >= 0.1f){

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(-moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
