using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour {

	public bool isSurper=false;

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.name == "Pacman") {

			if (isSurper) {
				GameManager.Instance.OnEatPacdot (gameObject);
				GameManager.Instance.OnEatSuperPacdot ();
			} else {
				GameManager.Instance.OnEatPacdot (gameObject);
			}
			Destroy (this.gameObject);
		}
	}
}
