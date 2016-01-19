using UnityEngine;
using System.Collections;

public class BaseGun : MonoBehaviour {
	public float coolDown = 0.1f;
	public float bulletDmg = 10f;
	public float bulletSpeed = 0.1f;
	public Vector3 direction;
	public GameObject bullet;
	//public float DISTANCE_FROM_PLAYER = 2.5f;

	private float timeToCooldown = 0;
	private Player player;

    //private float offset_x = 0.2f;
    //private float offset_y = -0.8f;
    //private float offset_z = 2.5f;

    private Vector3 offset = new Vector3 (0.4f, -0.8f, 2.5f);
	// Use this for initialization
	void Start () {
		direction = new Vector3 (0, 0, 1);
        //player = transform.parent.gameObject.GetComponent<Player>();

		//transform.position = transform.position + direction + new Vector3(DISTANCE_FROM_PLAYER, DISTANCE_FROM_PLAYER, DISTANCE_FROM_PLAYER);

	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            direction = player.direction;
        }

        ////transform.position = (transform.parent.position + Vector3.Normalize(direction) * DISTANCE_FROM_PLAYER) + offset ;
        //transform.position = transform.parent.position + offset;
        //Fire ();
	}

	public void Fire(){
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot(){
		if(timeToCooldown <= 0){
			if(bullet!= null){
				GameObject go = (GameObject) Instantiate(bullet, transform.position, transform.rotation);
				//BaseBulletCC projectile = go.GetComponent<BaseBulletCC>();
				BaseBulletRB projectile = go.GetComponent<BaseBulletRB>();
                if(player == null)
                {
                    player = transform.parent.transform.parent.gameObject.GetComponent<Player>();
                }
                
                projectile.direction = player.direction;
				projectile.damage = bulletDmg;
				//				Rigidbody insBullet = go.GetComponent<Rigidbody>();
				//				insBullet.AddForce(direction * bulletSpeed, ForceMode.Impulse);
			}
			timeToCooldown += coolDown;
			yield return new WaitForSeconds (coolDown);
			timeToCooldown = 0;
			Debug.Log("Cooled Down");
		}



	}
}
