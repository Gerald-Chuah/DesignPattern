using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    private float x, y;

    private Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return string.Format("X: {0}, Y: {1}", x, y);
    }


    //Create new Point object one time (better)
    public static Point OriginPoint = new Point(0, 0);


    //Create new Point everytime when request
    public static Point OriginPointFactory
    {
        get
        {
            return new Point(0, 0);
        } 
    }

public static class Factory
    {

        public static Point CartesianPoint(float x, float y)
        {
            return new Point(x,y);
        }

        public static Point PolarPoint(float rho, float theta)
        {
            return new Point(rho * Mathf.Cos(theta), rho * Mathf.Sin(theta));
        }
    }
}


public class Factory : MonoBehaviour
{

	
	void Start ()
    {
		Point cartesianPoint = Point.Factory.CartesianPoint(2,2);
		Point polarPoint = Point.Factory.PolarPoint(2,2);

        Point origin = Point.OriginPoint;
        Point originFactory = Point.OriginPointFactory;

        Debug.Log(string.Format("cartesianPoint: {0}", cartesianPoint));
        Debug.Log(string.Format("polarPoint: {0}", polarPoint));
        Debug.Log(string.Format("origin: {0}", origin));
        Debug.Log(string.Format("originFactory: {0}", originFactory));
    }
	
	
}
