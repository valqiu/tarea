﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SailorMoon.Models
{
    public partial class Grado
    {
        public Grado()
        {
            Estudiante = new HashSet<Estudiante>();
        }

        public int IdGrado { get; set; }
        public string Grado1 { get; set; }
        public string Seccion { get; set; }
        public string Tutor { get; set; }

        public virtual ICollection<Estudiante> Estudiante { get; set; }
    }
}