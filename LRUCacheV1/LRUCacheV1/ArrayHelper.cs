using System;
using System.Collections.Generic;
using System.Text;

namespace LRUCacheV1
{
    public static class ArrayHelper
    {
        /// <summary>
        /// <see cref="https://referencesource.microsoft.com/#mscorlib/system/collections/generic/list.cs,9808f1f5ef16c436"/>
        /// </summary>
        public static T[] EnsureCapacity<T>(this T[] array, int newLimit)
        {
            if (array.Length == newLimit || array.Length > newLimit)
                return array;

            T[] newArray = new T[newLimit];

            Array.Copy(array, 0, newArray, 0, array.Length);

            return newArray;
        }

        public static LRUItem<T> BinarySearch<T>(this LRUItem<T>[] array, int key)
        {
            return BinarySearch(array, key, 0, array.Length - 1);
        }

        /// <summary>
        /// <see cref="https://www.youtube.com/watch?v=P3YID7liBug"/>
        /// <see cref="https://www.c-sharpcorner.com/blogs/binary-search-implementation-using-c-sharp1"/>
        /// </summary>
        private static LRUItem<T> BinarySearch<T>(this LRUItem<T>[] array, int key, int start, int end)
        {
            int lowest = start;
            int highest = end;
            while (lowest <= highest)
            {
                int middle = (lowest + highest) / 2;
                if (key == array[middle].key)
                {
                    return array[middle];
                }
                else if (key < array[middle].key)
                {
                    return BinarySearch(array, key, lowest, middle - 1);
                }
                else
                {
                    return BinarySearch(array, key, middle + 1, highest);
                }
            }

            return null;
        }

        public static LRUItem<T>[] QuickSort<T>(this LRUItem<T>[] array)
        {
            QuickSort(array, 0, array.Length - 1);

            return array;
        }

        /// <summary>
        /// <see cref="https://www.youtube.com/watch?v=Hoixgm4-P4M"/>
        /// <see cref="https://raphaelcardoso.com.br/algoritmos-de-ordenacao-em-csharp/"/>
        /// </summary>
        private static void QuickSort<T>(LRUItem<T>[] array, int startingPoint, int endPoint)
        {
            if (array.Length <= 1)
                return;

            int middle = (startingPoint + endPoint) / 2;

            int pivot = array[middle].key;

            int leftIndex = startingPoint;
            int rightIndex = endPoint;


            if (leftIndex >= rightIndex)
                return;

            while (leftIndex <= rightIndex)
            {
                // Find Highest to the left
                while (array[leftIndex].key < pivot)
                {
                    // When Highest number found, move index to the right
                    leftIndex++;
                }

                // Find Lowest to the left
                while (array[rightIndex].key > pivot)
                {
                    // When Lower number found, move index to the right
                    rightIndex--;
                }

                if (leftIndex >= rightIndex)
                {
                    // If left higher or equal than right, stop sorting
                    break;
                }
                else
                {
                    var swap = array[leftIndex];
                    array[leftIndex] = array[rightIndex];
                    array[rightIndex] = swap;
                }
            }

            QuickSort(array, startingPoint, rightIndex - 1);
            QuickSort(array, rightIndex + 1, endPoint);
        }

    }
}
