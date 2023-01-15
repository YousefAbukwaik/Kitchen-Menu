using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Globalization;

namespace Home_Project
{
    class Program
    {
        static void SaveFile(string Path, string str)
        {
            File.AppendAllText(Path, str + "\n");
        }

        static void editFile(string Path, string[] str)
        {
            StreamWriter writer = new StreamWriter(Path);
            for (int i = 0; i < str.Length; i++)
            {
                writer.WriteLine(str[i]);
            }
            writer.Close();
        }

        static bool compareIng(string[] str1, string[] str2)
        {

            int flag = 0;
            foreach (string ing in str1)
            {
                for (int i = 0; i < str2.Length; i++)
                {
                    if (ing.Trim().Equals(str2[i].Trim()))
                        flag++;
                }
            }
            if (flag == str1.Length)
                return true;

            return false;
        }

        static void swing(string path)
        {
            Console.WriteLine("You will find your output at file output.txt \n enter the ingredients \n");

            string str = Console.ReadLine();
            string[] strings = str.Split(',');

            string[] allLines = File.ReadAllLines(path);

            for (int i = 0; i < allLines.Length; i++)
            {
                string[] items = allLines[i].Split(',');
                string myString = items[2];
                string[] searchIngs = myString.Split('-');
                string[] searchIng = new string[searchIngs.Length];
                int j = -1;
                foreach (string ing in searchIngs)
                {
                    j++;
                    searchIng[j] = ing.Trim().Split(' ')[0];

                }
                if (compareIng(strings, searchIng))
                {
                    SaveFile(output, allLines[i]);
                }

            }

        }


        static void search(string path, int k)
        {
            Console.WriteLine("You will find your output at file output.txt \n enter your search key  \n");

            string str = Console.ReadLine();
            if (isfound(str, path, k))
            {
                SaveFile(output, "recipe does not exist");

            }
            else
            {
                string[] lines = File.ReadAllLines(path);
                SaveFile(output, lines[searchFlag]);
            }

        }


        static bool isfound(string str, string path, int j)
        {
            string[] reader = File.ReadAllLines(path);

            for (int i = 0; i < reader.Length; i++)
            {
                string[] items = reader[i].Split(',');
                string myString = items[j];

                if (myString.Equals(str))

                {
                    searchFlag = i;
                    return false;
                }

            }

            return true;
        }

        static void Add(string path)

        {
            Recipe recipe;

            Console.WriteLine("we are in Add Menu");
            Console.WriteLine("enter the recipe name: \n");
            string name = Console.ReadLine();
            if (isfound(name, path, 0))
            {
                Console.WriteLine("enter category \n");
                string category = Console.ReadLine();
                Console.WriteLine("please enter the ingredients\n ps:name quantity, name quantity, ....etc \n");
                string ingredient = Console.ReadLine();
                string[] ings = ingredient.Split(',');
                Console.WriteLine("please enter the cooking process");
                string process = Console.ReadLine();
                recipe = new Recipe(name, category, process, ings);
                SaveFile(path, recipe.Alldata);
                Console.WriteLine("Add is sucessful");
            }
            else
            {
                Console.WriteLine("the name is used before");
                Mainmenu(path);
            }

        }

        static void edit(string path)
        {
            Console.WriteLine("we are in Edit menu \n");
            Console.WriteLine("Enter your search key  \n");
            string str = Console.ReadLine();

            if (isfound(str, path, 0))
            {
                Console.WriteLine("Recipe does not exist");
                Mainmenu(path);
            }
            else
            {
                string[] lines = File.ReadAllLines(path);
                Console.WriteLine(lines[searchFlag] + "\n");
                Console.WriteLine(" Write thr full recipe with the same format with edited information \n");
                string editrec = Console.ReadLine();
                lines[searchFlag] = editrec;
                editFile(path, lines);
                Mainmenu(path);
            }
        }

        static void sae(string path)
        {
            Console.WriteLine("we are in search menu");
            Console.WriteLine("want to search by: \n " +
               "1- name \n" +
               "2- category \n" +
               "3- ingredients\n ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: search(path, 0); break;
                case 2: search(path, 1); break;
                case 3: swing(path); break;

                default: Console.WriteLine("invalid input \n "); Mainmenu(path); break;
            }
        }
        static void Mainmenu(string path)
        {
            Console.WriteLine("Welcome to cook boook \n " +
                "choose one of the following: \n" +
                "1- Add new reciepe \n" +
                "2- Search \n" +
                "3- Edit \n ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: Add(path); break;
                case 3: edit(path); break;
                case 2: sae(path); break;

                default: Console.WriteLine("invalid input \n "); Mainmenu(path); break;
            }
        }
        static int searchFlag = -1;
        static string output = "output.txt";
        static void Main(string[] args)
        {
            string filename = "recipes.txt";
            Mainmenu(filename);
        }
    }
}
