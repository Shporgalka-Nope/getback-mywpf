using CommandLine;
using System.IO;
using System.Runtime.CompilerServices;

public class Flags
{
    [Option('u', "user", Required = false, 
        HelpText = "Name of the user model after scaffold without extension. For example \"User\"")]
    public string UsersTableName { get; set; }

    [Option('p', "product", Required = false, 
        HelpText = "Name of the products model after scaffold without extension. For example \"Product\"")]
    public string ProductsTableName {  get; set; }

    [Option('o', "order", Required = false,
        HelpText = "Name of the orders model after scaffold without extension. For example \"Order\"")]
    public string OrdersTableName { get; set; }

    [Option('c', "context", Required = false,
        HelpText = "Name of DBContext class after scaffold. For example \"ReAA_DBContext\"")]
    public string ContextName { get; set; }
    
    public bool GetOneC { get; set; }

    [Option("get-db", Required = false,
        HelpText = "Выполнить команду scaffold-dbcontext.")]
    public bool GetDatabase { get; set; }
}