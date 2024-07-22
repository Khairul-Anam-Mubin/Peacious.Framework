using Microsoft.AspNetCore.Mvc;

namespace Peacious.Framework.Results.ErrorAdapters;

public interface IErrorActionResultAdapter
{
    IActionResult Convert(Error error);
}