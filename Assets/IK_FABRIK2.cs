using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK2 : MonoBehaviour
{


    struct myTransform
    {
        public MyVec pos;
        public MyQuat rotation;
    }


    public Transform[] joints;
    public Transform target;

    private MyVec[] copy;
    private float[] distances;
    private bool done;
    private float treshold_condition = 0.1f;

    private MyVec vecTarget;

    void Start()
    {
        distances = new float[joints.Length - 1];
        copy = new MyVec[joints.Length];
        //vecTarget = new MyVec(target.position.x, target.position.y, target.position.z);
    }

    void Update()
    {
        vecTarget = new MyVec(target.position.x, target.position.y, target.position.z);
        copy[0] = new MyVec (joints[0].transform.position.x, joints[0].transform.position.y, joints[0].transform.position.z);
        // Copy the joints positions to work with
        //TODO
        for (int i = 0; i < joints.Length - 1; i++)
        {
            copy[i + 1] = new MyVec(joints[i + 1].transform.position.x, joints[i + 1].transform.position.y, joints[i + 1].transform.position.z);
            distances[i] = (joints[i + 1].position - joints[i].position).magnitude;

        }
        
        done = (copy[copy.Length - 1] - vecTarget).length() < treshold_condition;
       

        if (!done)
        {
            float targetRootDist = new MyVec().Distance(copy[0], vecTarget);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for(int i = 0; i < joints.Length - 1; i++)
                {
                    float r = new MyVec().Distance(vecTarget, copy[i]);
                    float lambda = distances[i] / r;
                    copy[i + 1] = (1 - lambda) *copy[i] + (lambda *vecTarget);
                }
            }
            else
            {
                // The target is reachable
                MyVec b = new MyVec(copy[0].x, copy[0].y, copy[0].z);
                float r = new MyVec().Distance(copy[copy.Length-1], vecTarget);
                while (r > treshold_condition)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO
                    copy[copy.Length - 1] = vecTarget;
                    for(int i = copy.Length - 2; i > 0 ; i--)
                    {
                        float l = new MyVec().Distance(copy[i + 1], copy[i]);
                        float lambda = distances[i] / l;
                        copy[i] = (1 - lambda)*copy[i + 1] + (lambda*copy[i]);
                    }

                    // STAGE 2: BACKWARD REACHING
                    //TODO
                    copy[0] = b;
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        float l = new MyVec().Distance(copy[i + 1], copy[i]);
                        float lambda = distances[i] / l;
                        copy[i + 1] = (1 - lambda)*copy[i] + (lambda*copy[i + 1]);
                    }
                    
                    r = new MyVec().Distance(copy[copy.Length - 1], vecTarget);
                }
            }

            

            // Update original joint rotations
            for (int i = 0; i < joints.Length - 1; i++)
            {
                //TODO 
                MyVec temp1 = new MyVec(joints[i + 1].position.x, joints[i + 1].position.y, joints[i + 1].position.z);
                MyVec temp2 = new MyVec(joints[i].position.x, joints[i].position.y, joints[i].position.z);
                MyVec init = temp1 - temp2;
                init.normalize();
                MyVec now = copy[i + 1] - copy[i];
                now.normalize();
                float cos = (new MyVec().dotProduct(init.normalized, now.normalized));
                float sin = new MyVec().crossProduct(init.normalized, now.normalized).length();
                print("cos" + cos);
                print("sin" + sin);
                float angle = Mathf.Atan2(sin, cos);//* Mathf.Rad2Deg;
                
                    MyVec axis = new MyVec().crossProduct(init.normalized, now.normalized).normalize();
                    MyQuat tempRot = new MyQuat(joints[i].rotation.x, joints[i].rotation.y, joints[i].rotation.z, joints[i].rotation.w);

                /*print(axis.x);
                print(axis.y);
                print(axis.z);*/

                if (angle != 0)
                {
                    MyQuat finalRot = new MyQuat().fromAxisAngle(angle, axis.normalized) * tempRot;
                    /*print(finalRot.x);
                    print(finalRot.y);
                    print(finalRot.z);
                    print(finalRot.w);*/
                    joints[i].rotation = new Quaternion(finalRot.x, finalRot.y, finalRot.z, finalRot.w);
                }
                Vector3 finalPos = new Vector3(copy[i].x, copy[i].y, copy[i].z);
                joints[i].position = finalPos;

                
            }          
        }
    }

}
