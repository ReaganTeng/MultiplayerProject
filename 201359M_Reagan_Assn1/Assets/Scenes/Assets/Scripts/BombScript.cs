using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;


public class BombScript : MonoBehaviour
{

	public bool isMine = false;
	float time;
	private Transform wall;
	private PhotonView photonView;
	public GameObject explosionPrefab;


	public LayerMask levelMask;
	Vector3 direction;
	private bool exploded = false;
	private bool mWallinRange;

	

	int bombType;

	//private bool defaultBomb, strongBomb, spreadBomb;

	// Start is called before the first frame update

	void Awake()
	{
		photonView = GetComponent<PhotonView>();
		time = 5.0f;

	}

	void Start()
	{
		//enemies = GameObject.Find("Enemies");

		//Invoke("Explode", 3f);
	}


	

	[PunRPC]
	public void CallExplode()
	{
        if (PhotonNetwork.IsMasterClient)
        {
			switch(this.gameObject.tag) 
			{
				case "Bomb":
				{
						//determine direction of explosion
						for (int x = 0; x < 4; x++)
					{
						if (x == 0)
						{
							direction = new Vector3(0, 0, 1.0f);
						}
						else if (x == 1)
						{
							direction = new Vector3(0, 0, -1.0f);
						}
						else if (x == 2)
						{
							direction = new Vector3(1.0f, 0, 0);
						}
						else if (x == 3)
						{
							direction = new Vector3(-1.0f, 0, 0);
						}

						//determine the length of the explosion
						for (int i = 1; i < 3; i++)
						{
							RaycastHit hit;
							Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
								i, levelMask);

								if (!hit.collider)
								{
									GameObject explosion = PhotonNetwork.Instantiate("Explosion", transform.position + (i * direction), explosionPrefab.transform.rotation);
									//PhotonNetwork.Destroy(explosion);
								}
								else
								{
									break;
								}
						}
					}
						break;
				}
				case "StrongBomb":
					{
						GameObject[] wallPrefab = GameObject.FindGameObjectsWithTag("wall");

						//determine direction of explosion
						for (int x = 0; x < 8; x++)
						{
							if (x == 0)
							{
								direction = new Vector3(0, 0, 1.0f);
							}
							else if (x == 1)
							{
								direction = new Vector3(0, 0, -1.0f);
							}
							else if (x == 2)
							{
								direction = new Vector3(1.0f, 0, 0);
							}
							else if (x == 3)
							{
								direction = new Vector3(-1.0f, 0, 0);
							}
							else if (x == 4)
							{
								direction = new Vector3(-1.0f, 0, -1.0f);
							}
							else if (x == 5)
							{
								direction = new Vector3(1.0f, 0, 1.0f);
							}
							else if (x == 6)
							{
								direction = new Vector3(-1.0f, 0, 1.0f);
							}
							else if (x == 7)
							{
								direction = new Vector3(1.0f, 0, -1.0f);
							}

							
							GameObject explosion_firstlayer = PhotonNetwork.Instantiate("Explosion", transform.position + (direction), explosionPrefab.transform.rotation);
							GameObject explosion_secondlayer = PhotonNetwork.Instantiate("Explosion", new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + (direction)
								, explosionPrefab.transform.rotation);
							explosion_firstlayer.GetComponent<ExplosionScript>().setfromBomb2();
							explosion_secondlayer.GetComponent<ExplosionScript>().setfromBomb2();


						}
						break;
					}
				case "SpreadBomb":
					{

						//determine direction of explosion
						for (int x = 0; x < 8; x++)
						{
							if (x == 0)
							{
								direction = new Vector3(0, 0, 1.0f);
							}
							else if (x == 1)
							{
								direction = new Vector3(0, 0, -1.0f);
							}
							else if (x == 2)
							{
								direction = new Vector3(1.0f, 0, 0);
							}
							else if (x == 3)
							{
								direction = new Vector3(-1.0f, 0, 0);
							}
							else if (x == 4)
							{
								direction = new Vector3(-1.0f, 0, -1.0f);
							}
							else if (x == 5)
							{
								direction = new Vector3(1.0f, 0, 1.0f);
							}
							else if (x == 6)
							{
								direction = new Vector3(-1.0f, 0, 1.0f);
							}
							else if (x == 7)
							{
								direction = new Vector3(1.0f, 0, -1.0f);
							}

							//determine the length of the explosion
							for (int i = 1; i < 3; i++)
							{
								RaycastHit hit;
								Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
									i, levelMask);

								if (!hit.collider)
								{
									GameObject explosion = PhotonNetwork.Instantiate("Explosion", transform.position + (i * direction), explosionPrefab.transform.rotation);
								}
								else
								{
									break;
								}
							}
						}
						break;
					}
				default:
					return;
			}

			PhotonNetwork.Destroy(this.gameObject);
		}
	}


	// Update is called once per frame
	void Update()
	{
		if (!PhotonNetwork.IsMasterClient)
			return;

		time -= Time.deltaTime;
		if (time <= 0.0f)
		{

			photonView.RPC("CallExplode", RpcTarget.AllViaServer);
			//destroy the bomb itself

		}
	}

	public float getTime()
    {
		return time;
    }
	
	


	// Checks the the bomb hasn't exploded.
	// Checks if the trigger collider has the Explosion tag assigned.
	// Cancel the already called Explode invocation by dropping the bomb -- if you don't do this the bomb might explode twice.
	// Explode!
	//}
}