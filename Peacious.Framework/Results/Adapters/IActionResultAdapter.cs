using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors.Adapters;

namespace Peacious.Framework.Results.Adapters;

public interface IActionResultAdapter
{
    IActionResult Convert(IResult result, IErrorActionResultAdapter visitor);
}