using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqTest2 : MonoBehaviour
{

    void Start()
    {
        Show4();
        Show5();
    }

    void Update()
    {

    }


    public void show1()
    {
        List<Weapon> weapons = WeaponDatabase.weapons.Where
        (w => { return w.Price > 3000; }
        ).ToList();

        foreach (Weapon weapon in weapons)
        {
            Debug.Log($"({weapon.Name}) : {weapon.Price}");
        }
    }

    public void show2()
    {
        List<string> weaponNames = WeaponDatabase.weapons.Select(w => w.Name).ToList();

        List<string> weaponNameQuery = (from w in WeaponDatabase.weapons
                                        select w.Name).ToList();
    }

    public void show3()
    {
        List<Weapon> weapons = WeaponDatabase.weapons.Where
        (w => { return w.Price > 1000; }
        ).ToList().OrderBy(w => w.Price).ToList();

        foreach (Weapon weapon in weapons)
        {
            Debug.Log($"({weapon.Name}) : {weapon.Price}");
        }
    }

    public void Show4()
    {
        IEnumerable<IGrouping<Grade, Weapon>> grouped =
        WeaponDatabase.weapons.GroupBy(w => w.Grade);

        foreach (IGrouping<Grade, Weapon> group in grouped)
        {
            Debug.Log(group.Key + "이것이 키값");

            foreach (Weapon weapon in group)
            {
                Debug.Log($"{weapon.Name})");
            }
        }
    }

    public void Show5()
    {
        List<Weapon> top3Weapon = WeaponDatabase.weapons.OrderByDescending(w => w.Price).Skip(3).ToList();

        foreach (Weapon weapon in top3Weapon)
        {
            Debug.Log($"{weapon.Name} 의 가격은 {weapon.Price}");
        }
    }

    public ItemUIManager uIManager;

    [ContextMenu("테스트 버튼 실행기")]
    public void Test()
    {
        uIManager.SetSortFunction(w => w.Where(w => w.Price > 1000).ToList());
        
    }

}
