using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GhostMove : MonoBehaviour {
	 
	public float speed=0.1f;
	public GameObject[] wayPointGos;

	private List<Vector3> wayPoints = new List<Vector3> (); 
	private int index=0;
	private Vector3 startPos;
	void Start(){
		startPos = transform.position + new Vector3 (0, 3, 0);
		LoadAPath (wayPointGos [GameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder-2]]);
	}
	private void FixedUpdate(){
		if (transform.position != wayPoints [index]) {
			Vector2 temp =  Vector2.MoveTowards (transform.position, wayPoints[index] , speed);
			GetComponent<Rigidbody2D> ().MovePosition (temp);
		} else {
			index++;
			if (index >= wayPoints.Count) {
				index = 0;
				LoadAPath (wayPointGos [Random.Range (0, wayPointGos.Length)]);
			}

		}
		Vector2 dir = wayPoints[index]  - transform.position;
		//把获取到的移动方向设置给动画状态机
		GetComponent<Animator>().SetFloat("DirectionX", dir.x);
		GetComponent<Animator>().SetFloat("DirectionY", dir.y);
	}

	private void LoadAPath(GameObject go){
		wayPoints.Clear ();
		foreach (Transform t in go.transform) {
			wayPoints.Add (t.position);
		}
		wayPoints.Insert (0, startPos);
		wayPoints.Add (startPos);
	}
	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.name == "Pacman") {
			if (GameManager.Instance.isSuperPacman) {
				transform.position = startPos - new Vector3 (0, 3, 0);
				index = 0;
				GameManager.Instance.score += 500;
			} else {
				collision.gameObject.SetActive (false);
				GameManager.Instance.gamePanel.SetActive (false);
				GameManager.Instance.directionButton.SetActive (false);

				Instantiate (GameManager.Instance.gameoverPrefab);

				Invoke ("ReStart", 3f);
			}

		}
	}

	private void ReStart(){
		SceneManager.LoadScene (0);
	}
}
