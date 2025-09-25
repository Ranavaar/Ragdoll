using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
	#region Fields
	[SerializeField] private Transform _target;
	[SerializeField] private Vector3 _offset = new Vector3(0, 2, -10); 
	[SerializeField] private float _smoothSpeed = 0.125f;
	[SerializeField] private float _mouseSensitivity = 2f;
	private float _rotationX;
	private float _rotationY;
	#endregion

	#region Unity Callbacks
	private void Start()
    {

    }

	void Update()
    {
		float horizontal = Input.GetAxis("Mouse X") * _mouseSensitivity;
		float vertical = -Input.GetAxis("Mouse Y") * _mouseSensitivity;
		transform.RotateAround(_target.position, Vector3.up, horizontal);
		transform.RotateAround(_target.position, transform.right, vertical);
		transform.position = _target.position + (transform.position - _target.position).normalized * _offset.magnitude;
		transform.LookAt(_target.position + Vector3.up * 1.5f);

	}
	#endregion
}
