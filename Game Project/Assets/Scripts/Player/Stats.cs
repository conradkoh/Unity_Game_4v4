using UnityEngine;
using System.Collections;

public class Stats: MonoBehaviour {

    public delegate void PlayerDiedDelegate(string killerName);
    public PlayerDiedDelegate PlayerDied;
    private const string MESSAGE_WAITING_TO_RESPAWN = "Waiting to respawn";
    private float _health = 1000;
    public string health;

    Player player;
	// Use this for initialization
	void Start () {
        health = _health.ToString();
        player = gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

	public void TakeDamage(float damage){
		Debug.Log(damage);
		_health -= damage;
		if(_health <= 0){
			_health = 0;
            health = MESSAGE_WAITING_TO_RESPAWN;
            PlayerDied("unknown killer (needs to be set)");
			//Destroy (gameObject);
		}
        else
        {
            health = _health.ToString();
        }
	}

    public bool IsAlive()
    {
        if(_health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
