using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STConsole2.Input
{
    internal static class Helper
    {
        private static readonly string AddValidErrorMessage = "[bold]Number must be positive and not 0.[/]";

        internal static bool YesNoPrompt()
        {
            var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>("Correct?")
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(true)
            .WithConverter(choice => choice ? "y" : "n"));

            return confirmation;
        }

        internal static int GetAmount()
        {
            var amount = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter reading from your glucose machine => ")
            .Validate((n) => n switch
            {
                <= 0 => ValidationResult.Error(AddValidErrorMessage),
                > 0 => ValidationResult.Success()
            }));

            return amount;
        }

        internal static DateOnly GetDate()
        {
            var added = AnsiConsole.Prompt(
                new TextPrompt<DateOnly>("Enter Date of the reading(MM/DD/YY) => "));

            return added;
        }
    }
}

