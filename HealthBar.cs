using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Player _player;


    private void OnEnable()
    {
        _player.HealthChanged += OnValueChange;
        Slider.value = 1f;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChange;
    }
}
