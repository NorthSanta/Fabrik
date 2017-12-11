using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVec
{
    public float x, y, z;
    public MyVec normalized;
    public MyVec()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
        
    }
    public MyVec(float _x, float _y, float _z)
    {
        this.x = _x;
        this.y = _y;
        this.z = _z;
       
    }
    public MyVec normalize()
    {
        normalized = this;
        normalized.x = this.x / length();
        normalized.y = this.y / length();
        normalized.z = this.z / length();
        return normalized;
        
    }
    public float length()
    {
        return Mathf.Sqrt((this.x * this.x) + (this.y * this.y) + (this.z * this.z));
    }
    
    public MyVec crossProduct(MyVec v, MyVec u)
    {
        MyVec c = new MyVec();
        c.x = v.y * u.z - v.z * u.y;
        c.y = v.z * u.x - v.x * u.z;
        c.z = v.x * u.y - v.y * u.x;          
                        
        return c;
    }
    public float dotProduct(MyVec v, MyVec u)
    {
        float a = v.x * u.x + v.y * u.y + v.z * u.z;
        return a;
    }

    public static MyVec operator *(float p, MyVec q)
    {
        MyVec m = new MyVec();
        m.x = q.x * p;
        m.y = q.y * p;
        m.z = q.z * p;
        return m;
    }

    public static MyVec operator +(MyVec q, MyVec p)
    {
        MyVec m = new MyVec();
        m.x = q.x + p.x;
        m.y = q.y + p.y;
        m.z = q.z + p.z;
        return m;
    }

    public static MyVec operator -(MyVec q, MyVec p)
    {
        MyVec m = new MyVec();
        m.x = q.x - p.x;
        m.y = q.y - p.y;
        m.z = q.z - p.z;
        return m;
    }
    public float Distance(MyVec a, MyVec b)
    {
        MyVec separation = new MyVec();

        separation.x = a.x - b.x;
        separation.y = a.y - b.y;
        separation.z = a.z - b.z;

        float res = separation.length();
        return res;

    }
}
