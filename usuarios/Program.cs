using System;

namespace usuarios
{
    class Usuario
    {
        public string id;
        public string usuario;
        public string clave;
        public string nombres;
        public string apellido_p;
        public string apellido_m;
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
            IngresarDatosDB();
        }
		/*
		 * Procedimiento IngresarDatosDB: Ingresa los datos de la cantidad de usuarios a ingresar
		*/
        static void IngresarDatosDB() {
            for(int i = 0; i < database.Length; i++) {
                Usuario base_usuario = new Usuario();
                Console.WriteLine("|----------------------------------------|");
                Console.Write("[#{0}] Ingrese el usuario: ", i);
                base_usuario.usuario = Console.ReadLine();
                Console.Write("[#{0}] Ingrese la contraseña: ", i);
                base_usuario.clave = Console.ReadLine();
                Console.Write("[#{0}] Ingrese los nombres: ", i);
                base_usuario.nombres = Console.ReadLine();
                Console.Write("[#{0}] Ingrese el apellido paterno: ", i);
                base_usuario.apellido_p = Console.ReadLine();
                Console.Write("[#{0}] Ingrese el apellido materno: ", i);
                base_usuario.apellido_m = Console.ReadLine();
                Console.Write("[#{0}] Ingrese la edad: ", i);
                base_usuario.edad = int.Parse(Console.ReadLine());
                Console.WriteLine("\n Usuario [#{0}] '{1}' se ha generado con el ID: {2}", i, base_usuario.usuario, base_usuario.id);
                Console.WriteLine("|----------------------------------------|");
            }
            Continuar(true);
        }
        /*
         * Funcion Continuar(bool del): Pide que ingrese al usuario que ingrese cualquier tecla para continuar,
         *                              en caso que se requiera que el usuario alcance a ver algo importante
         *                              antes de poder continuar con el proceso.
         *                  PARAMETROS:
         *                         bool del:    Es opcional, si se pasa 'true' como parametro, borrara el log
         *                                      de la consola despues de oprimir cualquier boton.
        */
        static void Continuar(bool del = false) {
            Console.Write("Ingrese cualquier tecla para continuar: ");
            Console.ReadKey();
            if(del)
                Console.Clear();
        }
    }
}
