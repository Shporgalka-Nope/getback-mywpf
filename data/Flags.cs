using CommandLine;

public class Flags
{
    [Option('u', "user", Required = true, 
        HelpText = "Name of the user model after scaffold without extension. For example \"User\"")]
    public string UsersTableName { get; set; }

    [Option('p', "product", Required = true, 
        HelpText = "Name of the products model after scaffold without extension. For example \"Product\"")]
    public string ProductsTableName {  get; set; }

    [Option('o', "order", Required = true,
        HelpText = "Name of the orders model after scaffold without extension. For example \"Order\"")]
    public string OrdersTableName { get; set; }

    [Option('c', "context", Required = true,
        HelpText = "Name of DBContext class after scaffold. For example \"ReAA_DBContext\"")]
    public string ContextName { get; set; }
}