using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgresBar : Bar
{
    [SerializeField] private Spawner _spawner;


    private void OnEnable()
    {
        _spawner.EvenyCountChanged += OnValueChange;
        Slider.value = 0f;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        _spawner.EvenyCountChanged -= OnValueChange;
    }
}
