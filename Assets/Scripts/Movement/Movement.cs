using UnityEngine;
using UnityEngine.AI;
using R3;
using System;
using DG.Tweening;
using Nowhere.Helper;
using Nowhere.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float goStraightAcceleration = 6f;
    [SerializeField] private float turnAroundAcceleration = 12f;

    private NavMeshAgent _agent;
    private Animator _animator;

    private IDisposable _groundHitSubscription;
    private bool _canControl = true;
    private bool _isProcedureMoving = false;
    private bool _waitUpdatePath = false;
    private Action _onManualMovePlayerTo;

    RayCastHitComparer _rayCastHitComparer = new();
    private RaycastHit[] _groundDetectionHits = new RaycastHit[5];

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
        MatchMovementAnimationWithTerrain();

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

        if (Vector3.Angle(transform.forward.WithoutY(), (_agent.destination - transform.position).WithoutY()) > 10)
        {
            _agent.acceleration = turnAroundAcceleration;
        }
        else
        {
            _agent.acceleration = goStraightAcceleration;
        }

        _animator.SetFloat(AnimationHash.speedHash, _agent.velocity.magnitude / _agent.speed);
    }

    private void MatchMovementAnimationWithTerrain()
    {
        Ray groundCheckRay = new Ray(transform.position, -transform.up * 5);

        int hits = Physics.RaycastNonAlloc(groundCheckRay, _groundDetectionHits, 5, LayerMask.GetMask("Ground"));
        if(hits > 0)
        {
            Array.Sort(_groundDetectionHits, 0, hits, _rayCastHitComparer);
            if(_groundDetectionHits[0].collider.TryGetComponent(out Terrain terrain))
            {
                switch (terrain.TerrainType)
                {
                    case TerrainType.Land:
                        _animator.SetInteger(AnimationHash.runTypeHash, 0);
                        break;
                    case TerrainType.Water:
                        _animator.SetInteger(AnimationHash.runTypeHash, 1);
                        break;
                    default:
                        break;
                }
            }
        }
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

    private class RayCastHitComparer : IComparer<RaycastHit>
    {
        public int Compare(RaycastHit x, RaycastHit y)
        {
            return x.distance > y.distance ? 1 : 0;
        }
    }
}

