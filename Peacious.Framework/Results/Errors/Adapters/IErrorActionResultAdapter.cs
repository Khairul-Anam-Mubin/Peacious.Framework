using Microsoft.AspNetCore.Mvc;

namespace Peacious.Framework.Results.Errors.Adapters;

public interface IErrorActionResultAdapter
{
    IActionResult Convert(Error error);
}