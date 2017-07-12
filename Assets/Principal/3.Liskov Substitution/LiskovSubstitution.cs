using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Principal
{
    public class Rectangle
    {
        //before	
        //	public int width{ get; set;}
        //	public int height{ get; set;}

        public virtual int width { get; set; }
        public virtual int height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            return string.Format("[Rectangle: width={0}, height={1}]", width, height);
        }
    }

    public class Square : Rectangle
    {
        //before
//	public new int Width
//	{
//	  set { base.width = base.height = value; }
//	}
//
//	public new int Height
//	{ 
//		set { base.width = base.height = value; }
//	}

        public override int width
        {
            set { base.width = base.height = value; }
        }

        public override int height
        {
            set { base.width = base.height = value; }
        }
    }


    public class LiskovSubstitution : MonoBehaviour
    {
        static public int Area(Rectangle r)
        {
            return r.width * r.height;
        }


        void Start()
        {
            Rectangle rc = new Rectangle(3, 7);
            Debug.Log(string.Format("{0} has area {1}", rc, Area(rc)));

            //substitute base type for a subtype
            Rectangle s = new Square();
            s.width = 6;


            Debug.Log(string.Format("{0} has area {1}", s, Area(s)));
            //Before output
            //[Rectangle: width=6, height=0] has area 0

            //After output
            //[Rectangle: width=6, height=6] has area 36
        }

    }

}