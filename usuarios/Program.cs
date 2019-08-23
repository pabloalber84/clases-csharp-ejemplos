using System;

namespace usuarios
{
    class Usuario
    {
        public string id;
        public string usuario;
        public string clave;
        public string nombres;
        public string apellido_m;
        public string apellido_p;
        public int edad;
        // Constructor
        public Usuario() {
            // Generar ID automaticamente al crear objeto
            id = System.Guid.NewGuid().ToString();
        }
    }
    class Program
    {
        static Usuario[] database;
        static void Main(string[] args)
        {
            Console.Title = "Base de datos estatica de Usuarios";
            Console.WriteLine("Bienvenido a ejemplo de base de datos estatica usando clases.");
            Console.WriteLine("¿Cuantos usuarios tendra su base de datos?");
            int size = int.Parse(Console.ReadLine());
            database = new Usuario[size];
        }
    }
}
