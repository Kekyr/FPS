using UnityEngine;

public class PlayerColliderListener : MonoBehaviour
{
    private bool _isCollided;
    private IActionable _item;

    private void Update()
    {
        if (_isCollided)
        {
            if (_item != null)
            {
                _item.Action();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        _isCollided = true;
        _item = collider.gameObject.GetComponent<IActionable>();
    }
}
