using GcdCalculator.Models;
using GcdCalculator.Services.StaticClasses;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

#pragma warning disable CS8602
#pragma warning disable CS8604
#pragma warning disable CS8629

namespace GcdCalculator.Controllers
{
    /// <summary>
    /// Class HomeController
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class
        /// </summary>
        public HomeController()
        {
        }

        /// <summary>
        /// Calculates a greatest commn divisor
        /// </summary>
        /// <param name="vdata">The ResultViewModel</param>
        /// <returns>Returns a representation of the result model</returns>
        public IActionResult Index(ResultViewModel vdata, string submit)
        {
            if (submit != "Calculate")
                return View(vdata);

            try
            {
                if (!this.CheckRequiredParams(vdata))
                    return View(vdata);

                this.GcdCalculate(vdata);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                vdata.Error = $"Error: {ex.Message}";
            }
            catch (ArgumentException ex)
            {
                vdata.Error = $"Error: {ex.Message}";
            }

            return View(vdata);
        }

        /// <summary>
        /// Represents handling an unexpected error
        /// </summary>
        /// <returns>Returns a representation of the error model</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool CheckRequiredParams(ResultViewModel model) =>
            this.CheckField(model, model.CountNumbers, "The count of numbers is not selected.") &&
            this.CheckField(model, model.First, "First") &&
            this.CheckField(model, model.Second, "Second") &&
            this.CheckField(model, model.Algorithm, "No algorithm selected.");

        private bool CheckField(ResultViewModel model, string field, string? error)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                model.Error = "Error: " + error ?? "Integer's field is not filled in.";
                return false;
            }

            return true;
        }

        private bool CheckField(ResultViewModel model, int? field, string fieldName)
        {
            if (!field.HasValue)
            {
                model.Error = @$"Error: The value entered in the ""{fieldName}"" field is not an integer in the allowed range.";
                return false;
            }

            return true;
        }

        private void GcdCalculate(ResultViewModel model) =>
        (
            model switch
            {
                { Extended: "on", CountNumbers: "2", Algorithm: "Euclidean" } => () =>
                    (model.Result, model.CalculationTime) =
                        (GcdAlgorithms.GetGcdByEuclidean(out long ms, model.First.Value, model.Second.Value), ms),
                { Extended: "on", CountNumbers: "2", Algorithm: "Stein" } => () =>
                    (model.Result, model.CalculationTime) =
                        (GcdAlgorithms.GetGcdByStein(out long ms, model.First.Value, model.Second.Value), ms),
                { Extended: "on", CountNumbers: "3", Algorithm: "Euclidean" } when this.CheckField(model, model.Third, "Third") => () =>
                    (model.Result, model.CalculationTime) =
                        (GcdAlgorithms.GetGcdByEuclidean(out long ms, model.First.Value, model.Second.Value, model.Third.Value), ms),
                { Extended: "on", CountNumbers: "3", Algorithm: "Stein" } when this.CheckField(model, model.Third, "Third") => () =>
                    (model.Result, model.CalculationTime) =
                        (GcdAlgorithms.GetGcdByStein(out long ms, model.First.Value, model.Second.Value, model.Third.Value), ms),
                { Extended: "on", CountNumbers: "4", Algorithm: "Euclidean" } 
                    when this.CheckField(model, model.NumbersOther, null) && this.GetNumberList(model) is var numberList && numberList.Any() => () =>
                        (model.Result, model.CalculationTime) =
                            (GcdAlgorithms.GetGcdByEuclidean(out long ms, model.First.Value, model.Second.Value, numberList.ToArray()), ms),
                { Extended: "on", CountNumbers: "4", Algorithm: "Stein" } 
                    when this.CheckField(model, model.NumbersOther, null) && this.GetNumberList(model) is var numberList && numberList.Any() => () =>
                        (model.Result, model.CalculationTime) = 
                            (GcdAlgorithms.GetGcdByStein(out long ms, model.First.Value, model.Second.Value, numberList.ToArray()), ms),
                { Extended: null, CountNumbers: "2", Algorithm: "Euclidean" } => () =>
                    model.Result = GcdAlgorithms.GetGcdByEuclidean(model.First.Value, model.Second.Value),
                { Extended: null, CountNumbers: "2", Algorithm: "Stein" } => () =>
                    model.Result = GcdAlgorithms.GetGcdByStein(model.First.Value, model.Second.Value),
                { Extended: null, CountNumbers: "3", Algorithm: "Euclidean" } when this.CheckField(model, model.Third, "Third") => () =>
                    model.Result = GcdAlgorithms.GetGcdByEuclidean(model.First.Value, model.Second.Value, model.Third.Value),
                { Extended: null, CountNumbers: "3", Algorithm: "Stein" } when this.CheckField(model, model.Third, "Third") => () =>
                    model.Result = GcdAlgorithms.GetGcdByStein(model.First.Value, model.Second.Value, model.Third.Value),
                { Extended: null, CountNumbers: "4", Algorithm: "Euclidean" }
                    when this.CheckField(model, model.NumbersOther, null) && this.GetNumberList(model) is var numberList && numberList.Any() => () =>
                        model.Result = GcdAlgorithms.GetGcdByEuclidean(model.First.Value, model.Second.Value, numberList.ToArray()),
                { Extended: null, CountNumbers: "4", Algorithm: "Stein" }
                    when this.CheckField(model, model.NumbersOther, null) && this.GetNumberList(model) is var numberList && numberList.Any() => () =>
                        model.Result = GcdAlgorithms.GetGcdByStein(model.First.Value, model.Second.Value, numberList.ToArray()),
                _ => new Action(() => { })
            }
        )();

        private IEnumerable<int> GetNumberList(ResultViewModel model)
        {
            var numbers = model.NumbersOther.Split(new[] { "\n", "\r\n", " " }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var count = numbers.Length;
            var numberList = new List<int>(capacity: count);
            int number;
            for (int i = 0; i < count; i++)
            {
                if (int.TryParse(numbers[i], out number))
                {
                    numberList.Add(number);
                }
                else
                {
                    model.Error = $"Error: The entered value {numbers[i]} is not an integer in the allowed range.";
                    return Enumerable.Empty<int>();
                }
            }

            return numberList;
        }        
    }
}