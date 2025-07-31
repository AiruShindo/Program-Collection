using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatObject : MonoBehaviour
{
    private int Decrease = 1;
    [SerializeField] private float DelTime;
    public GameObject Explosion;
    void Start()
    {
       
    }
    void Update()
    {
        Destroy(this.gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            GameObject.Find("GameSystem").GetComponent<GameSystem>().Target -= Decrease;
            Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            DelTime += Time.deltaTime;
            if(DelTime >= 3.0f)
            {
                Destroy(Explosion);
            }

        }
    }
}
