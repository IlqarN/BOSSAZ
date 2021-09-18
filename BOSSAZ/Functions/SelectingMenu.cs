using System;
using System.Linq;
namespace Menu
{
    public static class SelectingMenu
    {
        public static int Choose(string[] items)
        {
            int size = items.Length;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            while (true)
            {
                Console.Clear();
                int j = 0;
                foreach (var item in items)
                {
                    string temp = item;
                    if (temp.Length > 40)
                    {
                        temp = item.Substring(0, 37);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(item);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    return index;
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }

        public static int Choose(string dontSelect, string[] items)
        {
            int size = items.Length;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            while (true)
            {
                Console.Clear();
                int j = 0;
                Console.WriteLine(dontSelect);
                foreach (var item in items)
                {
                    string temp = item;
                    if (temp.Length > 40)
                    {
                        temp = item.Substring(0, 37);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(item);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    return index;
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }

        public static string ChooseByString(string[] items)
        {
            int size = items.Length;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            while (true)
            {
                Console.Clear();
                int j = 0;
                foreach (var item in items)
                {
                    string temp = item;
                    if (temp.Length > 40)
                    {
                        temp = item.Substring(0, 37);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(item);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    return items[index];
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }
    }
}