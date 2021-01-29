﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour {

	public Transform[] _firePoint;
	public GameObject _cannonBallPrefab;
	[Header("Firerate")]
	public float _fireRate;
	private float _fireRateTimer;
	public float _rayDistance;
	public LayerMask _mask;
	PoolManager _poolMG;


	// Use this for initialization
	void Start () {
		Physics2D.queriesStartInColliders = false;
		_poolMG = PoolManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		CannonDetect();
	}

	void CannonDetect() {
		RaycastHit2D _hitInfo2d = Physics2D.Raycast(transform.position, -transform.right, _rayDistance, _mask);
		if(_hitInfo2d.collider != null) {
			CannonShoot();
			Debug.DrawLine(transform.position, _hitInfo2d.point, Color.red);
		} else {
			Debug.DrawLine(transform.position, transform.position - transform.right * _rayDistance, Color.green);
		}

		RaycastHit2D _hitInfo2d2 = Physics2D.Raycast(transform.position, transform.right, _rayDistance, _mask);
		if(_hitInfo2d2.collider != null) {
			CannonShoot();
			Debug.DrawLine(transform.position, _hitInfo2d2.point, Color.red);
		} else {
			Debug.DrawLine(transform.position, transform.position + transform.right * _rayDistance, Color.red);
		}
	}

	void CannonShoot() {
		if(_fireRateTimer <= 0) {
			for(int i = 0; i < _firePoint.Length; i++) {
				_poolMG.SpawnFromPool("e_cannonball", _firePoint[i].position, _firePoint[i].rotation);				
				// Instantiate(_cannonBallPrefab, _firePoint[i].position, _firePoint[i].rotation);
			}
			_fireRateTimer = _fireRate;
		} else {
			_fireRateTimer -= Time.deltaTime;
		}
	}
}
