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
        static void Main(string[] args)
        {
            Console.WriteLine("Hola mundo!");
        }
    }
}
