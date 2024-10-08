
using System;
using System.Collections.Generic;

public static class Utils {

    public static void Shuffle<T>(this IList<T> list) {
        Random rng = new Random(); 
        int n = list.Count;
        while(n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static object GetInstance(Type type) {
        return Activator.CreateInstance(type);
    }
    public static object GetInstance(string typeName) {
        return Activator.CreateInstance(Type.GetType(typeName));
    }
}