using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca.Dominio
{
    public class Auditoria
    {
        public Guid CreacionUsuario {get;set;} 
        public  DateTime FechaCreacion {get;set;}=DateTime.Now;
        
    }
}