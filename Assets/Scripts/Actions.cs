using UnityEngine;

public abstract class Actions : MonoBehaviour
{
    public static void Fly(Transform destination, Rigidbody rigidbody, GameObject gameObject)
    {
        IsGravityAndRotationOn(false, rigidbody);
        ChangeParent(destination,gameObject);
        gameObject.transform.position = destination.position;
    }

    public static void Fall(Transform destination, Rigidbody rigidbody, GameObject gameObject)
    {
        IsGravityAndRotationOn(true, rigidbody);
        ChangeParent(destination,gameObject);
    }

    private static void IsGravityAndRotationOn(bool isOn, Rigidbody rigidbody)
    {
        rigidbody.useGravity = isOn;
        rigidbody.freezeRotation = !isOn;
    }

    private static void ChangeParent(Transform newParent, GameObject gameObject)
    {
        gameObject.transform.parent = newParent;
    }
}
