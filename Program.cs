using System;
using System.Collections.Generic;
using System.Threading;

namespace CacheOfObjects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(Cache c = new Cache(3, TimeSpan.FromSeconds(5)))
            {
                Item i1 = new Item("log1.txt");
                Item i2 = new Item("log2.txt");
                Item i3 = new Item("log3.txt");
                Item i4 = new Item("log4.txt");
                c.Add(i1);
                c.Add(i2);
                c.Add(i3);

                for (int q = 0; q < 5; q++)
                {
                    foreach (var I in c)
                    {
                        var i = I as Item;
                        i.WriteIntoFile();
                    }
                    Thread.Sleep(2000);
                }

                Thread.Sleep(5000);

                c.Add(i4);
                i4.WriteIntoFile();
            }
        }

    }
}
