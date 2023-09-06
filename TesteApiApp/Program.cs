using TesteApiApp.Service;

namespace TesteApiApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process started");
            Task.Run(async () =>
            {
                try
                {
                    await ListUsers();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro!");
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }).Wait();
            Console.WriteLine("Process Ended");

        }

        private static async Task ListUsers()
        {

            using  UserService userService = new UserService();
            var users = await userService.GetUsers();

            foreach (var user in users)
            {
                Console.WriteLine("Name:\t"+user.name);
                var todos = await userService.GetUserTodo(user.id);
                Console.WriteLine("Todos:");
                foreach (var todo in todos)
                {
                    if (!todo.completed)
                        Console.WriteLine("\t" + todo.title);
                }
                Console.WriteLine();
            }


        }
    }
}