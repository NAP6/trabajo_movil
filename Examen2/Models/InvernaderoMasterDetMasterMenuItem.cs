using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen2.Views.L
{
    public enum MenuItem
    {
        mqtt,
        historial,
        invernaderos
    }
    public class InvernaderoMasterDetMasterMenuItem
    {
        public MenuItem Id { get; set; }
        public string Title { get; set; }

    }
}