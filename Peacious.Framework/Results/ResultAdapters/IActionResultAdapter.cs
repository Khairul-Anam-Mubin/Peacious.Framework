using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.ErrorAdapters;

namespace Peacious.Framework.Results.ResultAdapters;

public interface IActionResultAdapter
{
    IActionResult Convert(IResult result, IErrorActionResultAdapter visitor);
}