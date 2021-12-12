using System.Collections;
using UnityEngine;

public class BombScript : MonoBehaviour
{

    public GameObject explosionPrefab;
    public LayerMask levelMask;
    private bool exploded = false;
    private int _explength;
    [SerializeField] private PlayerController _player;

    private isHitBlock block;

    void Start()
    {
        Invoke("Explode", 3f);
        _player.AddBomb(1);
    }

    void Explode()
    {
        exploded = true;
        GameObject Ex1 = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        CreateExplosions(Vector2.up);
        CreateExplosions(Vector2.down);
        CreateExplosions(Vector2.left);
        CreateExplosions(Vector2.right);


        Destroy(gameObject, 0.3f);
        Destroy(Ex1, 3f);

    }

    public void CreateExplosions(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,_player.GetPower(), levelMask);
        if (gameObject.CompareTag("Bomb"))
        {
            int toBlow = 0;
            if (!Physics2D.Raycast(transform.position, direction, _player.GetPower(), levelMask))
            {
                toBlow = _player.GetPower();

            }
            else if (hit.collider.tag == "Destroyable" || hit.collider.tag == "Undestroyable")
            {

                toBlow = Mathf.RoundToInt(hit.distance)-1;

                if (hit.collider.tag == "Destroyable")
                {
                    block = hit.collider.gameObject.GetComponent<isHitBlock>();
                    if (block.isAlive)
                    {
                        block.isAlive = false;
                    }
                    Destroy(hit.collider.gameObject);
                }

            }
            
            for (int i = 0; i < toBlow; i++)
            {
                GameObject Exes = Instantiate(explosionPrefab, transform.position + ((1 + i)
                    * new Vector3(direction.x, direction.y, 0)),
                    explosionPrefab.transform.rotation);
                Destroy(Exes, 0.5f);

            }
        }

    }

    public void OnTriggerEnter(Collider other) //potentional chain 
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }
    
}
