using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Data.JsonConverters;

internal class ShoppingCartItemConverter : JsonConverter<ShoppingCartItem>
{
    public override ShoppingCartItem? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader); 
        var jsonObject = jsonDocument.RootElement;

        var id = jsonObject.GetProperty("id").GetGuid();
        var shoppingCartId = jsonObject.GetProperty("shoppingCartId").GetGuid();
        var productId = jsonObject.GetProperty("productId").GetGuid();
        var quantity = jsonObject.GetProperty("quantity").GetInt32();
        var color = jsonObject.GetProperty("color").GetString()!;
        var price = jsonObject.GetProperty("price").GetDecimal();
        var productName = jsonObject.GetProperty("productName").GetString()!;

        return new ShoppingCartItem
        (         
            id,
            shoppingCartId,
            productId,
            quantity,
            color,
            price,
            productName
        );
    }

    public override void Write(
        Utf8JsonWriter writer,
        ShoppingCartItem value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("id", value.Id.ToString());
        writer.WriteString("shoppingCartId", value.ShoppingCartId.ToString());
        writer.WriteString("productId", value.ProductId.ToString());
        writer.WriteNumber("quantity", value.Quantity);
        writer.WriteString("color", value.Color);
        writer.WriteNumber("price", value.Price);
        writer.WriteString("productName", value.ProductName);

        writer.WriteEndObject();
    }
}
