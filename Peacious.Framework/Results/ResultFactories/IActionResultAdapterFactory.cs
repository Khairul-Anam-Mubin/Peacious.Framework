using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ResultAdapters;

namespace Peacious.Framework.Results.ResultFactories;

public interface IActionResultAdapterFactory
{
    IActionResultAdapter GetActionResultAdapter(ResponseType responseType);
}
