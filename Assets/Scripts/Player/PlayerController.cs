using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravityMultiplier;
    [SerializeField] private MouseLook _mouseLook;
    [SerializeField] private float _stepInterval;
    [SerializeField] private AudioClip[] _footstepSounds;

    private Camera _camera;
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _moveDir = Vector3.zero;
    private float _stepCycle;
    private float _nextStep;
    private AudioSource _audioSource;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.main;
        _stepCycle = 0f;
        _nextStep = _stepCycle / 2f;
        _mouseLook.Init(transform, _camera.transform);
    }

    private void Update()
    {
        RotateView();
    }
    private void FixedUpdate()
    {
        GetInput();

        Vector3 desiredMove = transform.forward * _input.y + transform.right * _input.x;

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, _characterController.radius, Vector3.down, out hitInfo,
                           _characterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        _moveDir.x = desiredMove.x * _speed;
        _moveDir.z = desiredMove.z * _speed;

        _moveDir += Physics.gravity * _gravityMultiplier * Time.fixedDeltaTime;

        _characterController.Move(_moveDir * Time.fixedDeltaTime);

        ProgressStepCycle(_speed);

        _mouseLook.UpdateCursorLock();
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _input = new Vector2(horizontal, vertical);

        if (_input.sqrMagnitude > 1)
        {
            _input.Normalize();
        }
    }

    private void ProgressStepCycle(float speed)
    {
        if (_characterController.velocity.sqrMagnitude > 0 && (_input.x != 0 || _input.y != 0))
        {
            _stepCycle += (_characterController.velocity.magnitude + speed) *
                         Time.fixedDeltaTime;
        }

        if (!(_stepCycle > _nextStep))
        {
            return;
        }

        _nextStep = _stepCycle + _stepInterval;

        PlayFootStepAudio();
    }

    private void PlayFootStepAudio()
    {
        int n = Random.Range(1, _footstepSounds.Length);
        _audioSource.volume = 0.2f;
        _audioSource.clip = _footstepSounds[n];
        _audioSource.PlayOneShot(_audioSource.clip);

        _footstepSounds[n] = _footstepSounds[0];
        _footstepSounds[0] = _audioSource.clip;
    }

    private void RotateView()
    {
        _mouseLook.LookRotation(transform, _camera.transform);
    }
}
