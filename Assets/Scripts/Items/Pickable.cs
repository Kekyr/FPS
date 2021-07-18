using UnityEngine;

public class Pickable : MonoBehaviour, IActionable
{
    [SerializeField] private string _buttonName = "e";
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
            Destroy(gameObject);
        }
    }
}
