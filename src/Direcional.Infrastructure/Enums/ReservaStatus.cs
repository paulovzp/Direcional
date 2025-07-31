using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direcional.Infrastructure.Enums;

public enum ReservaStatus
{
    Pendente = 1,
    Confirmada = 2,
    Cancelada = 3,
    Expirada = 4
}

public enum TipoUsuario
{
    Admin = 0,
    Vendedor = 1,
    Cliente = 2
}