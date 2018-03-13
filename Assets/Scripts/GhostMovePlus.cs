using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovePlus : MonoBehaviour {
	public float speed=0.1f;
	public GameObject[] wayPointGos;

	private List<Vector3> wayPoints = new List<Vector3> (); 
	private int index=0;
	//private Vector3 startPos;
	void Start(){
		//startPos = transform.position + new Vector3 (0, 3, 0);
		LoadAPath (wayPointGos [0]);
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
		//wayPoints.Insert (0, startPos);
		//wayPoints.Add (startPos);
	}
}
