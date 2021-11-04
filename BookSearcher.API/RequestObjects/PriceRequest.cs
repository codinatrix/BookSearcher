using BookSearcher.API.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookSearcher.API.RequestObjects
{
    [ModelBinder(BinderType = typeof(PriceRequestBinder))]
    public class PriceRequest
    {
        public PriceRequest(double[] priceParameters)
        {
            if (priceParameters.Length == 1)
            {
                SinglePrice = priceParameters[0];
            }
            else if (priceParameters.Length == 2)
            {
                MinPrice = priceParameters[0];
                MaxPrice = priceParameters[1];
            }
            else
            {
                throw new ArgumentException("PriceRequest must have one or two constructor parameters.");
            }
        }

        // Used as a parameter to search for a range of prices
        [Range(0, double.MaxValue, ErrorMessage = "Value may not be a negative number")]
        public double? MinPrice { get; }
        // Used as a parameter to search for a range of prices
        [Range(0, double.MaxValue, ErrorMessage = "Value may not be a negative number")]
        public double? MaxPrice { get; }
        // Used as a parameter to search a single price
        [Range(0, double.MaxValue, ErrorMessage = "Value may not be a negative number")]
        public double? SinglePrice { get; }

        public bool isSinglePrice => SinglePrice.HasValue;
        public bool isPriceRange => MinPrice.HasValue && MaxPrice.HasValue;
    }
}
