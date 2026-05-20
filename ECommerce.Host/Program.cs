using ECommerce.Modules.Cart.Data;
using ECommerce.Modules.Cart.Features;
using ECommerce.Modules.Cart.Features.AddToCart;
using ECommerce.Modules.Cart.Features.CheckoutCart;
using ECommerce.Modules.Cart.Features.GetCart;
using ECommerce.Modules.Cart.Features.RemoveFromCart;
using ECommerce.Modules.Cart.Features.UpdateCartItem;
using ECommerce.Modules.Catalog.Data;
using ECommerce.Modules.Catalog.Features;
using ECommerce.Modules.Catalog.Features.CreateCategory;
using ECommerce.Modules.Catalog.Features.CreateProduct;
using ECommerce.Modules.Catalog.Features.GetCategory;
using ECommerce.Modules.Catalog.Features.GetHomepageItems;
using ECommerce.Modules.Catalog.Features.Search;
using ECommerce.Modules.Common;
using ECommerce.Modules.Sales.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var salesConnectionString = builder.Configuration.GetConnectionString("SalesConnection");
var cartConnectionString = builder.Configuration.GetConnectionString("CartConnection");

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseSqlite(connectionString, o => o.MigrationsAssembly("ECommerce.Modules.Catalog"));
});

builder.Services.AddDbContext<SalesDbContext>(options =>
{
    options.UseSqlite(salesConnectionString, o => o.MigrationsAssembly("ECommerce.Modules.Sales"));
});

builder.Services.AddDbContext<CartDbContext>(options =>
{
    options.UseSqlite(cartConnectionString, o => o.MigrationsAssembly("ECommerce.Modules.Cart"));
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Wolverine
builder.Host.UseWolverine(opts =>
{
    // This tells Wolverine to look for Handlers in your Module projects
    opts.Discovery.IncludeAssembly(typeof(ECommerce.Modules.Catalog.ICatalogModule).Assembly);
    opts.Discovery.IncludeAssembly(typeof(ECommerce.Modules.Sales.ISalesModule).Assembly);
    opts.Discovery.IncludeAssembly(typeof(ECommerce.Modules.Cart.ICartModule).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("My Modern API").WithTheme(ScalarTheme.DeepSpace); // Optional: customize style
    });
}

app.UseHttpsRedirection();

// Cart

app.MapGet(
        "/cart/checkout/{customerId:int}",
        async (int customerId, IMessageBus bus) =>
        {
            await bus.InvokeAsync(new CheckoutCartCommand(customerId));
            return Results.Ok("");
        }
    )
    .WithName("Checkout")
    .WithTags("Cart");

app.MapGet(
        "/cart/{customerId:int}",
        async (int customerId, IMessageBus bus) =>
        {
            var cart = await bus.InvokeAsync<CartDto?>(new GetCartQuery(customerId));
            return cart is null ? Results.NotFound() : Results.Ok(cart);
        }
    )
    .WithName("GetCart")
    .WithTags("Cart");

app.MapPost(
        "/cart/items",
        async (AddToCartCommand command, IMessageBus bus) =>
        {
            var cart = await bus.InvokeAsync<CartDto?>(command);
            return cart is null ? Results.NotFound() : Results.Ok(cart);
        }
    )
    .WithName("AddToCart")
    .WithTags("Cart");

app.MapPut(
        "/cart/items",
        async (UpdateCartItemCommand command, IMessageBus bus) =>
        {
            var cart = await bus.InvokeAsync<CartDto?>(command);
            return cart is null ? Results.NotFound() : Results.Ok(cart);
        }
    )
    .WithName("UpdateCartItem")
    .WithTags("Cart");

app.MapDelete(
        "/cart/{customerId:int}/items/{productId:int}",
        async (int customerId, int productId, IMessageBus bus) =>
        {
            var cart = await bus.InvokeAsync<CartDto?>(
                new RemoveFromCartCommand(customerId, productId)
            );
            return cart is null ? Results.NotFound() : Results.Ok(cart);
        }
    )
    .WithName("RemoveFromCart")
    .WithTags("Cart");

app.MapGet(
        "/catalog/homepage",
        async (IMessageBus bus) =>
        {
            return await bus.InvokeAsync<GetHomepageResponse>(new GetHomepageQuery());
        }
    )
    .WithName("GetHomepage")
    .WithTags("Catalog");

app.MapPost(
        "/catalog/products",
        async (CreateProductCommand command, IMessageBus bus) =>
        {
            var result = await bus.InvokeAsync<CreateProductResponse>(command);

            return Results.Created($"/catalog/products/{result.ProductId}", result);
        }
    )
    .WithName("AddProduct")
    .WithTags("Catalog");

app.MapGet(
        "/catalog/products/{id:int}",
        async (int id, IMessageBus bus) =>
        {
            return await bus.InvokeAsync<ProductDto>(new GetProductQuery(id));
        }
    )
    .WithName("GetProduct")
    .WithTags("Catalog");

app.MapPost(
        "/catalog/categories",
        async (CreateCategoryCommand command, IMessageBus bus) =>
        {
            var result = await bus.InvokeAsync<CreateCategoryResponse>(command);

            return Results.Created($"/catalog/categories/{result.CategoryId}", result);
        }
    )
    .WithName("AddCategory")
    .WithTags("Catalog");

app.MapGet(
        "/catalog/categories/{id:int}",
        async (int id, IMessageBus bus) =>
        {
            return await bus.InvokeAsync<CategoryDto>(new GetCategoryQuery(id));
        }
    )
    .WithName("GetCategory")
    .WithTags("Catalog");

app.MapGet(
        "/catalog/Search",
        async (string? term, IMessageBus bus) =>
        {
            return await bus.InvokeAsync<SearchItemsResponse>(new SearchItemsQuery(term));
        }
    )
    .WithName("SearchItems")
    .WithTags("Catalog");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
