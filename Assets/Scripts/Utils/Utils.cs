using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static IEnumerator MoveObjToPoint(Transform obj, Vector3 endPoint) {
        Vector3 startPos = obj.transform.position;
        
        float t = 0f;
        while (t < 1) {
            t += Constants.ObjSnapSpeed * Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPos, endPoint, t);
            yield return null;
        }
    }
}
