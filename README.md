# Getback-mywpf

### ! In development !

A CLI tool designed for fast and (not so) reliable WPF project scaffolding.
This one scaffolds a WPF demo-exam template, basing itself on the models' classes that you provide.

This project depends on 2 other Nuget packages:

[CommandLineParser](https://www.nuget.org/packages/commandlineparser) - For parsing command line arguments.

[Scriban](https://scriban.github.io/) - For scaffolding templates.

## Installation

### Nuget package

As project is not finished, Nuget package is not available

### Git cloning

1. Clone the repository via `Git clone https://github.com/Shporgalka-Nope/getback-mywpf.git`
2. Run `dotnet restore` to download Nuget dependencies
3. Run `dotnet pack -c Release` to build the project.
4. Lastly, run `dotnet tool install -g --add-source ./bin/Release getback-mywpf` to install the project

## Usage

To use the tool, run following command in the directory with `.csproj` file of your WPF project `getback-mywpf -u User -o Order -p Product -c DbContext`

### Arguments

| Argument  | Alternative | Required? | Meaning                                         |
| --------- | ----------- | --------- | ----------------------------------------------- |
| --version |             | No        | Display tool's version                          |
| --help    |             | No        | Display tool's documentation                    |
| -u        | -user       | Yes       | Classname of User model used for authentication |
| -o        | -order      | Yes       | Classname of Order model                        |
| -p        | -product    | Yes       | Classname of Product model                      |
| -c        | -context    | Yes       | Classname of your context class                 |

## How it works?

The tool contains templates (.sbn) for `.xaml` and corresponding `.xaml.cs` files in the `templates` directory. It will create `./Windows/` directory in your project and copy all the templates inside, changing some of the values to those, that you have provided.
Be aware that this tool will look for `.csproj` file, to read project's namespace.

For more information about templates, please read [Scriban's documentation](https://scriban.github.io/docs/getting-started/)
