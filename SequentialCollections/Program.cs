using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequentialCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var MyQueue = new Queue<string>();
            MyQueue.Enqueue("First");
            MyQueue.Enqueue("Second");
            MyQueue.Enqueue("Third");
            MyQueue.Enqueue("Fourth");

            while (MyQueue.Count > 0)
            {
                Console.WriteLine("Dequeuing '{0}'", MyQueue.Dequeue());
            }
            Console.WriteLine("");

            var MyStack = new Stack<string>();
            MyStack.Push("First");
            MyStack.Push("Second");
            MyStack.Push("Third");
            MyStack.Push("Fourth");

            while (MyStack.Count > 0)
            {
                Console.WriteLine("Dequeuing '{0}'", MyStack.Pop());
            }
            Console.WriteLine("");

            var MyHash = new Hashtable
            {
                { '0', "Zero" },
                { '1', "One" },
                { '2', "Two" },
                { '3', "Three" },
                { '4', "Four" },
                { '5', "Five" },
                { '6', "Six" },
                { '7', "Seven" },
                { '8', "Eight" },
                { '9', "Nine" }
            };

            var MyString = "1246983750";

            foreach (var Number in MyString)
            {
                if (MyHash.ContainsKey(Number)) { Console.WriteLine(MyHash[Number].ToString()); }
            }
            Console.WriteLine("");

            var MyListDictionary = new ListDictionary
            {
                { "United States", "Estados Unidos" },
                { "Canada", "Canadá" },
                { "Spain", "España" }
            };

            Console.WriteLine(MyListDictionary["Spain"]+" "+ MyListDictionary["Canada"]);
            Console.WriteLine("");

            var MyDictionary = new Dictionary<int,string>
            {
                {1, "Hungary" },
                {2, "Dominica" },
                {3, "Uruguay" },
                {4, "Afhanistan" },
                {5, "Cambodia" },
            };

            foreach (KeyValuePair<int, string> kvp in MyDictionary)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }



            Console.ReadKey();
        }

    }
}
