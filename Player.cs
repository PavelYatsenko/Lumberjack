using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _buttonChangeWeapon;

    public int Money { get; private set; }

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;
    private Image _currentIconWeapon;

    public event UnityAction<int,int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Awake()
    {
        _currentIconWeapon = _buttonChangeWeapon.GetComponent<Image>();
    }
    private void Start()
    {
      

        ChangeWeapon(_weapons[_currentWeaponNumber]);   
        _currentWeapon = _weapons[0];
       
        _currentHealth = _health;
        ChangeWeaponIcon(_weapons[_currentWeaponNumber]);
        _animator = GetComponent<Animator>();
    }

    internal void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);
        if (_currentHealth <= 0)
        {
            _animator.Play("Death");
            Destroy(gameObject);
            
        }
            
    }
    private void ChangeWeaponIcon(Weapon weapon)
    {
        _currentIconWeapon.sprite = weapon.Icon;
        _currentIconWeapon.SetNativeSize();
    }

    public  void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }
    public void NextWeapon()
    {
        print("Yes");
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    } 

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        ChangeWeaponIcon(_currentWeapon);
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }
    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
        
    }
    public void OnEnamyDied(int reward)
    {
        Money += reward;
    }
}
