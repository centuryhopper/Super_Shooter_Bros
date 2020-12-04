using System;
using UnityEngine;

// source: https://www.bitshiftprogrammer.com/2018/05/unity-c-extension-methods.html

namespace Extensions
{
    /// <summary>
    /// A bunch of utilities methods
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        ///  Checks if GameObject has a certain component
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasComponent<T>(this Component component) where T : Component
        {
            return component.GetComponent<T>() != null;
        }

        /// <summary>
        /// Resets the Transform
        /// </summary>
        /// <param name="trans"></param>
        public static void ResetTransformation(this Transform trans)
        {
            trans.position = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = new Vector3(1, 1, 1);
        }

        /// <summary>
        /// Write to position coordinates
        /// </summary>
        /// <param name="original"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>The new position at the specified x, y, and z coordinates</returns>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        /// <summary>
        /// pass in the transform's position at that coordinate
        /// if you don't want a certain coordinate to change.
        /// For example,
        /// if you just want to flatten the x coordinate
        /// of your transform position, you would write:
        /// transform.position.Flat(y: transform.position.y, z: transform.position.z)
        /// </summary>
        /// <param name="original"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 Flat(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? 0, y ?? 0, z ?? 0);
        }

        /// <summary>
        /// Calculates the direction of a vector from
        /// the source to the destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns>The direction vector</returns>
        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Normalize(destination - source);
        }

        /// <summary>
        /// given an int array, return a subarray of values
        /// starting from the given array's "startingIndex" to,
        /// but not including, the "endingIndex"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startingIndex"></param>
        /// <param name="endingIndex"></param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] data, int startingIndex, int? endingIndex = null)
        {
            if (endingIndex == null) { endingIndex = data.Length; }
            if (startingIndex < 0 || endingIndex <= 0) { return Array.Empty<T>(); }
            if (startingIndex >= endingIndex) { return Array.Empty<T>(); }

            // length of new array
            int length = endingIndex.GetValueOrDefault() - startingIndex;
            T[] result = new T[length];
            Array.Copy(data, startingIndex, result, 0, length);
            return result;
        }

    }
}
