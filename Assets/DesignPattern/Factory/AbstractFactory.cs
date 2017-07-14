using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
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

internal class CoffeeFactory : IHotDrinkFactory
{

    public IHotDrink PrepareDrink(int ml)
    {
        Debug.Log(string.Format("Starbuck, pour {0} ml", ml));
        return new Coffee();
    }
}

class Factories
{
    public string name;
    public IHotDrinkFactory factory;

    public Factories(string name, IHotDrinkFactory factory)
    {
        this.name = name;
        this.factory = factory;
    }
}

public class HotDrinkMachine
{
    
    public enum AvailableDrink
    {
        Coffee,Tea
    }

    //private  Dictionary<AvailableDrink,IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        private List<Factories> namedFactories= new List<Factories>();

    public HotDrinkMachine()
    {
        //foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //{
        //    var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType(Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
        //   factories.Add(drink, factory);
        //}


        //foreach (var type in typeof(HotDrinkMachine).Assembly.GetTypes())


        Assembly assembly = typeof(HotDrinkMachine).Assembly;
        Debug.Log(assembly);
         foreach (var type in assembly.GetTypes())
            {
            Debug.Log(type);
            if (typeof(IHotDrinkFactory).IsAssignableFrom(type) && !type.IsInterface)
            {
                string name = type.Name.Replace("Factory", String.Empty);
                
                IHotDrinkFactory factory = (IHotDrinkFactory) Activator.CreateInstance(type); 
                namedFactories.Add(new Factories(name,factory));
               
            }


            }

    }

    public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    {
        //return factories[drink].PrepareDrink(amount);

        return namedFactories.
                            Find(x => x.name == Enum.GetName(typeof(AvailableDrink), (int) drink))
                            .factory.PrepareDrink(amount);



    }

}



public class AbstractFactory : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		HotDrinkMachine machine= new HotDrinkMachine();

        IHotDrink drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 120);
        IHotDrink drink2 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 500);

        drink.Consume();

        drink2.Consume();

        Debug.Log(Enum.GetName(typeof(HotDrinkMachine.AvailableDrink), 0));
        Debug.Log(Enum.GetName(typeof(HotDrinkMachine.AvailableDrink), 1));
    }

}
