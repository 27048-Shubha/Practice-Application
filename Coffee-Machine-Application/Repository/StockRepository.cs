using Coffee_Machine_Application.Enums;
using Coffee_Machine_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Repository
{
    internal class StockRepository
    {
        private object _stockLock = new ();
        private static IngredientQuantity StockQuantity = new IngredientQuantity();

        public void UseIngredients(IngredientQuantity quantity, QuantityRange range)
        {
            lock (_stockLock)
            {
                StockQuantity.CoffeeBean -= ((int)quantity.CoffeeBean * (int)range);
                StockQuantity.Milk -= ((int)quantity.Milk * (int)range);
                StockQuantity.Sugar -= ((int)quantity.Sugar * (int)range);
                StockQuantity.Water -= ((int)quantity.Water * (int)range);
            }
        }

        public IngredientQuantity GetCurrentStockQuantity()
        {
            lock (_stockLock)
            {
                return new IngredientQuantity(StockQuantity);
            }
        }

        public int Refill(IngredientType ingredientType)
        {
            lock (_stockLock)
            {
                int refilledQuantity = 0;
                switch (ingredientType)
                {
                    case IngredientType.CoffeeBean:
                        refilledQuantity = (int)(IngredientRange.Max) - (int)StockQuantity.CoffeeBean;
                        StockQuantity.CoffeeBean = (int) IngredientRange.Max;
                        break;

                    case IngredientType.Milk:
                        refilledQuantity = (int) IngredientRange.Max - (int) StockQuantity.Milk;
                        StockQuantity.Milk = (int)IngredientRange.Max;
                        break;

                    case IngredientType.Water:
                        refilledQuantity = (int)IngredientRange.Max - (int)StockQuantity.Water;
                        StockQuantity.Water = (int)IngredientRange.Max;
                        break;

                    case IngredientType.Sugar:
                        refilledQuantity = (int)IngredientRange.Max - (int)StockQuantity.Sugar;
                        StockQuantity.Sugar = (int)IngredientRange.Max;
                        break;
                }

                return refilledQuantity;

            }
            
        }
    }
}
