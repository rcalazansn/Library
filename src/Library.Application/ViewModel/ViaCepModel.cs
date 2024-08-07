﻿namespace Library.Application.ViewModel
{
    public class ViaCepModel
    {
        public ViaCepModel
        (
            string cep, string logradouro, string complemento, 
            string bairro, string localidade, string uf)
        {
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }

        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string Uf { get; private set; }
    }
}
