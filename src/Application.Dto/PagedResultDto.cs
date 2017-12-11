namespace PetProjects.MicroTransactionsApi.Application.Dto
{
    using System.Collections.Generic;

    public class PagedResultDto<T>
    {
        public string NextPageToken { get; set; }

        public IEnumerable<T> Result { get; set; }

        public bool LastPage { get; set; }
    }
}