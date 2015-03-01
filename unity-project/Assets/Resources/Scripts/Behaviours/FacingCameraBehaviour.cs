using UnityEngine;
using System.Collections;

public class FacingCameraBehaviour : MonoBehaviour {
	GameObject m_player;
	public GameObject m_hook;
	public float m_repositionRate = 0.1f;
	public float m_lookAtRate = 0.1f;
	public float m_wallPadding = 0.1f;

	// Use this for initialization
	void Start () {
		m_player = GameObject.Find("Player");
		if(m_player == null)
		{
			throw new UnityException();
		}
	}
	
	// Update is called once per frame
	void Update () {
		tryFollowHook ();
		lookAtPlayer ();
	}

	void lookAtPlayer()
	{
		Vector3 toPlayer = m_player.transform.position - gameObject.transform.position;
		toPlayer.Normalize ();
		gameObject.transform.forward = Vector3.Normalize(Vector3.Lerp (gameObject.transform.forward, toPlayer, m_lookAtRate));
	}
	
	bool tryFollowHook()
	{
		if (m_hook == null)
			return false;
		RaycastHit rayResult;
		Vector3 newPosition = m_hook.transform.position;
		Vector3 hookToPlayer = m_player.transform.position - m_hook.transform.position;
		float distHookPlayer = hookToPlayer.magnitude;
		hookToPlayer.Normalize ();
		//add the wall padding so we can begin pushing off the wall before we touch it.
		//not perfect because we want the padding to push off the wall, not towards the hook
		if (Physics.Raycast (m_player.transform.position, -hookToPlayer, out rayResult, distHookPlayer + m_wallPadding)) {
			//todc: check if the ray hit a wall and not some stray molecule
			//layermask has good potential
			newPosition = rayResult.point;
			
			Vector3 wallToPlayer = m_player.transform.position - rayResult.point;
			wallToPlayer.Normalize ();
			float wallDot = Vector3.Dot (rayResult.normal, wallToPlayer);
			if (wallDot != 0) {
				float scale = 1 / wallDot;
				Vector3 wallPadding = wallToPlayer * scale * m_wallPadding;
				//todo: If player is criminally close to the wall we should move the camera away from the player and the wall.
				newPosition += wallPadding;
			}
		}
		
		transform.position = Vector3.Lerp (transform.position, newPosition, m_repositionRate);
		return true;
	}
}
