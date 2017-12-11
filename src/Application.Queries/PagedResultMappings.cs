namespace PetProjects.MicroTransactionsApi.Application.Queries
{
    using System;
    using System.Linq;
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Infrastructure.CrossCutting.Paging;

    public static class PagedResultMappings
    {
        public static PagedResultDto<TDto> ToPagedResultDto<TModel, TDto>(this PagedResult<TModel> pagedResult, Func<TModel, TDto> mapFunc)
        {
            return new PagedResultDto<TDto>
            {
                NextPageToken = pagedResult.NextPageToken,
                Result = pagedResult.Result.Select(mapFunc),
                LastPage = pagedResult.LastPage
            };
        }
    }
}
