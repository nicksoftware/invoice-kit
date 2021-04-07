using System.Collections.Generic;
using System;
using Spectre.Console;

namespace InvoiceKit.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                DisplayHeading();

                var userOption = MenuPrompt();
                if (userOption == MenuOptions.ExitProgram && AnsiConsole.Confirm("Are you sure you want to exit?"))
                    break;

                ProcessMenuOption(userOption);

                Console.ReadKey();
            } while (true);

        }
        private static string MenuPrompt()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green] What do you want to do with the program [/]?")
                    .AddChoice(MenuOptions.CreateInvoice)
                    .AddChoices(new[] {
                    MenuOptions.Settings,
                    MenuOptions.ExitProgram
                    }));

            return option;
        }

        public static void DisplayHeading()
        {
            var rollDice = new Random();

            AnsiConsole.Render(
                new FigletText("Invoice Kit Cli")
                    .LeftAligned()
                    .Color((Color)rollDice.Next(0, 80)));

            // AnsiConsole.Markup("[underline green][/] ");
            // AnsiConsole.MarkupLine("[bold]Nicolas Maluleke[/] ");



            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine();
        }

        private static void ProcessMenuOption(string userOption)
        {
            var executionsOptions = new Dictionary<string, Action>();

            executionsOptions.Add(MenuOptions.CreateInvoice, () => new InvoiceCreator().Start());
            executionsOptions.Add(MenuOptions.Settings, () => DoNothing());
            executionsOptions.Add(MenuOptions.ExitProgram, () => DoNothing());

            executionsOptions[userOption]();
        }
        private static void DoNothing()
        {
        }
    }
}
