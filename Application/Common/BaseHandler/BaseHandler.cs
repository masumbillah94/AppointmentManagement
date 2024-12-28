using AutoMapper;
using Domain.Abstractions.Base;

namespace Application.Common.BaseHandler
{
    public abstract class BaseHandler
    {
        #region Protected Fields

        protected readonly IRepositoryFacade _repositoryFacade;
        protected readonly IMapper _mapper;

        #endregion Protected Fields

        #region Protected Constructors

        protected BaseHandler(IRepositoryFacade repositoryFacade, IMapper mapper)
        {
            _repositoryFacade = repositoryFacade;
            _mapper = mapper;
        }

        #endregion Protected Constructors
    }
}
