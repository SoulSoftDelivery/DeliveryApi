namespace DeliveryApi.Models
{
    public class ClienteEndereco
    {
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public char Sexo { get; set; }
        public int EnderecoId { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Quadra { get; set; }
        public string Lote { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public bool Ativo { get; set; }
    }
}