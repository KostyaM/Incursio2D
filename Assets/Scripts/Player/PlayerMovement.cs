
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : DamageableComponent, CollisionListener
{
    public float movementSpeed = 2f;
    public float jumpForce = 4f;
    public bool isJumping = false;
    public Button jumnButton;
    public GameObject menu;
    public Joystick joystick;


    private bool isFacingLeft = true;

    private bool isPlayable = true;
    public override void DestroyElement(float delay)
    {
        isPlayable = false;
        menu.GetComponent<DefeatListener>().onDefeat();
        Destroy(gameObject, delay);
    }

    void Start()
    {
        OnStart();
        jumnButton.onClick.AddListener(Jump);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayable)
            return;
        var keyboardMovement = new Vector2(Input.GetAxis("Horizontal"), 0f);
        var joystickMovement = new Vector2(joystick.Horizontal, 0f);

        var movement = keyboardMovement.x != 0 ? keyboardMovement : joystickMovement;
        if (movement.x > 0 && isFacingLeft)
        {
            isFacingLeft = !isFacingLeft;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else if (movement.x < 0 && !isFacingLeft) 
        {
            isFacingLeft = !isFacingLeft;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

       transform.Translate(movement * Time.deltaTime * movementSpeed * (isFacingLeft ? 1 : -1));

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void Jump()
    {
        if (isJumping)
            return;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    public void onCollide()
    {
        isJumping = false;
    }

    public void onExitCollide()
    {
        isJumping = true;
    }
}
