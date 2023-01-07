using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;


public class Spawner : MonoBehaviour
{

	private Vector3 direction;
	private bool findSpawnLoc;
	private bool findWallSpawnLoc;

	public bool isMine = false;
	float time;
	float time_2;

	private PhotonView photonView;
	public LayerMask levelMask;
	public LayerMask levelMask_2;



	public GameObject bombColPrefab;
	public GameObject wallPrefab;
	public GameObject ceilingPrefab;



	


	public GameObject[] no_of_walls;



	string collectibleName;

	int x;
	int z;

	// Start is called before the first frame update




	void Awake()
	{
		photonView = GetComponent<PhotonView>();
	}

	void Start()
	{
		//detect if there's ceiling
		direction = new Vector3(0, 1, 0);
		time = Random.Range(3, 15);

		time_2 = Random.Range(10, 30);
		findSpawnLoc = false;
		findWallSpawnLoc = false;



		for (int i = 0; i < 40;)
		{
			Vector3 top = new Vector3(1, 0, 0);
			Vector3 bottom = new Vector3(-1, 0, 0);
			Vector3 left = new Vector3(0, 0, 1);
			Vector3 right = new Vector3(0, 0, -1);
			Vector3 left_top = new Vector3(1, 0, 1);
			Vector3 left_bottom = new Vector3(-1, 0, 1);
			Vector3 right_top = new Vector3(1, 0, -1);
			Vector3 right_bottom = new Vector3(-1, 0, -1);
			Vector3 ceilingDirection = new Vector3(0, 1, 0);

			//x = Random.Range(-1, 9);
			//z = Random.Range(-17, -1);
			int x = Random.Range(0, 8);
			int z = Random.Range(-16, -2);

			
			GameObject Wall = PhotonNetwork.Instantiate("Wall", new Vector3(x, 0, z), wallPrefab.transform.rotation);
			RaycastHit hit_top;
			Physics.Raycast(Wall.transform.position, top, out hit_top, 2, levelMask);
			RaycastHit hit_bottom;
			Physics.Raycast(Wall.transform.position, bottom, out hit_bottom, 2, levelMask);
			RaycastHit hit_left;
			Physics.Raycast(Wall.transform.position, left, out hit_left, 2, levelMask);
			RaycastHit hit_right;
			Physics.Raycast(Wall.transform.position, right, out hit_right, 2, levelMask);
			RaycastHit hit_left_top;
			Physics.Raycast(Wall.transform.position, left_top, out hit_left_top, 1.0f, levelMask);
			RaycastHit hit_left_bottom;
			Physics.Raycast(Wall.transform.position, left_bottom, out hit_left_bottom, 1.0f, levelMask);
			RaycastHit hit_right_top;
			Physics.Raycast(Wall.transform.position, right_top, out hit_right_top, 1.0f, levelMask);
			RaycastHit hit_right_bottom;
			Physics.Raycast(Wall.transform.position, right_bottom, out hit_right_bottom, 1.0f, levelMask);
			RaycastHit hit_ceiling;
			Physics.Raycast(Wall.transform.position, ceilingDirection, out hit_ceiling, 1.0f, levelMask_2);

			if (((hit_top.collider && hit_left_top.collider && hit_right_top.collider)
				  ||
				  (hit_left.collider && hit_left_top.collider && hit_left_bottom.collider)
				  ||
				  (hit_bottom.collider && hit_left_bottom.collider && hit_right_bottom.collider)
				  ||
				  (hit_right.collider && hit_right_top.collider && hit_right_bottom.collider))
				 && hit_ceiling.collider)

			{
				PhotonNetwork.Destroy(Wall);
			}
			else
			{
				//instantitate the ceiling
				GameObject Ceiling = PhotonNetwork.Instantiate("Ceiling", new Vector3(Wall.transform.position.x, Wall.transform.position.y + 1,
					Wall.transform.position.z), ceilingPrefab.transform.rotation);
				Debug.Log("Ceiling " + i + " spawned in x:" + Wall.transform.position.x + " z:" + Wall.transform.position.z);
				i++;


			}

		}

	}

	public void SearchingSpawnLoc()
	{

		x = Random.Range(-1, 9);
		z = Random.Range(-17, -1);
		int randomGenerator = Random.Range(1, 3);


		if (randomGenerator == 1)
		{
			collectibleName = "BombCollectible";
		}
		else if (randomGenerator == 2)
		{
			collectibleName = "StrongBombCollectible";
		}
		else if (randomGenerator == 3)
		{
			collectibleName = "SpreadBombCollectible";
		}


		//spawn the bomb colectible at a random position
		GameObject BombCol = PhotonNetwork.Instantiate(collectibleName, new Vector3(x, 0, z), bombColPrefab.transform.rotation);

		RaycastHit hit;
		Physics.Raycast(BombCol.transform.position, direction, out hit, 1, levelMask);

		if (!hit.collider)
		{
			time = /*3.0f;*/ Random.Range(3, 15);
			findSpawnLoc = false;
		}
		else
		{
			PhotonNetwork.Destroy(BombCol);
		}

		//}
	}


	public void SearchingWallSpawnLoc()
	{
		x = Random.Range(-1, 9);
		z = Random.Range(-17, -1);

		//spawn the bomb colectible at a random position
		GameObject Wall = PhotonNetwork.Instantiate("Wall", new Vector3(x, 0, z), wallPrefab.transform.rotation);

		RaycastHit hit;
		Physics.Raycast(Wall.transform.position, direction, out hit, 1, levelMask);

		if (!hit.collider)
		{
			time_2 = Random.Range(10, 30);
			PhotonNetwork.Instantiate("Ceiling", new Vector3(x, 1, z), ceilingPrefab.transform.rotation);

			findWallSpawnLoc = false;
		}
		else
		{
			PhotonNetwork.Destroy(Wall);
		}

		//}
	}
	//photonView.RPC("PlantSpreadBomb", RpcTarget.AllViaServer, GetComponent<Rigidbody>().position);




	// Update is called once per frame
	void Update()
	{
		if (!PhotonNetwork.IsMasterClient)
			return;

		time -= Time.deltaTime;
		time_2 -= Time.deltaTime;

		if (time <= 0.0f)
		{
			findSpawnLoc = true;
		}



		while (findSpawnLoc)
		{
			//photonView.RPC("SearchingSpawnLoc", RpcTarget.AllViaServer, GetComponent<Transform>().position);
			SearchingSpawnLoc();

		}


        no_of_walls = GameObject.FindGameObjectsWithTag("wall");

        if (time_2 <= 0.0f && no_of_walls.Length < 40)
        {
            findWallSpawnLoc = true;
        }
        else if (time_2 <= 0.0f && no_of_walls.Length >= 40)
        {
            time_2 = /*3.0f;*/ Random.Range(5, 10);
        }

        while (findWallSpawnLoc)

        {
            //photonView.RPC("SearchingWallSpawnLoc", RpcTarget.AllViaServer, GetComponent<Transform>().position);

            SearchingWallSpawnLoc();
        }

    }






}