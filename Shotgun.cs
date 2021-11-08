using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] protected ShotgunBullet ShotgunBullet;
    [SerializeField] private float _scatter;
    public override void Shoot(Transform shootPoint)
    {
        
        for (int a = 0; a < 2; a++)
        {
            Instantiate(ShotgunBullet, new Vector2(shootPoint.position.x, shootPoint.position.y + _scatter), Quaternion.identity);
            _scatter -= 0.25f;

        }
        _scatter = 0.25f;

    }


}
