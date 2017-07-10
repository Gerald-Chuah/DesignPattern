using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq.Expressions;


// to get variable name
public static class MemberInfoGetting
{
	public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
	{
		MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
		return expressionBody.Member.Name;
	}
}

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

	}
	

}
