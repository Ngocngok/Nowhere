using UnityEngine;
using UnityEngine.AI;
using UniRx;
using System;
using DG.Tweening;
using Nowhere.Helper;

public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private float speed = 2.5f;

    private NavMeshAgent _agent;
    private Animator _animator;

    private IDisposable _groundHitSubscription;
    private bool _canControl = true;
    private bool _isProcedureMoving = false;
    private bool _waitUpdatePath = false;
    private Action _onManualMovePlayerTo;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _onManualMovePlayerTo = null;
        _agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (_isProcedureMoving && _agent.remainingDistance < .15f)
        {
            if (_waitUpdatePath)
            {
                _waitUpdatePath = false;
                return;
            }

            //destination reached
            if(_onManualMovePlayerTo != null)
            {
                _onManualMovePlayerTo.Invoke();
                _onManualMovePlayerTo = null;
            }

        }

        _animator.SetFloat(AnimationHash.speedHash, _agent.velocity.magnitude / _agent.speed);
    }

    private void MoveToPlayerInput(Vector3 destination)
    {
        if (!_canControl || _isProcedureMoving)
        {
            return;
        }

        _agent.SetDestination(destination);
        _waitUpdatePath = true;
    }

    public void EndProcedureMove()
    {
        _isProcedureMoving = false;
        _canControl = true;
    }

    public void ProcedureMoveTo(Vector3 targetDestination, Quaternion targetRotation, Action onDestinationReached)
    {
        _isProcedureMoving = true;
        _canControl = false;
        _agent.SetDestination(targetDestination);
        _waitUpdatePath = true;

        // mannually move character to exact position and rotation
        _onManualMovePlayerTo += () =>
        {
            //float timeToMove = (destination - _agent.transform.position).WithoutY().magnitude / speed;
            float timeToMove = .5f;
            Vector3 initialPosition = _agent.transform.position;
            Quaternion initialRotation = _agent.transform.rotation;
            DOVirtual.Float(0, 1f, timeToMove, (x) =>
            {
                _agent.transform.SetPositionAndRotation(Vector3.Lerp(initialPosition, targetDestination, x), Quaternion.Slerp(initialRotation, targetRotation, x));
            })
            .OnComplete( () =>
            {
                onDestinationReached?.Invoke();
            });
        };
    }

    public void OnEnable()
    {
        _groundHitSubscription = PlayerInputHandler.Instance.GroundHitAsObservable.Subscribe(MoveToPlayerInput);
    }

    public void OnDisable()
    {
        _groundHitSubscription.Dispose();
    }

    public void OnDestroy()
    {
        _groundHitSubscription.Dispose();
    }
}
