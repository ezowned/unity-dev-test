using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrajectoryLoader : MonoBehaviour {

    class BallPath
    {
        public float[] x;
        public float[] y;
        public float[] z;
    }


    public static Vector3[] Load(string path)
    {
        var ballPath = JsonUtility.FromJson<BallPath>(Resources.Load<TextAsset>(path).text);
        var x = ballPath.x;
        var y = ballPath.y;
        var z = ballPath.z;
        Vector3[] res = new Vector3[x.Length];
        for (int i = 0; i < ballPath.x.Length; i++)
        {
            res[i] = new Vector3(x[i], y[i], z[i]);
        }

        return res;
    }

}
