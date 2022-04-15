using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance;

	public VariableJoystick _joyStick;
	public Vector3 direction;
	
	public float movementSpeed = 5f;
	[SerializeField] private float xForce;
	public float xSpeed;
	private float _swipeSpeed = 3f;

	public List<GameObject> childList;
	public bool isGrounded;
	public Vector3 shrinkPos;

	public float minimumDistanceBetweenChildren = 1f;
	
	[HideInInspector]
	public float h;
	[HideInInspector]
	public float v;

	private Rigidbody _rb;

	public GameObject snakeObject;
	private Material _snakeMaterial;
	private void Awake()
	{
		if (Instance)
			Destroy(gameObject);
		else
			Instance = this;
		
		for (int i = 0; i < childList.Count; i++) childList[i].GetComponent<ChildFollow>().myId = i + 1;
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_snakeMaterial = snakeObject.GetComponent<MeshRenderer>().material;
		

	}

	private void FixedUpdate()
	{
		Move();

		if (Input.GetKeyDown(KeyCode.A))
		{
			var lerpValue = 0.1f;
			_snakeMaterial.SetFloat("_Panner",Mathf.Lerp(1.3f, -0.3f, lerpValue));
		}
	}

	
	private void Move()
    {
		h = _joyStick.Horizontal;
        v = _joyStick.Vertical;
        
		var inputdir = new Vector3(h , 0, v);
       
        inputdir.Normalize();

		var velocity = inputdir * movementSpeed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
		
		
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Shrink"))
		{
			print("Here");
			shrinkPos = other.transform.position;
			for (int i = 2; i < other.gameObject.GetComponent<ChildFollow>().myId; i++)
			{
				//  print(other.gameObject.GetComponent<ShrinkCheck>().id);
				//snakeBodyParts[i].transform.GetChild(0).GetComponent<Collider>().enabled = false;
				childList[i].transform.GetChild(0).GetComponent<ChildFollow>().isShrink = true;
			}
		}
	}
}
