using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca.Dominio
{
    public class Auditoria
    {
        public required string CreacionUsuario {get;set;}
        public required DateTime FechaCreacion {get;set;}
    }
}