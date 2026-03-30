using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model
{
    public class Administrador(int id, string nome, string email, string senha, byte adminitrador) : 
        PessoaFisica(id, nome, email, senha, adminitrador)
    {
    }
}
