using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeManage : MonoBehaviour {


	void Start(){
		Invoke ("changeToMain", 40f);
	}
	void changeToMain(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
}
