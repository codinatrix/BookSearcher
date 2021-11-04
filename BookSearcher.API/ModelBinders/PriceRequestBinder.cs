using BookSearcher.API.RequestObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace BookSearcher.API.ModelBinders
{
    public class PriceRequestBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string modelName = bindingContext.ModelName;
            ValueProviderResult urlQueryParameter = bindingContext.ValueProvider.GetValue(modelName);

            if (urlQueryParameter.Equals(ValueProviderResult.None))
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, urlQueryParameter);

            string urlQueryValue = urlQueryParameter.FirstValue;

            if (string.IsNullOrEmpty(urlQueryValue))
                return Task.CompletedTask;

            try
            {
                double[] priceParameters = Array.ConvertAll(urlQueryValue.Split('&'), double.Parse);

                PriceRequest priceRequest = null;
                priceRequest = new PriceRequest(priceParameters);

                if (priceRequest == null)
                    throw new ArgumentException("Incorrectly formatted price parameters");

                bindingContext.Result = ModelBindingResult.Success(priceRequest);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Not supported price string.");
            }
            return Task.CompletedTask;
        }
    }
}