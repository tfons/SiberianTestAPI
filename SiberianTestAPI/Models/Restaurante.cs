using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiberianTestAPI.Models
{
    public class Restaurante
    {
        public int _id_restaurante { get; set; }
        public string _nombre_restaurante {get;set;}
        public int _id_ciudad { get; set; }
        public string _nombre_ciudad { get; set; }
        public int _numero_aforo { get; set; }
        public string _telefono { get; set; }
        public string _fecha_creacion {get;set;}
}
}
