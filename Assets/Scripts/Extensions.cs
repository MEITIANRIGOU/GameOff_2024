using UnityEngine;

public static class Extensions
{
    public static T RandomChoice<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
        {
            throw new System.Exception("Array is null or empty.");
        }

        int randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }
}