using Domain.Dto.Common;

namespace Application.Common.Models
{
    internal static class ApplicationFactory
    {
        #region Internal Methods

        internal static ResponseDetail<T> CreateResponseDetails<T>()
        {
            return new ResponseDetail<T>();
        }
        internal static ResponseDetail<PaginatedResult<T>> CreatePaginationResponseDetails<T>()
        {
            return new ResponseDetail<PaginatedResult<T>>();
        }

        #endregion Internal Methods
    }
}
