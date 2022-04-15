using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFollow : MonoBehaviour
{
	public Transform target;
    private float _smoothTime = .125f;
    private Vector3 _velocity;
    public bool isClimbing;
    public bool isHitByBlock;

	public float snakeBodyFollowSpeed = 4.99f;
	public int myId;
	public bool isShrink;

	private void Start()
	{
		target = transform.parent.GetChild(myId - 1);
	}
    private void Update()
    {
        var dist = Vector3.Distance(transform.position, target.position);
        if (isShrink)
        {
            var distToShrink = Vector3.Distance(transform.position, PlayerMovement.Instance.shrinkPos);
            transform.position = Vector3.MoveTowards(transform.position, PlayerMovement.Instance.shrinkPos,
                2 * Time.deltaTime);
            if (distToShrink <= .65)
            {
                isShrink = false;
            }

        }
        if ( isHitByBlock)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position,
               snakeBodyFollowSpeed * 5 * Time.deltaTime);
        }
        if (!(dist >= PlayerMovement.Instance.minimumDistanceBetweenChildren)) return;
        if (!PlayerMovement.Instance.isGrounded)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 
                10f * Time.deltaTime);
            return;
        }
        if(isClimbing)
        {
            Transform transform2;
            (transform2 = transform).LookAt(target);
            transform.position = Vector3.SmoothDamp(transform2.position, target.position, ref _velocity, _smoothTime,
                snakeBodyFollowSpeed + 2,Time.deltaTime);
		}
       
        else
        {
            Transform transform1;
            (transform1 = transform).LookAt(target);
            transform.position = Vector3.MoveTowards(transform1.position, target.position, 
               snakeBodyFollowSpeed * Time.deltaTime);
        }
    }
}
