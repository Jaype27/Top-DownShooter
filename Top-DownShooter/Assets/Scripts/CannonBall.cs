﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour, IPooledObject {

	public float _speed;
	private Rigidbody2D rb2d;
	public float _activeSpanLife;
	private float _activeTimer;
	public float _damageGiven;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void OnEnable () {
		_activeTimer = _activeSpanLife;
	}

	public void OnObjectSpawn() {
		rb2d.velocity = transform.up * _speed;
	}
	
	// Update is called once per frame
	void Update () {
		OnObjectSpawn();
		_activeTimer -= Time.deltaTime;
		if(_activeTimer <= 0f) {
			this.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		EnemyHealth _enemyHealth = other.GetComponent<EnemyHealth>();

		if(other.gameObject.tag == "Enemy") {
			_enemyHealth.DamageTaken(_damageGiven);
			this.gameObject.SetActive(false);
			// Damage Enemy
			
		}

		if(other.gameObject.tag == "Obstacle") {
			this.gameObject.SetActive(false);
			
		}
	}
}
