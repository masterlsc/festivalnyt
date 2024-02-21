using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyDropDownExtension<T>
{

    private static MyDropDownExtension<T> Instance;
    public List<MyDropDownExtensionItem<T>> items;

    public MyDropDownExtension()
    {
        items = new List<MyDropDownExtensionItem<T>>();
    }

    public static MyDropDownExtension<T> GetInstance()
    {
        if (Instance == null)
        {
            Instance = new MyDropDownExtension<T>();

        }

        return Instance;
    }

    public void Add(T obj, int index)
    {
        items.Add(new MyDropDownExtensionItem<T>(obj, index));
    }

    public MyDropDownExtensionItem<T> Get(int index)
    {
        return items.SingleOrDefault(i => i.Index == index);
    }

    public class MyDropDownExtensionItem<I>
    {
        public I item;
        public int Index;

        public MyDropDownExtensionItem(I obj, int index)
        {
            item = obj;
            Index = index;
        }

    }


}
