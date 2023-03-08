using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
	public Vector2 timeGenerateDelayRange = new Vector2(1f, 3f);
	public List<GameObject> obstaclePrefabs;

	//public List<float> obstaclePositions;
	public float createDistance = 25f;
	public float resetDistance = 75f;
	public float killDistance = -150f;

	[Header("Read-only")]
	public float timeUntilGenerate = 3f;

	private Transform enemyCollectionTrans;

	private void Start() {
		enemyCollectionTrans = transform.Find("EnemyCollection");
		
	}


	public void Generate()
	{
		int randObjIndex = Random.Range(0, obstaclePrefabs.Count);
		GameObject prefebOj = GameObject.Instantiate(obstaclePrefabs[randObjIndex]);
		prefebOj.transform.parent = enemyCollectionTrans;
		prefebOj.transform.position = new Vector3(0f, 0f, GameManager.playerXRig.transform.position.z + createDistance);

	}
	public void FixedUpdate() {
		timeUntilGenerate -= Time.deltaTime;
		if (timeUntilGenerate <= 0f) {
			timeUntilGenerate = Random.Range(timeGenerateDelayRange.x, timeGenerateDelayRange.y);
			Generate();
		}
	}

	public void PushBackEnemies() {
		for (int i = 0; i < enemyCollectionTrans.childCount; i++) {
			enemyCollectionTrans.GetChild(i).transform.position = new Vector3(0f, 0f, enemyCollectionTrans.GetChild(i).transform.position.z + resetDistance);

			if (enemyCollectionTrans.GetChild(i).transform.position.z <= killDistance) {
				Destroy(enemyCollectionTrans.GetChild(i).gameObject);
			}
		}
	}

}
