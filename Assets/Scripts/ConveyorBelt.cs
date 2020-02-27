using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public enum Direction
{
    Left,
    Right
}

[RequireComponent(typeof(SpriteRenderer))]
public class ConveyorBelt : MonoBehaviour
{
    public float velocity;
    public float shaderVelocity;
    public Direction direction;
    private static readonly int ScrollSpeed = Shader.PropertyToID("_ScrollSpeed");

    private readonly List<Rigidbody2D> _objectsOnBelt = new List<Rigidbody2D>();

    public void OnEnable()
    {
        Vector2 scrollVelocity;
        switch (direction)
        {
            case Direction.Left:
                scrollVelocity = Vector2.right * shaderVelocity;
                break;
            case Direction.Right:
                scrollVelocity = -Vector2.right * shaderVelocity;
                break;
            default:
                scrollVelocity = Vector2.zero;
                break;

        }
        GetComponent<SpriteRenderer>().material.SetVector(ScrollSpeed, scrollVelocity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _objectsOnBelt.Add(other.rigidbody);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _objectsOnBelt.Remove(other.rigidbody);
    }

    private void Update()
    {

        Vector2 movementDirection;
        switch (direction)
        {
            case Direction.Left:
                movementDirection = -transform.right;
                break;
            case Direction.Right:
                movementDirection = transform.right;
                break;
            default:
                movementDirection = Vector2.zero;
                break;
        }

        var movedDistance = movementDirection * (velocity * Time.deltaTime);
        _objectsOnBelt.ForEach(rigidBody => { rigidBody.MovePosition(rigidBody.position + movedDistance); });
    }
}
