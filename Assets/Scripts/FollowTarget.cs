using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    [SerializeField] CircleCollider2D _CircleCollider;
    [SerializeField] float _CircleColliderradius = 5;
    [SerializeField] string _TargetTag = "Player";
    private bool _bDoHunt = false;
    private GameObject _Target;
    [SerializeField] float _MinDistance = 1;

    [SerializeField] float _Speed = 1;

    private bool _bDoReturnToWaitSpot = false;
    private Vector2 _WaitSpotPosition;
    [SerializeField] float _MaxDistanceFromWaitSpot = 10;

    // Start is called before the first frame update
    void Start()
    {
        _CircleCollider.radius = _CircleColliderradius;
        _CircleCollider.isTrigger = true;
        CompareTag(_TargetTag);
        _WaitSpotPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (_bDoHunt)
        {
            float distance = Vector2.Distance(_Target.transform.position, transform.position);

            if (distance > _MinDistance)
            {
                Vector2 direction = (_Target.transform.position - transform.position).normalized;
                transform.parent.Translate(direction * _Speed * Time.deltaTime, Space.World);
            }
        }

        if (_bDoReturnToWaitSpot)
        {
            float distance = Vector2.Distance(_WaitSpotPosition, transform.position);

            if (distance > _MinDistance)
            {
                Vector2 direction = (_WaitSpotPosition - (Vector2)transform.position).normalized;

                transform.parent.Translate(direction * _Speed * Time.deltaTime, Space.World);
            }

        }

        if (Vector2.Distance(_WaitSpotPosition, transform.position) > _MaxDistanceFromWaitSpot)
        {
            _bDoHunt = false;
            _bDoReturnToWaitSpot = true;
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_TargetTag))
        {
            _Target = collision.gameObject;
            _bDoHunt = true;
            _bDoReturnToWaitSpot = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_Target != null && _Target == collision.gameObject)
        {
            _Target = null;
            _bDoHunt = false;
            _bDoReturnToWaitSpot = true;
        }
    }
}
