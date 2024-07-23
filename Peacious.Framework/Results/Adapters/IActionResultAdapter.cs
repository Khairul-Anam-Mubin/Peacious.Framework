using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors.Adapters;

namespace Peacious.Framework.Results.Adapters;

/// <summary>
/// Converts a result object to IActionResult
/// </summary>
public interface IActionResultAdapter
{
    /// <param name="visitor">
    /// ErrorActionResultAdapter used to convert the error result
    /// </param>
    /// <returns>
    /// Converted IActionResult object
    /// </returns>
    IActionResult Convert(IResult result, IErrorActionResultAdapter visitor);
}