using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Model
{
    using Coffee_Machine_Application.Enums;
    public class IngredientQuantity
    {
        private const int MaxStockQuantity = 10;
        internal IngredientQuantity()
        {
            this.CoffeeBean = MaxStockQuantity;
            this.Milk = MaxStockQuantity;
            this.Water = MaxStockQuantity;
            this.Sugar = MaxStockQuantity;
        }

        internal IngredientQuantity(IngredientQuantity ingredient)
        {
            this.CoffeeBean = ingredient.CoffeeBean;
            this.Milk = ingredient.Milk;
            this.Water = ingredient.Water;
            this.Sugar = ingredient.Sugar;
        }

        internal IngredientQuantity(IngredientRange coffeeBean, IngredientRange milk, IngredientRange water, IngredientRange sugar)
        {
            this.CoffeeBean = (int) coffeeBean;
            this.Milk = (int) milk;
            this.Water = (int) water;
            this.Sugar = (int) sugar;
        }

        public int CoffeeBean{ get; set; }
        public int Milk { get; set; }
        public int Water { get; set; }
        public int Sugar { get; set; }

    }
}
