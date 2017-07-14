using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{

    public void Consume()
    {
        Debug.Log("Nice tea OwO");
    }
}

internal class Coffee : IHotDrink
{

    public void Consume()
    {
        Debug.Log("Kopi O");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink PrepareDrink(int ml);
}

internal class TeaFactory : IHotDrinkFactory
{

    public IHotDrink PrepareDrink(int ml)
    {
        Debug.Log(string.Format("TeaPot, pour {0} ml",ml));
        return new Tea();
    }
}

internal class CofeeFactory : IHotDrinkFactory
{

    public IHotDrink PrepareDrink(int ml)
    {
        Debug.Log(string.Format("Starbuck, pour {0} ml", ml));
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    
    public enum AvailableDrink
    {
        Coffee,Tea
    }

    private  Dictionary<AvailableDrink,IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

    public HotDrinkMachine()
    {
        foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        {
            var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType(Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
           factories.Add(drink, factory);
        }
    }

    public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    {
         return factories[drink].PrepareDrink(amount);
    }

}



public class AbstractFactory : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		HotDrinkMachine machine= new HotDrinkMachine();

        IHotDrink drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 120);

         drink.Consume();
    }

}
