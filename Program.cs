using System;
using System.Collections.Generic;

namespace Caso1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrisisRespiratoria consultaDeHoy = new CrisisRespiratoria("Masculino", new DateTime(2022, 10, 23, 8, 12, 0), new DateTime(2022, 10, 23, 10, 12, 0));
            consultaDeHoy.ImprimirDatos();
            Console.WriteLine("Cambio de prueba por github");

            ConsultaSimple consulta = new ConsultaSimple("Andrés", "Villa Crepuscular", "Masculino", new DateTime(2004, 4, 28), new DateTime(2022, 05, 05, 13, 15, 30), new DateTime(2022, 05, 05, 15, 15, 30), 70, "Dolor por corazón roto");
            consulta.ImprimirDatos();
        }
    }

    class Paciente
    {
        private readonly string? direccion;
        private readonly DateTime? fechaNacimiento;
        private readonly string? nombre;
        private readonly string sexo;

        public Paciente(string sexo)
        {
            this.nombre = null;
            this.direccion = null;
            this.sexo = sexo;
            this.fechaNacimiento = null;
        }

        public Paciente(string nombre, string direccion, string sexo, DateTime fechaNacimiento)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.sexo = sexo;
            this.fechaNacimiento = fechaNacimiento;
        }

        public string? Direccion => direccion;

        public DateTime? FechaNacimiento => fechaNacimiento;

        public string? Nombre => nombre;

        public string Sexo => sexo;
    }

    class Consulta
    {
        private readonly DateTime horaEntrada;
        private readonly DateTime horaSalida;
        private readonly Paciente pacienteConsulta;
        private List<string> _indicacionesASeguir;

        public Consulta(string nombre, string direccion, string sexo, DateTime fechaNacimiento, DateTime horaEntrada, DateTime horaSalida)
        {
            this.IndicacionesASeguir = new List<string>();
            this.pacienteConsulta = new Paciente(nombre, direccion, sexo, fechaNacimiento);
            this.horaEntrada = horaEntrada;
            this.horaSalida = horaSalida;
        }

        public Consulta(string sexo, DateTime horaEntrada, DateTime horaSalida)
        {
            this.IndicacionesASeguir = new List<string>();
            this.pacienteConsulta = new Paciente(sexo);
            this.horaEntrada = horaEntrada;
            this.horaSalida = horaSalida;
            Console.WriteLine("lolazo");
        }

        public virtual void ImprimirDatos()
        {
            Console.WriteLine($"DATOS DEL PACIENTE");
            Console.WriteLine($"Nombre: {pacienteConsulta.Nombre ?? "INDETERMINADO"}");
            Console.WriteLine($"Dirección: {pacienteConsulta.Direccion ?? "INDETERMINADO"}");
            Console.WriteLine($"Fecha de nacimiento: {(pacienteConsulta.FechaNacimiento == new DateTime() ? pacienteConsulta.FechaNacimiento : "INDETERMINADO")}");// por alguna razón no deja usar operador nullish coalessing sobre tipo DateTime, a pesar de que puede albergar null 
            Console.WriteLine($"Hora de entrada: {horaEntrada}");
            Console.WriteLine($"Hora de entrada: {horaSalida}");

        }

        protected virtual void agregarIndicacionesASeguir()
        {
            bool agregarNuevo;
            do
            {
                Console.WriteLine($"Ingrese indicación Número {IndicacionesASeguir.Count + 1}");
                IndicacionesASeguir.Add(Console.ReadLine());
                Console.WriteLine("Desea agregar una nueva indicación? Ingrese 0 si es cierto, sino ingrese 1");
                agregarNuevo = Console.ReadLine() == "0";
            } while (agregarNuevo);
        }

        public List<string> IndicacionesASeguir { get => _indicacionesASeguir; set => _indicacionesASeguir = value; }
    }

    class ConsultaSimple : Consulta 
    {
        private readonly int tension;
        private readonly string sintomas;

        public ConsultaSimple(string nombre, string direccion, string sexo, DateTime fechaNacimiento, DateTime horaEntrada, DateTime horaSalida, int tension, string sintomas) : base(nombre, direccion, sexo, fechaNacimiento, horaEntrada, horaSalida)
        {
            this.tension = tension;
            this.sintomas = sintomas;
            agregarIndicacionesASeguir();
        }

        public override void ImprimirDatos()
        {
            base.ImprimirDatos();
            Console.WriteLine($"Tensión: {tension}");
            Console.WriteLine($"Síntomas: {sintomas}");
        }
    }

    class Crisis : Consulta
    {
        private List<string> _medicamentosATomar;
        private List<int> _dosisMedicamento;
        public Crisis(string sexo, DateTime horaEntrada, DateTime horaSalida) : base(sexo, horaEntrada, horaSalida)
        {
            MedicamentosATomar = new List<string>();
            DosisMedicamento = new List<int>();
        }

        public override void ImprimirDatos()
        {
            base.ImprimirDatos();
            Console.WriteLine("Medicamentos recetados:");

            for (int i = 0; i < MedicamentosATomar.Count; i++)
            {
                Console.WriteLine($"{MedicamentosATomar[i]} : {DosisMedicamento[i]}");
            }

            Console.WriteLine("Indicaciones:");

            foreach (string indicacion in IndicacionesASeguir)
            {
                Console.WriteLine(indicacion);
            }

        }

        protected override void agregarIndicacionesASeguir()
        {
            base.agregarIndicacionesASeguir();
            bool agregarNuevo;
            do
            {
                Console.WriteLine("Lolazo");
                Console.WriteLine($"Ingrese medicamento Número {MedicamentosATomar.Count + 1}");
                MedicamentosATomar.Add(Console.ReadLine());
                AgregarDosisMedicamento(MedicamentosATomar.Count - 1);
                Console.WriteLine("Desea agregar un nuevo medicamento? Ingrese 0 si es cierto, sino ingrese 1");
                agregarNuevo = Console.ReadLine() == "0";
            } while (agregarNuevo);
        }

        private void AgregarDosisMedicamento(int posicion)
        {
            Console.WriteLine($"Ingrese la dosis de {MedicamentosATomar[posicion]} (mg)");
            DosisMedicamento.Add(int.Parse(Console.ReadLine()));
        }
        public List<string> MedicamentosATomar { get => _medicamentosATomar; set => _medicamentosATomar = value; }
        public List<int> DosisMedicamento { get => _dosisMedicamento; set => _dosisMedicamento = value; }
    }

    class CrisisLaceraciones : Crisis
    {
        private readonly bool _poseeAntitetanica;
        private readonly int _cantidadPuntosTomados;

        public CrisisLaceraciones(string sexo, DateTime horaEntrada, DateTime horaSalida, bool poseeAntitetanica, int cantidadPuntosTomados) : base(sexo, horaEntrada, horaSalida)
        {
            this._poseeAntitetanica = poseeAntitetanica;
            this._cantidadPuntosTomados = cantidadPuntosTomados;
            this.agregarIndicacionesASeguir();
        }

        public override void ImprimirDatos()
        {
            base.ImprimirDatos();
            Console.WriteLine($"Posee la vacuna antitetánica : {(_poseeAntitetanica ? "Sí" : "No")}");
            Console.WriteLine($"Cantidad de puntos tomados : {_cantidadPuntosTomados}");
        }


    }

    class CrisisRespiratoria : Crisis
    {
        public CrisisRespiratoria(string sexo, DateTime horaEntrada, DateTime horaSalida) : base(sexo, horaEntrada, horaSalida)
        {
            agregarIndicacionesASeguir();
        }
    }

    class CrisisHipertension : Crisis
    {
        private readonly int cantidadLecturasTension;

        public CrisisHipertension(string sexo, DateTime horaEntrada, DateTime horaSalida, int cantidadLecturasTension) : base(sexo, horaEntrada, horaSalida)
        {
            this.cantidadLecturasTension = cantidadLecturasTension;
            agregarIndicacionesASeguir();
        }

        public override void ImprimirDatos()
        {
            base.ImprimirDatos();
            Console.WriteLine($"Lecturas de Tensión : {cantidadLecturasTension}");
        }
    }

}