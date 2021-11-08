using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _wepons;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;
    private void Start()
    {
        for(int i = 0; i < _wepons.Count; i++)
        {
            AddItem(_wepons[i]);
        }
    }

    
    private void AddItem(Weapon weapon)
    {
        
        var view = Instantiate(_template, _itemContainer.transform);

        view.SellButtonCkick += OnSellButtonCick;
        view.Render(weapon);
    }

    private void OnSellButtonCick(Weapon weapon, WeaponView view)
    {
        TrySellWeapon(weapon, view);
    }
    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if(weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);
            weapon.Buy();
            view.SellButtonCkick -= OnSellButtonCick;
        }
    }


}
