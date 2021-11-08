using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ShotgunBullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeDestroy;

    private float _time;
    public void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
        _time += Time.deltaTime;
        if(_time >= _timeDestroy)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamge(_damage);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);

    }
}
