using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rectangle
{
	public virtual int width{ get; set;}
	public virtual int height{ get; set;}

	public Rectangle ()
	{
		
	}

	public Rectangle (int width, int height)
	{
		this.width = width;
		this.height = height;
	}

	public override string ToString()
	{
		return string.Format("[Rectangle: width={0}, height={1}]", width, height);
	}
}

public class Square: Rectangle
{
	public override int width
	{
		set{base.width=base.height=value;}
	}

	public override int height
	{
		set{base.width=base.height=value;}
	}
}


public class LiskovSubstitution : MonoBehaviour
{
	static public int Area(Rectangle r)
	{
		return r.width * r.height;
	}
		

	void Start () 
	{
		Rectangle rc = new Rectangle (3,7);
		Rectangle s = new Square ();
		s.width = 6;
		Debug.Log(string.Format("{0} has area {1}",rc,Area(rc)));
		Debug.Log(string.Format("{0} has area {1}",s,Area(s)));

	}

}
