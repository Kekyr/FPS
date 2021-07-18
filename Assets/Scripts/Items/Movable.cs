using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Actions.Fly(_destination, _rigidbody, gameObject);
    }

    private void OnMouseUp()
    {
        Actions.Fall(null, _rigidbody, gameObject);
    }

  
}
