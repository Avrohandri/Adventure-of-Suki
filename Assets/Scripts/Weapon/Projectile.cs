using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    public Vector3 Direction { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }

    private void Start()
    {
        Speed = speed;
    }

    private void Update()
    {
        transform.Translate(Direction * (Speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ITakeDamage>() != null)
        {
            other.GetComponent<ITakeDamage>().TakeDamage(Damage);
        }
        
        Debug.Log(Damage);
        Destroy(gameObject);
    }
}
