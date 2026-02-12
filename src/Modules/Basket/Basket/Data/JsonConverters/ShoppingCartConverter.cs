using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Data.JsonConverters;

internal class ShoppingCartConverter : JsonConverter<ShoppingCart>
{
    public override ShoppingCart? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var jsonObject = jsonDocument.RootElement;

        var id = jsonObject.GetProperty("id").GetGuid();
        var username = jsonObject.GetProperty("username").GetString()!;

        var shoppingCart = ShoppingCart.Create(id, username);

        var itemsArray = jsonObject.GetProperty("items").EnumerateArray();
        foreach (var itemElement in itemsArray)
        {
            var itemId = itemElement.GetProperty("id").GetGuid();
            var productId = itemElement.GetProperty("productId").GetGuid();
            var quantity = itemElement.GetProperty("quantity").GetInt32();
            var color = itemElement.GetProperty("color").GetString()!;
            var price = itemElement.GetProperty("price").GetDecimal();
            var productName = itemElement.GetProperty("productName").GetString()!;

            shoppingCart.ReconstructItem(
                itemId,
                productId,
                quantity,
                color,
                price,
                productName);
        }

        return shoppingCart;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ShoppingCart value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("id", value.Id.ToString());
        writer.WriteString("username", value.Username);

        writer.WriteStartArray("items");
        foreach (var item in value.Items)
        {
            JsonSerializer.Serialize(writer, item, options);
        }
        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
