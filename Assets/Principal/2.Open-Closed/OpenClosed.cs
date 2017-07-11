using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq.Expressions;
using ExtensionTool;


public enum Color
{
	Red,Green,Blue
}

public enum Size
{
	Small,Medium,Big
}

public class Product
{
	public string m_name;
	public Color m_color;
	public Size m_size;

	public Product (string name,Color color,Size size)
	{
		if (name == null)
		{
			throw new ArgumentNullException (paramName: MemberInfoGetting.GetMemberName(()=>name));
			//throw new ArgumentNullException (paramName: nameof(name); =>After C# 6.0

		}
			
		m_name = name;
		m_color = color;
		m_size = size;
	}
}
	


public class ProductFilter
{
	public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
	{
		foreach (var p in products)
		{
			if (p.m_size == size)
				yield return p;
		}
	}
	public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
	{
		foreach (var p in products)
		{
			if (p.m_color == color)
				yield return p;
		}
	}

	public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size,Color color)
	{
		foreach (var p in products)
		{
			if (p.m_size == size && p.m_color == color)
				yield return p;
		}
	}

}

public interface ISpecification<T>
{
	bool IsSatisfied(T t);
}

public interface IFilter<T>
{
	IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

public class ColorSpecification : ISpecification<Product>
{
	private Color color;

	public ColorSpecification (Color color)
	{
		this.color = color;
	}

	public bool IsSatisfied(Product p)
	{
		return p.m_color == color;
	}
}

public class SizeSpecification : ISpecification<Product>
{
	private Size size;

	public SizeSpecification (Size size)
	{
		this.size = size;
	}

	public bool IsSatisfied(Product p)
	{
		return p.m_size == size;
	}
}

//Combinator
public class AndSpecification<T>: ISpecification<T>
{
	private ISpecification<T> first, second;

	public AndSpecification (ISpecification<T> first,ISpecification<T> second)
	{	
		if(first == null)
		{
			throw new ArgumentNullException(paramName: MemberInfoGetting.GetMemberName(()=>first));
		}

		if(second == null)
		{
			throw new ArgumentNullException(paramName: MemberInfoGetting.GetMemberName(()=>second));
		}

		this.first = first;
		this.second =second;

		//C# 7.0 can write in this way
		//this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
	}

	public bool IsSatisfied(T t)
	{
		return first.IsSatisfied(t) && second.IsSatisfied(t);
	}
}

public class BetterFilter: IFilter<Product>
{
	public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
	{
		foreach (var i in items)
		{
			if (spec.IsSatisfied(i))
			{
				yield return i;
			}
		}
	}
}


public class OpenClosed : MonoBehaviour 
{

	void Start () 
	{

		var apple = new Product ("Apple",Color.Red,Size.Small);
		var tomato = new Product ("Tomato",Color.Red,Size.Medium);
		var grass = new Product ("Grass",Color.Green,Size.Medium);

		Product[] products = {apple,tomato,grass};

		var pf = new ProductFilter ();
		Debug.Log("Old Method");

		foreach (var p in pf.FilterByColor(products,Color.Red))
		{
			string text = string.Format("{0} is red color.",p.m_name);
			Debug.Log(text);
		}

		Debug.Log("New Method");
		var bf = new BetterFilter();

		foreach (var p in bf.Filter(products,new ColorSpecification(Color.Red)))
		{
			string text = string.Format("{0} is red color.",p.m_name);
			Debug.Log(text);
		}

		foreach (var p in bf.Filter(products,
				new AndSpecification<Product>( 
				new ColorSpecification(Color.Red),
				new SizeSpecification(Size.Small))))
		{
			string text = string.Format("{0} is red color and small.",p.m_name);
			Debug.Log(text);
		}

	}
	

}
