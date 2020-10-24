using System;
using System.Collections.Generic;

public static class ListSortHelper
{
    public static void Swap<T>(this List<T> list, int first, int second)
    {
        T temp = list[first];

        list[first] = list[second];

        list[second] = temp;
    }
    public static void CustomSort<T>(this List<T> toSort) where T : IComparable<T>
    {
        int n = toSort.Count;

        while(n > 1)
        {
            for(int i = 0; i < n -1; i++)
            {
                if(toSort[i].CompareTo(toSort[i + 1]) > 0)
                    toSort.Swap<T>(i, i + 1);
            }

            n--;
        }
    }

    public static void CustomSort<T>(this List<T> toSort, IComparer<T> comparer) where T : IComparable<T>
    {
        int n = toSort.Count;

        while(n > 1)
        {
            for(int i = 0; i < n -1; i++)
            {
                if(comparer.Compare(toSort[i], toSort[i + 1]) > 0)
                    toSort.Swap<T>(i, i + 1);
            }

            n--;
        }
    }

    public static void CustomSort<T>(this List<T> toSort, Comparison<T> comparison) where T : IComparable<T>
    {
        int n = toSort.Count;

        while(n > 1)
        {
            for(int i = 0; i < n -1; i++)
            {
                if(comparison(toSort[i], toSort[i + 1]) > 0)
                    toSort.Swap<T>(i, i + 1);
            }

            n--;
        }
    }
}