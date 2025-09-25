using UnityEngine;
using System;

public class Explosion : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Camera _mainCamera;
	[SerializeField] private Camera _explosionCamera;
	[SerializeField] private GameObject _effect;
	[SerializeField] private float _explosionArea;
	[SerializeField] private float _explosionForce = 1000;
	[SerializeField] private int _speed = 2;
	private GameObject _playerHips;
	private Rigidbody _playerRB;
	#endregion

	#region Unity Callbacks
	private void Update()
	{
		if (_playerHips != null && Vector3.Distance(_playerHips.transform.position, _explosionCamera.transform.position) > 5)
		{
			_explosionCamera.transform.LookAt(_playerHips.transform.position);
			_explosionCamera.transform.Translate(_explosionCamera.transform.forward * _speed * Time.deltaTime);

			if(_playerRB.velocity.magnitude < 3)
			{
				_mainCamera.enabled = true;
				_explosionCamera.enabled = false;
				Vector3 currentPos = _playerHips.transform.position;
				_playerHips.transform.localPosition = Vector3.zero;
				_playerHips.transform.parent.GetComponent<CharacterController>().enabled = false;
				_playerHips.transform.parent.position = currentPos;
				_playerHips.transform.parent.GetComponent<CharacterController>().enabled = true;
				//levantarse
				_playerHips.transform.parent.GetComponent<Animator>().enabled = true;
				Destroy(gameObject);
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		_mainCamera.enabled = false;
		_explosionCamera.enabled = true;


		_effect.SetActive(true);
		Animator playerAnim =other.GetComponentInParent<Animator>();
		_playerHips = playerAnim.transform.GetChild(0).gameObject;
		_playerRB = playerAnim.GetComponentInChildren<Rigidbody>();
		if (playerAnim != null)
			playerAnim.enabled = false;
		ExplosionForce();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _explosionArea);
	}
	private void ExplosionForce()
	{
		Collider[] objects = Physics.OverlapSphere(transform.position, _explosionArea);
		for (int i = 0; i < objects.Length; i++)
		{
			Rigidbody objectsRB = objects[i].GetComponent<Rigidbody>();
			if (objectsRB != null)
			{
				objectsRB.AddExplosionForce(_explosionForce, transform.position, _explosionArea);
			}
		}
	}
	#endregion

}
