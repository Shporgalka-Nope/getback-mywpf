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

    [Option('m', "manufacturer", Required = true,
        HelpText = "Name of the manufacturer model after scaffold without extension. For example \"Manufacturer\"")]
    public string ManufacturesTableName { get; set; }

    [Option('s', "supplier", Required = true,
        HelpText = "Name of the supplier model after scaffold without extension. For example \"Supplier\"")]
    public string SuppliersTableName { get; set; }
}