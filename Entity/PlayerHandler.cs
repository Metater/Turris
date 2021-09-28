using BitManipulation;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : EntityHandler
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hands;

    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    [SerializeField] private float momentumLerp = 0;

    private float rotationX = 0;
    private Vector3 moveDirection = Vector3.zero;
    private float lastSpeedX = 0;
    private float lastSpeedY = 0;

    public override void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical");
        float speedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal");
        if (speedX < lastSpeedX)
            speedX = Mathf.Lerp(speedX, lastSpeedX, momentumLerp);
        if (speedY < lastSpeedY)
            speedY = Mathf.Lerp(speedY, lastSpeedY, momentumLerp);
        lastSpeedX = speedX;
        lastSpeedY = speedY;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * speedX) + (right * speedY);

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
            moveDirection.y = jumpSpeed;
        else
            moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        BitWriter bw = new BitWriter();
        bw.Put(0, 2);
        bw.Put((int)((body.transform.position.x * 256f) + 8192), 14);
        bw.Put((int)((body.transform.position.y * 256f)), 14);
        bw.Put((int)((body.transform.position.z * 256f) + 8192), 14);
        bw.Put((byte)(body.transform.localEulerAngles.y / 1.41176470588f));
        bw.Put((byte)(rotationX / 1.41176470588f)); // representing more than you need
        GameManager.I.Send(bw.Assemble(), DeliveryMethod.Sequenced);
    }
}
