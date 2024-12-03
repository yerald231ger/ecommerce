using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data;

public class CachedBasketRepository(
    IBasketRepository basketRepository,
    IDistributedCache cache
)
    : IBasketRepository
{
    private readonly IBasketRepository _basketRepository = basketRepository;

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

        if (cachedBasket is not null)
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        var basket = await _basketRepository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        var storedBasket = await _basketRepository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(storedBasket.UserName, JsonSerializer.Serialize(storedBasket), cancellationToken);

        return storedBasket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await _basketRepository.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        return true;
    }
}