using System;
using System.ComponentModel;

namespace Vaquinha.Domain.ViewModels
{
    public class DoacaoViewModel
    {
        public decimal Valor { get; set; }
        
        [DisplayName("Aceito pagar a taxa de 20%")]
        public bool DoadorAceitouTaxa { get; set; }
        public PessoaViewModel DadosPessoais { get; set; }
        public EnderecoViewModel EnderecoCobranca { get; set; }
        public CartaoCreditoViewModel FormaPagamento { get; set; }
    }
}