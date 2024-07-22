using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ErrorAdapters;

namespace Peacious.Framework.Results.ErrorFactories;

public interface IErrorActionResultAdapterFactory
{
    IErrorActionResultAdapter GetErrorActionResultAdapter(ErrorResponseType responseType);
}
