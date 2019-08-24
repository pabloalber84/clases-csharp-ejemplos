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
        public Usuario()
        {
            // Generar ID automaticamente al crear objeto
            id = System.Guid.NewGuid().ToString();
        }
    }
    class Program
    {
        static Usuario[] database;
        static void Main(string[] args)
        {
            Console.Title = "DB Usuarios > Bienvenida";
            Console.WriteLine("Bienvenido a ejemplo de base de datos estatica usando clases.");
            Console.WriteLine("¿Cuantos usuarios tendra su base de datos?");
            int size = int.Parse(Console.ReadLine());
            database = new Usuario[size];
            IngresarDatosDB();
            MenuOpciones();
        }
		/*
		 * Procedimiento IngresarDatosDB()
         *
         * Ingresa los datos de la cantidad de usuarios a ingresar
         *
		*/
        static void IngresarDatosDB()
        {
            Console.Title = "DB Usuarios > Generar Usuarios";
            for(int i = 0; i < database.Length; i++)
            {
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
         * Procedimiento Continuar(bool del)
         *
         * Pide que ingrese al usuario que ingrese cualquier tecla para continuar, en caso que
         * se requiera que el usuario alcance a ver algo importante antes de poder continuar con
         * el proceso.
         *
         * PARAMETROS:
         *            bool del:    Es opcional, si se pasa 'true' como parametro, borrara el log
         *                         de la consola despues de oprimir cualquier boton.
         *
        */
        static void Continuar(bool del = false)
        {
            Console.Write("Ingrese cualquier tecla para continuar: ");
            Console.ReadKey();
            if(del)
                Console.Clear();
        }
		/*
		 * Procedimiento MenuOpciones()
         *
         * Muestra las opciones que tiene para manejar la base de datos de los usuarios.
         *
		*/
        static void MenuOpciones()
        {
            Console.Title = "DB Usuarios > Inicio";
            // Referencia para salto de codigo
            BackMenu:
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("|-          MENU DE OPCIONES          -|");
            Console.WriteLine("|-         Ingrese una opción         -|");
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("| 1.- Buscar Usuario                  -|");
            Console.WriteLine("| 2.- Crear usuario                   -|");
            Console.WriteLine("| 3.- Modificar Usuario               -|");
            Console.WriteLine("| 4.- Borrar usuario                  -|");
            Console.WriteLine("| 5.- Salir                           -|");
            Console.WriteLine("|--------------------------------------|");
            int opcion = int.Parse(Console.ReadLine());
            switch(opcion)
            {
                case 1:
                    BuscarUsuario();
                    goto BackMenu;
                case 2:
                    goto BackMenu;
                case 3:
                    goto BackMenu;
                case 4:
                    goto BackMenu;
                case 5:
                    Environment.Exit(1);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opción invalida.");
                    goto BackMenu;
            }
        }
		/*
		 * Procedimiento BuscarUsuario()
         *
         * Busca un usuario para modificar
         *
		*/
        static void BuscarUsuario()
        {
            Console.Title = "Busqueda de usuarios";
            // Referencia para salto de codigo
            BackMenu:
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("|-          MENU DE OPCIONES          -|");
            Console.WriteLine("|-    Ingrese el tipo de búsqueda:    -|");
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("| 1.- Buscar por ID                   -|");
            Console.WriteLine("| 2.- Buscar por Usuario              -|");
            Console.WriteLine("| 3.- Regresar                        -|");
            Console.WriteLine("|--------------------------------------|");
            int opcion = int.Parse(Console.ReadLine());
            switch(opcion)
            {
                case 1:
                    if(BuscarPorID())
                        break;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No se encontro resultados.");
                        goto BackMenu;
                    }
                case 2:
                    if(BuscarPorUsuario())
                        break;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No se encontro resultados.");
                        goto BackMenu;
                    }
                case 3: break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opción invalida.");
                    goto BackMenu;
            }
            // Funciones locales
            bool BuscarPorID()
            {
                Console.Clear();
                Console.Write("Ingrese el ID: ");
                string id = Console.ReadLine();
                int i = BuscarUsuario(1, id);
                if(i == -1)
                    return false;
                Console.WriteLine("Usuario con ID '{0}' fue encontrado:", id);
                Console.WriteLine("|----------------------------------------------------|");
                Console.WriteLine("| ID         {0}", database[i].id);
                Console.WriteLine("| Usuario    {0}", database[i].usuario);
                Console.WriteLine("| Nombre     {0} {1} {3}", database[i].usuario, database[i].nombres, database[i].apellido_p, database[i].apellido_m);
                Console.WriteLine("| Edad       {0} años", database[i].edad);
                Console.WriteLine("|----------------------------------------------------|");
                return true;
            }
            bool BuscarPorUsuario()
            {
                Console.Clear();
                Console.Write("Ingrese el usuario: ");
                string usuario = Console.ReadLine();
                int i = BuscarUsuario(2, usuario);
                if(i == -1)
                    return false;
                Console.WriteLine("Usuario '{0}' fue encontrado:", usuario);
                Console.WriteLine("|----------------------------------------------------|");
                Console.WriteLine("| ID         {0}", database[i].id);
                Console.WriteLine("| Usuario    {0}", database[i].usuario);
                Console.WriteLine("| Nombre     {0} {1} {3}", database[i].usuario, database[i].nombres, database[i].apellido_p, database[i].apellido_m);
                Console.WriteLine("| Edad       {0} años", database[i].edad);
                Console.WriteLine("|----------------------------------------------------|");
                return true;
            }
            Continuar(true);
        }
        /*
         * Función int BuscarUsuario(int tipo, string dato)
         *
         * Busca el usuario y retornar el index/indice de la base de datos.
         *
         * PARAMETROS:
         *            int tipo:    Tipo de busqueda:
         *                              (1.- Por ID)
         *                              (2.- Por nombre de usuario)
         *            string dato:    El dato a buscar.
         *
        */
        static int BuscarUsuario(int tipo, string dato)
        {
            for(int i = 0; i < database.Length; i++)
            {
                if ((tipo == 1 && database[i].id.ToLower().Contains(dato.ToLower()) == true) ||
                    (tipo == 2 && database[i].usuario.ToLower().Contains(dato.ToLower()) == true))
                    return i;
            }
            return -1;
        }
    }
}
