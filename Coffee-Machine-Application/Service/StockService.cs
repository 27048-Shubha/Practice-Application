using Coffee_Machine_Application.Enums;
using Coffee_Machine_Application.Model;
using Coffee_Machine_Application.Repository;

namespace Coffee_Machine_Application.Service
{
    public class StockService
    {
        private readonly StockRepository _stockRepository;
        private bool isCoffeeBeenAvailable = false;
        private bool isWaterAvailable = false;
        private bool isMilkAvailable = false;
        private bool isSugarAvailable = false;
        internal StockService(StockRepository stockRepository)
        {
            this._stockRepository = stockRepository;
        }

        public int RefillIngredients()
        {
            int refilledCount = 0; 

            if (!this.isCoffeeBeenAvailable)
            {
                refilledCount += this._stockRepository.Refill(IngredientType.CoffeeBean);
            }

            if (!this.isMilkAvailable)
            {
                refilledCount += this._stockRepository.Refill(IngredientType.Milk);
            }

            if (!this.isWaterAvailable)
            {
                refilledCount += this._stockRepository.Refill(IngredientType.Water);
            }

            if (!this.isSugarAvailable)
            {
                refilledCount += this._stockRepository.Refill(IngredientType.Sugar);
            }

            return refilledCount;
        }

        public int RefillStock(IngredientType ingredient)
        {
            return this._stockRepository.Refill(ingredient);
        }

        public bool IsAllIngredientsAvailable(CoffeeType type)
        {
            IngredientQuantity currentQuantity = this.FetchCurrentQuantity();
            IngredientQuantity requiredQuantity = this.FetchDefaultQuantity(type);

            this.isCoffeeBeenAvailable = currentQuantity.CoffeeBean >= requiredQuantity.CoffeeBean;
            this.isWaterAvailable = currentQuantity.Water >= requiredQuantity.Water;
            this.isMilkAvailable = currentQuantity.Milk >= requiredQuantity.Milk;
            this.isSugarAvailable = currentQuantity.Sugar >= requiredQuantity.Sugar;

            return (isCoffeeBeenAvailable && isWaterAvailable) && (isMilkAvailable && isSugarAvailable);
        }

        public int ConsumeIngredients(CoffeeType type)
        {
            IngredientQuantity quantity = this.FetchDefaultQuantity(type);
            this._stockRepository.UseIngredients(quantity);
            int value = ((int)quantity.CoffeeBean + (int)quantity.Milk + (int)quantity.Water + (int)quantity.Sugar);
            return value;
        }

        public IngredientQuantity FetchCurrentQuantity()
        {
            return this._stockRepository.GetCurrentStockQuantity();
        }

        public IngredientQuantity FetchDefaultQuantity(CoffeeType type)
        {
            switch (type)
            {
                case CoffeeType.Americano:
                    return new IngredientQuantity(IngredientRange.Medium, IngredientRange.No, IngredientRange.High, IngredientRange.Low);

                case CoffeeType.Cappucino:
                    return new IngredientQuantity(IngredientRange.Medium, IngredientRange.Medium, IngredientRange.No, IngredientRange.Low);

                case CoffeeType.Espresso:
                    return new IngredientQuantity(IngredientRange.High, IngredientRange.No, IngredientRange.No, IngredientRange.Low);

                case CoffeeType.Latte:
                    return new IngredientQuantity(IngredientRange.Low, IngredientRange.High, IngredientRange.No, IngredientRange.Medium);

                default: // By default: Americano
                    return new IngredientQuantity(IngredientRange.Medium, IngredientRange.No, IngredientRange.High, IngredientRange.Low);
            }
        } 
    }
}
