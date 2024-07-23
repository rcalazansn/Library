using System.Text.Json.Serialization;

namespace Library.Application.Http.ViaCep
{
    public class ViaCepResponse
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }
        
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }
        
        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }
       
        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }
       
        [JsonPropertyName("localidade")]
        public string Localidade { get; set; }
       
        [JsonPropertyName("uf")]
        public string Uf { get; set; }
       
        [JsonPropertyName("unidade")]
        public string Unidade { get; set; }
       
        [JsonPropertyName("ibge")]
        public string Ibge { get; set; }
       
        [JsonPropertyName("gia")]
        public string Gia { get; set; }
    }
}
