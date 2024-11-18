using System.Text.Json.Serialization;

namespace SecretNight.Entities.Models
{
    public partial class Producto
    {
        [JsonPropertyName("idProductos")]
        public int IdProductos { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }

        [JsonPropertyName("talle")]
        public string Talle { get; set; } = null!;
    }
}
