using System;
using System.Collections.Generic;
using System.Linq;

namespace LFU_Cache
{
    class Program
    {
        static void Main(string[] args)
        {
            LFUCache lfu = new LFUCache(2);
            lfu.Put(2, 1);
            lfu.Put(1, 1);
            lfu.Put(2, 3);
            lfu.Put(4, 1);
            Console.WriteLine(lfu.Get(1));
            Console.WriteLine(lfu.Get(2));

            //lfu.Put(2, 1);
            //lfu.Put(3,2);
            //Console.WriteLine(lfu.Get(3));
            //Console.WriteLine(lfu.Get(2));
            //lfu.Put(4, 3);
            //Console.WriteLine(lfu.Get(2));
            //Console.WriteLine(lfu.Get(3));
            //Console.WriteLine(lfu.Get(4));
            //lfu.Put(2, 2);
            //lfu.Put(1, 1);
            //Console.WriteLine(lfu.Get(2));
            //Console.WriteLine(lfu.Get(1));
            //Console.WriteLine(lfu.Get(2));
            //lfu.Put(3, 3);
            //lfu.Put(4, 4);

            //Console.WriteLine(lfu.Get(3));
            //Console.WriteLine(lfu.Get(2));
            //Console.WriteLine(lfu.Get(1));
            //Console.WriteLine(lfu.Get(4));
            //lfu.Put(1, 1);
            //lfu.Put(2, 2);
            //Console.WriteLine(lfu.Get(1)); 
            //lfu.Put(3, 3);
            //Console.WriteLine(lfu.Get(2));
            //Console.WriteLine(lfu.Get(3)); 
            //lfu.Put(4, 4);
            //Console.WriteLine(lfu.Get(1));
            ////lfu.Put(5, 5);
            //Console.WriteLine(lfu.Get(3));
            //Console.WriteLine(lfu.Get(4));
            //lfu.Put(2, 1);
            //lfu.Put(3, 2);
            //Console.WriteLine(lfu.Get(3));
            //Console.WriteLine(lfu.Get(2));
            //lfu.Put(4, 3);
            //Console.WriteLine(lfu.Get(2));
            //Console.WriteLine(lfu.Get(3));
            //Console.WriteLine(lfu.Get(4));
        }

        public class LFUCache
        {
            int CAPACITY;
            Dictionary<int, (int value, int count, LinkedListNode<int> node)> reference;
            Dictionary<int, LinkedList<int>> frequency;
            int LEASTRECENTLYUSEDCOUNT;

            public LFUCache(int CAPACITY)
            {
                this.CAPACITY = CAPACITY;
                LEASTRECENTLYUSEDCOUNT = 1;
                reference = new Dictionary<int, (int value, int count, LinkedListNode<int> node)>();
                frequency = new Dictionary<int, LinkedList<int>>();
            }

            public int Get(int key)
            {
                if (!reference.ContainsKey(key))
                {
                    return -1;
                }
                else
                {
                    int value = reference[key].value;
                    updateKey(key, value);
                    return value;
                }
            }

            public void Put(int key, int value)
            {
                if (this.CAPACITY < 1)
                {
                    return;
                }

                if (!reference.ContainsKey(key))
                {
                    if (reference.Count >= this.CAPACITY)
                    {
                        var minNode = frequency[LEASTRECENTLYUSEDCOUNT].Last;
                        reference.Remove(minNode.Value);
                        frequency[LEASTRECENTLYUSEDCOUNT].Remove(minNode);
                        if (frequency[LEASTRECENTLYUSEDCOUNT].Count == 0)
                        {
                            frequency.Remove(LEASTRECENTLYUSEDCOUNT);
                        }
                    }
                    int count = 1;
                    LEASTRECENTLYUSEDCOUNT = 1;
                    var node = new LinkedListNode<int>(key);
                    var item = (value, count, node);
                    reference[key] = item;
                    if (!frequency.ContainsKey(count))
                    {
                        frequency[count] = new LinkedList<int>();
                    }
                    frequency[count].AddFirst(node);
                }
                else
                {
                    updateKey(key, value);
                }
            }

            void updateKey(int key, int value)
            {
                var item = reference[key];
                int count = item.count;
                var node = item.node;
                frequency[count].Remove(node);
                if (frequency[count].Count == 0)
                {
                    if (LEASTRECENTLYUSEDCOUNT == count)
                    {
                        LEASTRECENTLYUSEDCOUNT++;
                    }
                    frequency.Remove(count);
                }
                var nodeNew = new LinkedListNode<int>(key);
                var itemNew = (value, count + 1, nodeNew);
                reference[key] = itemNew;
                if (!frequency.ContainsKey(count + 1))
                {
                    frequency[count + 1] = new LinkedList<int>();
                }
                frequency[count + 1].AddFirst(nodeNew);
            }
        }
    }
}
