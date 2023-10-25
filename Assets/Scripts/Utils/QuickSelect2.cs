namespace Nowhere.Utility
{
    using System.Collections.Generic;

    /// <summary>
    /// The QuickSelect algorithm is a selection algorithm that uses a similar method as the QuickSort sorting algorithm. More information can be found on this page:
    /// https://en.wikipedia.org/wiki/Quickselect. 
    /// </summary>
    public static class QuickSelect2
    {
        public interface IRefComparer<T> where T : struct
        {
            int Compare(ref T x, ref T y);
        }

        /// <summary>
        /// Swap the first and second elements.
        /// </summary>
        /// <param name="array">The array that should be sorted.</param>
        /// <param name="firstIndex">The first index that should be swapped.</param>
        /// <param name="secondIndex">The second index that should be swapped.</param>
        private static void Swap<T>(T[] array, int firstIndex, int secondIndex)
        {
            T temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        public static TElement SmallestK<TElement, TComparer>(TElement[] array, int arrayCount, int k, TComparer comparer) where TComparer : struct, IComparer<TElement>
        {
            if (k > arrayCount - 1)
            {
                k = arrayCount - 1;
            }

            return SmallestK(array, 0, arrayCount - 1, k, ref comparer);
        }

        /// <summary>
        /// Returns the element that is the Kth smallest within the array.
        /// </summary>
        /// <param name="array">The array that should be searched.</param>
        /// <param name="startIndex">The starting index of the array.</param>
        /// <param name="endIndex">The ending index of the array.</param>
        /// <param name="k">The nth smallest value to retrieve. 0 indicates the smallest element, endIndex - 1 indicates the largest.</param>
        /// <param name="comparer">The IComparer used to compare the array.</param>
        /// <returns>The element that is the Kth smallest within the array.</returns>
        private static TElement SmallestK<TElement, TComparer>(TElement[] array, int startIndex, int endIndex, int k, ref TComparer comparer) where TComparer : struct, IComparer<TElement>
        {
            while (true)
            {
                if (startIndex == endIndex)
                {
                    return array[startIndex];
                }

                // Similar to the QuickSort algorithm, split the array into a subset and reorder based on the pivot.
                int pivotIndex = Partition(array, startIndex, endIndex, ref comparer);

                // If the pivot is same as k then the kth smallest value has been found. 
                if (pivotIndex == k)
                {
                    return array[pivotIndex];
                }

                if (k < pivotIndex)
                {
                    endIndex = pivotIndex - 1;
                }
                else
                {
                    startIndex = pivotIndex + 1;
                }
            }
        }

        /// <summary>
        /// Partition the array into two groups based on the pivot. All values smaller than the pivot will be moved to the left, and all values greater will be moved
        /// to the right. This is similar to the QuickSort algorithm.
        /// </summary>
        /// <param name="array">The array that should be sorted.</param>
        /// <param name="startIndex">The starting index of the array.</param>
        /// <param name="endIndex">The ending index of the array.</param>
        /// <param name="comparer">The IComparer used to compare the array.</param>
        /// <returns>The position of the pivot.</returns>
        private static int Partition<TElement, TComparer>(TElement[] array, int startIndex, int endIndex, ref TComparer comparer) where TComparer : struct, IComparer<TElement>
        {
            int pivotIndex = UnityEngine.Random.Range(startIndex, endIndex + 1);
            // The pivot has not been reordered yet. Move all elements that are less than the pivot to the left, and move all elements that are greater to the right.
            TElement pivotValue = array[pivotIndex];
            // The pivot should be moved to the end so it won't be compared against itself.
            Swap(array, pivotIndex, endIndex);
            int index = startIndex;
            for (int i = startIndex; i < endIndex; ++i)
            {
                if (comparer.Compare(array[i], pivotValue) < 0)
                {
                    Swap(array, index, i);
                    index++;
                }
            }
            // Move pivot back
            Swap(array, index, endIndex);
            return index;
        }

        public static TElement SmallestKRefCompare<TElement, TComparer>(TElement[] array, int arrayCount, int k, TComparer comparer)
            where TElement : struct
            where TComparer : struct, IRefComparer<TElement>
        {
            if (k > arrayCount - 1)
            {
                k = arrayCount - 1;
            }

            return SmallestKRefCompare(array, 0, arrayCount - 1, k, ref comparer);
        }

        /// <summary>
        /// Returns the element that is the Kth smallest within the array.
        /// </summary>
        /// <param name="array">The array that should be searched.</param>
        /// <param name="startIndex">The starting index of the array.</param>
        /// <param name="endIndex">The ending index of the array.</param>
        /// <param name="k">The nth smallest value to retrieve. 0 indicates the smallest element, endIndex - 1 indicates the largest.</param>
        /// <param name="comparer">The IComparer used to compare the array.</param>
        /// <returns>The element that is the Kth smallest within the array.</returns>
        private static TElement SmallestKRefCompare<TElement, TComparer>(TElement[] array, int startIndex, int endIndex, int k, ref TComparer comparer)
            where TElement : struct
            where TComparer : struct, IRefComparer<TElement>
        {
            while (true)
            {
                if (startIndex == endIndex)
                {
                    return array[startIndex];
                }

                // Similar to the QuickSort algorithm, split the array into a subset and reorder based on the pivot.
                int pivotIndex = PartitionRefCompare(array, startIndex, endIndex, ref comparer);

                // If the pivot is same as k then the kth smallest value has been found. 
                if (pivotIndex == k)
                {
                    return array[pivotIndex];
                }

                if (k < pivotIndex)
                {
                    endIndex = pivotIndex - 1;
                }
                else
                {
                    startIndex = pivotIndex + 1;
                }
            }
        }

        /// <summary>
        /// Partition the array into two groups based on the pivot. All values smaller than the pivot will be moved to the left, and all values greater will be moved
        /// to the right. This is similar to the QuickSort algorithm.
        /// </summary>
        /// <param name="array">The array that should be sorted.</param>
        /// <param name="startIndex">The starting index of the array.</param>
        /// <param name="endIndex">The ending index of the array.</param>
        /// <param name="comparer">The IComparer used to compare the array.</param>
        /// <returns>The position of the pivot.</returns>
        private static int PartitionRefCompare<TElement, TComparer>(TElement[] array, int startIndex, int endIndex, ref TComparer comparer)
            where TElement : struct
            where TComparer : struct, IRefComparer<TElement>
        {
            int pivotIndex = UnityEngine.Random.Range(startIndex, endIndex + 1);
            // The pivot has not been reordered yet. Move all elements that are less than the pivot to the left, and move all elements that are greater to the right.
            TElement pivotValue = array[pivotIndex];
            // The pivot should be moved to the end so it won't be compared against itself.
            Swap(array, pivotIndex, endIndex);
            int index = startIndex;
            for (int i = startIndex; i < endIndex; ++i)
            {
                if (comparer.Compare(ref array[i], ref pivotValue) < 0)
                {
                    Swap(array, index, i);
                    index++;
                }
            }
            // Move pivot back
            Swap(array, index, endIndex);
            return index;
        }
    }
}