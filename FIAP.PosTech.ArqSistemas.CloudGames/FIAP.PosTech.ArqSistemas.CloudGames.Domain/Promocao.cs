using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain
{
    public class Promocao
    {
        public int Id { get; set; }
        public required string Descricao { get; set; }
        public byte Ativa { get; set; }
    }
}
