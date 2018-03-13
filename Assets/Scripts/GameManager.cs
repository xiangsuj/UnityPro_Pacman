using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public static GameManager Instance{
		get{ 
			return _instance; 
		}
	}
	public GameObject pacman;
	public GameObject blinky;
	public GameObject clyde;
	public GameObject inky;
	public GameObject pinky;
	public GameObject maze;
	public bool isSuperPacman = false;
	public GameObject startPanel;
	public GameObject gamePanel;
	public GameObject startTimerPrefab;
	public GameObject gameoverPrefab;
	public GameObject winPrefab;
	public AudioClip startClip;
	public Text scoreText;
	public Text remainText;
	public GameObject directionButton;


	public List<int> usingIndex=new List<int>();
	public List<int> rawIndex = new List<int>{ 0, 1, 2, 3 };

	private List<GameObject>pacdotGos=new List<GameObject>();
	private int pacdotNum = 0; 
	private int nowEat=0;
	public int score=0;

	private void Awake(){
		_instance = this;
		int tempCount = rawIndex.Count;
		for (int i = 0; i < tempCount; i++) {
			
			int tempIndex = Random.Range (0, rawIndex.Count);
			usingIndex.Add (rawIndex[tempIndex]);
			rawIndex.RemoveAt (tempIndex);
		}
		foreach (Transform t in maze.GetComponent<Transform>()) {
			pacdotGos.Add (t.gameObject);
		}

		pacdotNum = maze.transform.childCount;

	}



	private void Start(){
		SetGameState (false);


	}

	private void Update(){
		if (nowEat == pacdotNum&&pacman.GetComponent<PacmanMove>().enabled!=false) {
			gamePanel.SetActive (false);
			Instantiate (winPrefab);
			StopAllCoroutines ();
			SetGameState (false);
		}
		if (nowEat == pacdotNum) {
			
				UnityEngine.SceneManagement.SceneManager.LoadScene (1);

		}
		if (gamePanel.activeInHierarchy) {
			remainText.text = "Remain\n\n" + (pacdotNum - nowEat);
			scoreText.text = "Score\n\n" + score;
		}
	}

	public void OnStartButton(){
		StartCoroutine (PlayStartTimer ());
		AudioSource.PlayClipAtPoint (startClip, Vector3.zero);
		startPanel.SetActive (false);



	}

	public void OnExitButton(){
		Application.Quit ();
	}
	public void OnEatPacdot(GameObject go){
		nowEat++;
		score += 100;
		pacdotGos.Remove (go);
	}
	IEnumerator PlayStartTimer(){
		GameObject go = Instantiate (startTimerPrefab);
		yield return new WaitForSeconds (4f);
		Destroy (go);
		SetGameState (true);
		Invoke ("CreateSuperPacdot", 10f);
		gamePanel.SetActive (true);
		directionButton.SetActive (true);
		GetComponent<AudioSource> ().Play ();
	}
	public void OnEatSuperPacdot(){
		score += 200; 
		Invoke("CreateSuperPacdot",10f);
			isSuperPacman=true;
		FreezeEnemy ();
		StartCoroutine (RecoveryEnemy ());

	}
	private void CreateSuperPacdot(){
		if (pacdotGos.Count < 10) {
			return;
		}
		int tempIndex = Random.Range (0, pacdotGos.Count);
		pacdotGos [tempIndex].transform.localScale = new Vector3 (3, 3, 3);
		pacdotGos [tempIndex].GetComponent<Pacdot> ().isSurper = true;
	}

	private void FreezeEnemy(){
		blinky.GetComponent<GhostMove> ().enabled = false;
		clyde.GetComponent<GhostMove> ().enabled = false;
		inky.GetComponent<GhostMove> ().enabled = false;
		pinky.GetComponent<GhostMove> ().enabled = false;

		blinky.GetComponent<SpriteRenderer> ().color = new Color (0.7f, 0.7f, 0.7f,0.7f);
		clyde.GetComponent<SpriteRenderer> ().color = new Color (0.7f, 0.7f, 0.7f,0.7f);
		inky.GetComponent<SpriteRenderer> ().color = new Color (0.7f, 0.7f, 0.7f,0.7f);
		pinky.GetComponent<SpriteRenderer> ().color = new Color (0.7f, 0.7f, 0.7f,0.7f);

	}
	private void DisFreezeEnemy(){
		blinky.GetComponent<GhostMove> ().enabled = true;
		clyde.GetComponent<GhostMove> ().enabled = true;
		inky.GetComponent<GhostMove> ().enabled = true;
		pinky.GetComponent<GhostMove> ().enabled = true;

		blinky.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);
		clyde.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);
		inky.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);
		pinky.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);

	}

	IEnumerator RecoveryEnemy(){
		yield return new WaitForSeconds (3f);
		DisFreezeEnemy ();
		isSuperPacman = false;
	}

	private void SetGameState(bool state){
		pacman.GetComponent<PacmanMove> ().enabled = state;
		blinky.GetComponent<GhostMove> ().enabled = state;
		clyde.GetComponent<GhostMove> ().enabled = state;
		inky.GetComponent<GhostMove> ().enabled = state;
		pinky.GetComponent<GhostMove> ().enabled = state;

	}
}
