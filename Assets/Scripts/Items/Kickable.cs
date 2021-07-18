using UnityEngine;

public class Kickable : MonoBehaviour, IActionable
{
    [SerializeField] private string _buttonName = "f";
    [SerializeField] private Vector3 _kickForce = new Vector3(0, 0, 200);

    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public string ButtonKeyCode
    {
        get
        {
            return _buttonName;
        }
        set
        {
            _buttonName = value;
        }
    }

    public void Action()
    {
        if (Input.GetKeyDown(_buttonName))
        {
            _rigidbody.AddForce(_kickForce);
        }
    }
}
