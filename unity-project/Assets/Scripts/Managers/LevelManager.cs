using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LevelManager : MonoBehaviour {
	// Vars set from unity editor
	public Vector3 checkpoint;

	private Dictionary<string, List<GameObject>> collected;
	private Dictionary<string, List<GameObject>> collectables;
	private bool winState; //true is win false is lose

	private StateObj state;

	private int score;

	private ShipBehaviour ship;
	private EventPublisher ep;
	public GUIManager GUIMan;
	private LevelLoader loader;

	//Call before Start()
	void OnEnable () 
	{
		// Setup level vars
		collectables = TagLookupTable (GetCollectables ());
		collected = new Dictionary<string, List<GameObject>>();
		winState = false;
		score = 0;
		state = new StateObj ();
	}

	// Use this for initialization
	void Start () 
	{
		var util = GameObject.Find ("Utilities");
		if (util == null) {
			util = GameObject.CreatePrimitive (PrimitiveType.Cube);
			util.AddComponent<LevelLoader>();
			util.name = "Utilities";
		}
		loader = util.GetComponent<LevelLoader> ();
		GUIMan.UpdateCollectedMolecules (Flatten (collected));
		ship = GameObject.Find("Player").GetComponent<ShipBehaviour>();
		SetCheckpoint(ship.transform.position);
		ep = GameObject.Find("Level").GetComponent<EventPublisher>();

	}

	// Update is called once per frame
	void Update () 
	{
		setupHierarchy ();
	}

	/**
	 * Event Listener methods 
	 *
	 */

	public void setupHierarchy() {
		GameObject[] levelObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach(GameObject orphan in levelObjects) {
			if(orphan.transform.parent == null && !orphan.gameObject.Equals(gameObject) && (orphan.name != "Utilities")) {
				orphan.transform.parent = gameObject.transform;
			}
		}
	}

	public void OnDeath() 
	{
		GUIMan.UpdateHealthBar (0, ship.MaxHealth);
		Invoke ("RespawnPlayer", 3.0f);
	}
	
	public void OnCollect(CollectableEvent colEvent) {
		GameObject collectable = colEvent.collectable;
		CollectCollectable (collectable);
		ArrayList flatCollected = Flatten (collected);
		if (flatCollected.Count == Flatten(collectables).Count) {
			ChangeWinState();
			EndLevel ();
		}
		GUIMan.UpdateCollectedMolecules (flatCollected);
	}
	public void OnDecollect(CollectableEvent colEvent){
		GameObject collectable = colEvent.collectable;
		DecollectCollectable (collectable);
		GUIMan.UpdateCollectedMolecules (Flatten(collected));
	}
	
	public void OnDamage(DamageEvent damage) 
	{
		GUIMan.UpdateHealthBar (damage.postHealth, damage.maxHealth);
	}

	/**
	 * Collectable management methods
	 * 
	 */
	private ArrayList GetCollectables()
	{
		CollectableBehaviour[] collectableContainers = GameObject.FindObjectsOfType<CollectableBehaviour> ();
		ArrayList tmpList = new ArrayList ();
		foreach(CollectableBehaviour bhv in collectableContainers) {
				tmpList.Add(bhv.gameObject);
		}
		return tmpList;
	}

	private Dictionary<string, List<GameObject>> TagLookupTable(ArrayList list) 
	{
		Dictionary<string, List<GameObject>> tmpDic = new Dictionary<string, List<GameObject>>();
		foreach (GameObject tmpObj in list) 
		{
			if(!tmpDic.ContainsKey(tmpObj.tag)){
				tmpDic.Add (tmpObj.tag, new List<GameObject>());
			}
			tmpDic[tmpObj.tag].Add (tmpObj);
		}
		return tmpDic;
	}

	private ArrayList Flatten(Dictionary<string, List<GameObject>> table) 
	{
		ArrayList tmp = new ArrayList();
		foreach(KeyValuePair<string, List<GameObject>> entry in table) {
			tmp.AddRange(entry.Value);
		}
		return tmp;
	}
	
	/**
	 * Game Logic Methods
	 */
	public void RespawnPlayer()
	{
		//score, collected, dead objects
		//player health, energy, position, velocity, angular vel.
		this.score = this.state.getScore();
		ArrayList collectedList = this.state.getCollected();
		List<GameObject> saveCollected = collectedList.Cast<GameObject>().ToList();
		List<GameObject> curCollected = Flatten(this.collected).Cast<GameObject>().ToList();
		IEnumerable<GameObject> enables = curCollected.Except (saveCollected);
		ship.Health = this.state.getHealth();
		ship.BeamEnergy = this.state.getBeamenergy();
		foreach(GameObject obj in enables){
			obj.SetActive(true);
		}
		this.collected = TagLookupTable(collectedList);
		ship.gameObject.transform.position = this.checkpoint;
		ship.gameObject.rigidbody.velocity = Vector3.zero;
		ship.gameObject.rigidbody.angularVelocity = Vector3.zero;
		ship.Respawn ();
		GUIMan.UpdateHealthBar (ship.Health, ship.MaxHealth);
		GUIMan.UpdateCollectedMolecules (Flatten(collected));
	}

	public void SetCheckpoint(Vector3 checkpoint)
	{
		this.state.SaveState(score,Flatten(collected),ship.BeamEnergy, ship.Health);
		this.checkpoint = checkpoint;
	}

	public void EndLevel()
	{
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.SetString ("Level" ,Application.loadedLevelName);
		if(winState)
		{
			PlayerPrefs.SetInt ("Win", 1);
			loader.LoadLevel("game_over");
		}
		else
		{
			PlayerPrefs.SetInt ("Win", 0);
			loader.LoadLevel("game_over");
		}
		
	}

	public void ChangeWinState()
	{
		if(winState)
		{
			winState = false;
		}
		else
		{
			winState = true;
		}
	}

	public void CollectCollectable(GameObject collectable){
		if(!this.collected.ContainsKey(collectable.tag)) {
			this.collected.Add(collectable.tag, new List<GameObject>());
		}
		this.collected[collectable.tag].Add (collectable);
	}

	public void DecollectCollectable(GameObject collectable)
	{
		//check if this type of collectable has been collected
		if(this.collected.ContainsKey(collectable.tag)) {
			//remove this specific collectable from its group of collected brethren
			this.collected[collectable.tag].Remove(collectable);
		}
	}

	public ArrayList GetCollectedByTag(string tag) 
	{
		if(collected.ContainsKey(tag)){
			return new ArrayList(collected [tag]);
		} else {
			return new ArrayList();
		}
	}

	/**
	 * public properties of the level manager
	 */
	public int Score
	{
		get{ return score;}
	}
	
	public ArrayList Collected
	{
		get{
			return Flatten (this.collected);
		}
	}

	public ArrayList Collectables
	{
		get{
			return Flatten (this.collectables);
		}
	}
}
