using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//参考：https://baba-s.hatenablog.com/entry/2014/12/29/154306

public static class GameObjectExtensions
{
    public static T[] FindObjectsOfInterface<T>() where T : class
    {
        List<T> result = new List<T>();

        foreach ( Component component in GameObject.FindObjectsOfType<Component>())
        {
            T componentTriedCast = component as T;

            if (componentTriedCast != null)
            {
                result.Add(componentTriedCast);
            }
        }
        return result.ToArray();
    }
}
