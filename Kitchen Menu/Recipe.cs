using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Project
{
    class Recipe
    {
        string name;
        string category;
        string process;
        string[] ings;

        public string Alldata
        {
            get
            {
                string ing = string.Join("-", this.ings);
                return $"{name},{category}, {ing},{process}";
            }
        }
        public Recipe(string name, string category, string process, string[] ings)
        {
            this.name = name;
            this.category = category;
            this.process = process;
            this.ings = ings;
        }


    }
}
