﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SailorMoon.Models
{
    public partial class Asistencias
    {
        public int IdAsistencias { get; set; }
        public string InastJustificada { get; set; }
        public string InastInjustificada { get; set; }
        public string TardJustificada { get; set; }
        public string TardInjustificada { get; set; }
        public int? IdEstudiante { get; set; }

        public virtual Estudiante IdEstudianteNavigation { get; set; }
    }
}