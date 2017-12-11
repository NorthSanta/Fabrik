using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct axisAngle
{
    public float x, y, z, w;
    
}
public class MyQuat
{
    public float x, y, z, w;
    public MyQuat()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
        this.w = 0;
    }
    public MyQuat(float _x, float _y, float _z, float _w)
    {
        this.x = _x;
        this.y = _y;
        this.z = _z;
        this.w = _w;
    }
    public void normalize()
    {
        this.x = this.x / norm();
        this.y = this.y / norm();
        this.z = this.z / norm();
        this.w = this.w / norm();
    }
    public float norm()
    {
        return Mathf.Sqrt((this.x * this.x) + (this.y * this.y) + (this.z * this.z) + (this.w *this.w));
    }
    public MyQuat inverse()
    {
        MyQuat p = new MyQuat();
        this.normalize();
        p.x = -this.x;
        p.y = -this.y;
        p.z = -this.z;
        p.w = this.w;
            
        return p;
    }
    public static MyQuat operator *(MyQuat q, MyQuat p)
    {
        MyQuat m = new MyQuat();
        //q.normalize();
        // p.normalize();
        m.w = (p.w * q.w - p.x * q.x - p.y * q.y - p.z * q.z);
        m.x = (p.w * q.x + p.x * q.w - p.y * q.z + p.z * q.y);
        m.y = (p.w * q.y + p.x * q.z + p.y * q.w - p.z * q.x);
        m.z = (p.w * q.z - p.x * q.y + p.y * q.x + p.z * q.w);
        m.normalize();
        return m;
    }
    public MyQuat fromAxisAngle(float angle, MyVec axis)
    {
        //angle *= Mathf.Rad2Deg;
        Debug.Log("angle :"+ angle);
        MyQuat q = new MyQuat();
        q.w = Mathf.Cos(angle / 2);
        q.x = axis.x * Mathf.Sin(angle / 2);
        q.y = axis.y * Mathf.Sin(angle / 2);
        q.z = axis.z * Mathf.Sin(angle / 2);
        q.normalize();
        
       
        Debug.Log("q.x" + q.x);
        Debug.Log("q.y" + q.y);
        Debug.Log("q.z" + q.z);
        Debug.Log("q.w" + q.w);
        return q;
    }

    public axisAngle toAxisAngle()
    {
        axisAngle axis = new axisAngle();
        this.normalize();
        axis.w = 2 * Mathf.Acos(this.w);
        float s = Mathf.Sqrt(1 - (this.w * this.w));
        if (s < 0.01)
        {
            axis.x = this.x;
            axis.y = this.y;
            axis.z = this.z;
        }
        else
        {
            axis.x = this.x / s;
            axis.y = this.y / s;
            axis.z = this.z / s;
        }

        return axis;
    }

}

