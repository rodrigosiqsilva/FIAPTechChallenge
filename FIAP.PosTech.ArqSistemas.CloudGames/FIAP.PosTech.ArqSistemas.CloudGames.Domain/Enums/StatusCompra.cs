using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Enums
{
    public  enum StatusCompra
    {
        [Description("Pedido em andamento")]
        PedidoEmAndamento,

        [Description("Pagamento pendente")]
        PagamentoPendente,

        [Description("Pagamento efetuado")]
        PagamentoEfetuado,

        [Description("Entrega pedente")]
        EntregaPendente,

        [Description("Concluído")]
        Concluido
    }
}
