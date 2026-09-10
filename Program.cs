using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        private static List<ColorChip> Chips = new List<ColorChip>();       // Unused Chips
        private static List<ColorChip> AllChips = new List<ColorChip>();
        private static int MaxList = 0;
        static List<ColorChip> LargestSet = new List<ColorChip>();    // Chips already used
        static void Main(string[] args)
        {
            Console.WriteLine("Chip Security System");
            Console.WriteLine("1. Use your own set of chips");
            Console.WriteLine("2. Use example [Blue, Yellow], [Red, Green], [Yellow, Red], [Orange, Purple]");
            Console.WriteLine("Enter \"exit\" to end program");
            string input = Console.ReadLine();
            while (input.ToLower() != "exit")
            {
                if (input != "1" && input != "2")
                {
                    Console.WriteLine("Invalid input, enter 1 or 2");
                    input = Console.ReadLine();
                    continue;
                }

                switch (input)
                {
                    case "1":
                        UserChipSet();
                        break;
                    case "2":
                        Chips.Add(new ColorChip(Color.Blue, Color.Yellow));
                        Chips.Add(new ColorChip(Color.Red, Color.Green));
                        Chips.Add(new ColorChip(Color.Yellow, Color.Red));
                        Chips.Add(new ColorChip(Color.Orange, Color.Purple));
                        break;

                };

                List<ColorChip> curList = new List<ColorChip>();
                GetChipSet(curList);
                for (int i = 0; i < LargestSet.Count; i++)
                {
                    Console.Write("[{0},{1}] ", LargestSet[i].StartColor, LargestSet[i].EndColor);
                }
                if (LargestSet.Count == 0)
                {
                    Console.WriteLine("No set");
                }
                input = Console.ReadLine();
            }
        }

        static void UserChipSet()
        {
            Console.WriteLine("Enter in chip set");
            Console.WriteLine("1 = Red\n2 = Green\n3 = Blue\n4 = Yellow\n5 = Purple\n6 = Orange");

            string input = "";
            while (true)
            {
                Color startColor = SetColors("Start Color");
                Color endColor = SetColors("End Color");

                Chips.Add(new ColorChip(startColor, endColor));
                Console.WriteLine("Done: Y/N");
                input = Console.ReadLine(); 

                Console.WriteLine("Current Chip set");
                for (int i = 0; i < Chips.Count; i++)
                {
                    Console.Write("{0}\n", Chips[i]);
                }
                if (input.ToLower() == "y")
                {
                    break;
                }
            }
            AllChips = new List<ColorChip>(Chips);
        }
        static void GetChipSet(List<ColorChip> curList)
        {
            if (AllChips.Count == 1 && AllChips[0].StartColor == Color.Blue && AllChips[0].EndColor == Color.Green)
            {
                LargestSet = new List<ColorChip>(Chips);
                return;
            }
            else if (AllChips.Count == 1)
            {
                Console.WriteLine("No set");
            }
            if (curList.Count > 0 && curList[curList.Count - 1].EndColor == Color.Green)
            {
                if (MaxList < curList.Count)
                {
                    MaxList = curList.Count;
                    Console.WriteLine("Found Chip Set size {0}", curList.Count);
                    LargestSet = new List<ColorChip>(curList);
                }
            }
            for (int i = 0; i < Chips.Count; i++)
            {
                ColorChip chip = Chips[i];
                if (curList.Count == 0 && Chips[i].StartColor == Color.Blue)
                {
                    curList.Add(Chips[i]);
                    Chips.RemoveAt(i);
                }

                if (curList.Count != 0 && curList[curList.Count - 1].EndColor == Chips[i].StartColor)
                {
                    curList.Add(Chips[i]);
                    Chips.RemoveAt(i);
                }
                else
                    continue;

                GetChipSet(curList);

                curList.RemoveAt(curList.Count - 1);
                Chips.Insert(i, chip);
            }

        }
        private static Color SetColors(string colorType)
        {
            Console.Write($"{colorType}: ");

            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 6)
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            return (Color)(input - 1);
        }
    }
}
