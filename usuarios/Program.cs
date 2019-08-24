using System;
using System.Reflection;

namespace usuarios
{
    // Clonable para clonar la DB
    class Usuario : ICloneable
    {
        public string id {get; set;}
        public string usuario {get; set;}
        public string clave {get; set;}
        public string nombres {get; set;}
        public string apellido_p {get; set;}
        public string apellido_m {get; set;}
        public int edad {get; set;}
        // Constructor
        public Usuario()
        {
            // Generar ID automaticamente al crear objeto
            id = System.Guid.NewGuid().ToString();
        }
        // Referencia de uso interno para localizar propiedades, usado para verificar y cambiar el valor de una propiedad (Creditos: https://stackoverflow.com/a/49133964)
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName)?.GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName)?.SetValue(this, value, null); }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
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
            for(int i = 0; i < database.GetLength(0); i++)
            {
                Usuario base_usuario = new Usuario();
                Console.WriteLine("|----------------------------------------|");
                BadUsuario:
                Console.Write("[#{0}] Ingrese el usuario: ", i);
                base_usuario.usuario = Console.ReadLine();
                if(BuscarUsuario(2, base_usuario.usuario, i) != -1)
                {
                    Console.WriteLine("ERROR: ESE USUARIO YA EXISTE.");
                    goto BadUsuario;
                }
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
                database[i] = base_usuario;
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
            Console.Write("\nIngrese cualquier tecla para continuar: ");
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
            // Referencia para salto de codigo
            BackMenu:
            Console.Title = "DB Usuarios > Inicio";
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
            Console.Clear();
            switch(opcion)
            {
                case 1:
                    BuscarUsuario();
                    goto BackMenu;
                case 2:
                    CrearUsuario();
                    goto BackMenu;
                case 3:
                    ModificarUsuario();
                    goto BackMenu;
                case 4:
                    BorrarUsuario();
                    goto BackMenu;
                case 5:
                    Environment.Exit(1);
                    break;
                default:
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
            Console.Clear();
            switch(opcion)
            {
                case 1:
                    if(BuscarPorID())
                        break;
                    else
                    {
                        Console.WriteLine("No se encontro resultados.");
                        goto BackMenu;
                    }
                case 2:
                    if(BuscarPorUsuario())
                        break;
                    else
                    {
                        Console.WriteLine("No se encontro resultados.");
                        goto BackMenu;
                    }
                case 3: break;
                default:
                    Console.WriteLine("Opción invalida.");
                    goto BackMenu;
            }
            // Funciones locales
            bool BuscarPorID()
            {
                Console.Write("Ingrese el ID: ");
                string id = Console.ReadLine();
                int i = BuscarUsuario(1, id);
                if(i == -1)
                    return false;
                Console.WriteLine("Usuario con ID '{0}' fue encontrado:", id);
                Console.WriteLine("|----------------------------------------------------|");
                Console.WriteLine("| ID         {0}", database[i].id);
                Console.WriteLine("| Usuario    {0}", database[i].usuario);
                Console.WriteLine("| Nombre     {0} {1} {2}", database[i].nombres, database[i].apellido_p, database[i].apellido_m);
                Console.WriteLine("| Edad       {0} años", database[i].edad);
                Console.WriteLine("|----------------------------------------------------|");
                return true;
            }
            bool BuscarPorUsuario()
            {
                Console.Write("Ingrese el usuario: ");
                string usuario = Console.ReadLine();
                int i = BuscarUsuario(2, usuario);
                if(i == -1)
                    return false;
                Console.WriteLine("Usuario '{0}' fue encontrado:", usuario);
                Console.WriteLine("|----------------------------------------------------|");
                Console.WriteLine("| ID         {0}", database[i].id);
                Console.WriteLine("| Usuario    {0}", database[i].usuario);
                Console.WriteLine("| Nombre     {0} {1} {2}", database[i].nombres, database[i].apellido_p, database[i].apellido_m);
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
        static int BuscarUsuario(int tipo, string dato, int len = -1)
        {
            if(len == -1)
                len = database.GetLength(0);
            for(int i = 0; i < len; i++) {
                if (
                    (tipo == 1 && database[i].id.ToLower().Contains(dato.ToLower()) == true) ||
                    (tipo == 2 && database[i].usuario.ToLower().Contains(dato.ToLower()) == true) ||
                    (tipo == 3 && database[i].id.ToLower() == dato.ToLower()) ||
                    (tipo == 4 && database[i].usuario.ToLower() == dato.ToLower())
                    )
                    return i;
            }
            return -1;
        }
		/*
		 * Procedimiento CrearUsuario()
         *
         * Crea un nuevo usuario hasta el final de la base de datos
         *
		*/
        static void CrearUsuario()
        {
            Console.Title = "DB Usuarios > Crear nuevo usuario";
            Usuario base_usuario = new Usuario();
            Console.WriteLine("|----------------------------------------|");
            BadUsuario:
            Console.Write("Ingrese el usuario: ");
            base_usuario.usuario = Console.ReadLine();
            if(BuscarUsuario(2, base_usuario.usuario) != -1)
            {
                Console.WriteLine("ERROR: ESE USUARIO YA EXISTE.");
                goto BadUsuario;
            }
            Console.Write("Ingrese la contraseña: ");
            base_usuario.clave = Console.ReadLine();
            Console.Write("Ingrese los nombres: ");
            base_usuario.nombres = Console.ReadLine();
            Console.Write("Ingrese el apellido paterno: ");
            base_usuario.apellido_p = Console.ReadLine();
            Console.Write("Ingrese el apellido materno: ");
            base_usuario.apellido_m = Console.ReadLine();
            Console.Write("Ingrese la edad: ");
            base_usuario.edad = int.Parse(Console.ReadLine());
            // Guarda la cantidad de usuarios registrados
            int len = database.GetLength(0);
            // Respalda la base de datos
            Usuario[] backup = new Usuario[len+1];
            for(int i = 0; i < len; i++)
                backup[i] = (Usuario)database[i].Clone();
            // Genera de nuevo la variable database con un nuevo tamaño
            database = new Usuario[len+1];
            // Guarda la base de datos de acuerdo al respaldo
            for(int i = 0; i < len; i++)
                database[i] = (Usuario)backup[i].Clone();
            // Agrega al final el nuevo usuario
            database[len] = base_usuario;
            Console.WriteLine("\n Usuario '{0}' se ha generado con el ID: {1}", base_usuario.usuario, base_usuario.id);
            Console.WriteLine("|----------------------------------------|");
            Continuar(true);
        }
		/*
		 * Procedimiento ModificarUsuario()
         *
         * Modifica los datos de un usuario
         *
		*/
        static void ModificarUsuario()
        {
            Console.Title = "DB Usuarios > Crear modificar usuario";
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
            int i = 0;
            Console.Clear();
            switch(opcion)
            {
                case 1:
                    Console.Write("Ingrese el id: ");
                    string id = Console.ReadLine();
                    i = BuscarUsuario(1, id);
                    if(i != -1)
                        break;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No se encontro resultados.");
                        goto BackMenu;
                    }
                case 2:
                    Console.Write("Ingrese el usuario: ");
                    string usuario = Console.ReadLine();
                    i = BuscarUsuario(2, usuario);
                    if(i != -1)
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

            Console.WriteLine("USUARIO ENCONTRADO!");
            BadDato:
            Console.Write("Ingrese el dato a modificar: ");
            string dato = Console.ReadLine();
            if(!string.IsNullOrEmpty((string)database[i][dato]))
            {
                string antes = (string)database[i][dato];
                Console.WriteLine("Ingrese el nuevo valor del dato {0} (original '{1}'): ", dato, antes);
                string despues = Console.ReadLine();
                database[i][dato] = despues;
                Console.WriteLine("Cambio realizado del dato {0}:\n{1} >>> {2}", dato, antes, despues);
            }
            else
            {
                Console.WriteLine("ERROR: DATO DE DB DE USUARIO INCORRECTO!");
                goto BadDato;
            }
            Continuar(true);
        }
		/*
		 * Procedimiento BorrarUsuario()
         *
         * Borra un usuario de la base de datos.
         *
		*/
        static void BorrarUsuario()
        {
            Console.Title = "DB Usuarios > Borrar usuario";
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
            int i = 0;
            Console.Clear();
            switch(opcion)
            {
                case 1:
                    Console.Write("Ingrese el id: ");
                    string id = Console.ReadLine();
                    i = BuscarUsuario(1, id);
                    if(i != -1)
                        break;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No se encontro resultados.");
                        goto BackMenu;
                    }
                case 2:
                    Console.Write("Ingrese el usuario: ");
                    string usuario = Console.ReadLine();
                    i = BuscarUsuario(2, usuario);
                    if(i != -1)
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

            Console.Write("¿Esta seguro que desea borrar al usuario {0}? (Si - No)", database[i].usuario);
            if(Console.ReadLine().ToLower() == "si") {
                // Guarda la cantidad de usuarios registrados
                int len = database.GetLength(0);
                // Respalda la base de datos
                Usuario[] backup = new Usuario[len];
                for(int z = 0; z < len; z++)
                    backup[z] = (Usuario)database[z].Clone();
                // Genera de nuevo la variable database con un nuevo tamaño
                database = new Usuario[len-1];
                // Guarda la base de datos de acuerdo al respaldo e ignorando el eliminado
                for(int z = 0; z < len; z++)
                    if(z != i)
                        database[z] = (Usuario)backup[z].Clone();
                Console.WriteLine("Usuario Eliminado!");
            }
            Continuar(true);
        }
    }
}
