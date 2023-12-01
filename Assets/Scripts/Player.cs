using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    public float MovementDeadZone = 0.02f;
    public float MovementSmoothing = 0.5f;
    public float MovementSpeed = 3;
    public float JumpVelocity;

    enum RelativePosition
    {
        Above,
        Below,
        Aside,
        Inside
    }

    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity = rigidbody.velocity;
        float currentVelocity = 0;
        velocity.x = Mathf.SmoothDamp(velocity.x, 
            MathUtils.ProcessDeadZone(Input.acceleration.x, MovementDeadZone) * MovementSpeed * Time.deltaTime // ��� ���������� ����������
            + MathUtils.ProcessDeadZone(Input.GetAxis("Horizontal"), MovementDeadZone) * MovementSpeed * Time.deltaTime // ��� ������������ � ��������� � �������������� ������
            ,ref currentVelocity, MovementSmoothing);

        rigidbody.velocity = velocity;

        // ���� ����� �� ��������� ������, ���������� ��� �� ����� �� ������ �� � ��������������� ������� �� �����������
        Vector3 playerViewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (playerViewportPosition.x > 1.1f || playerViewportPosition.x < -0.1f)
        {
            playerViewportPosition.x = playerViewportPosition.x > 0 ? 0 : 1;
            transform.position = Camera.main.ViewportToWorldPoint(playerViewportPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������� ��������� ������� ������������ ������
        RelativePosition relPosition = CheckRelativePosition(other);
        switch (relPosition)
        {
            case RelativePosition.Inside:
            case RelativePosition.Below:
                if (other.gameObject.GetComponent<HighJumpPlatform>())
                {
                    HighJump();
                }
                else if (other.gameObject.GetComponent<BreakingPlatform>())
                {
                    Jump();
                } // ��������� �������� �� ����, ��� ����� ����� ��������
                else if (other.gameObject.GetComponent<Platform>())
                {
                    Jump();
                }
                other.gameObject.GetComponent<InteractableObject>().Interact();
                break;
        }
    }

    private void Jump()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.y = JumpVelocity;
        rigidbody.velocity = velocity;
    }

    private void HighJump()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.y = JumpVelocity * 1.5f;
        rigidbody.velocity = velocity;
    }

    /// <summary>
    /// ���������� ��������� ������� ������������ ������
    /// </summary>
    /// <param name="collision">��������� �������, ��������� �������� �� ���������</param>
    /// <returns></returns>
    private RelativePosition CheckRelativePosition(Collider2D collision)
    {
        if (transform.position.y < collision.transform.position.y)
            return RelativePosition.Above;
        else if (transform.position.y > collision.transform.position.y)
            return RelativePosition.Below;
        else if (transform.position.x < collision.transform.position.x)
            return RelativePosition.Aside;
        else if (transform.position.x > collision.transform.position.x) // ������ ��� ���������� ������� ��� ���������� ����
            return RelativePosition.Aside;
        else
            return RelativePosition.Inside;

    }
}
