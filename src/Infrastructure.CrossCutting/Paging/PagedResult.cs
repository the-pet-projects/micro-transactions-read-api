namespace PetProjects.MicroTransactionsApi.Infrastructure.CrossCutting.Paging
{
    using System.Collections.Generic;
    using System.Text;

    public class PagedResult<T>
    {
        public PagedResult(byte[] pagingState, IEnumerable<T> result)
        {
            this.NextPageToken = pagingState == null ? string.Empty : pagingState.ByteArrayToString();
            this.Result = result;
            this.LastPage = pagingState == null;
        }

        public PagedResult()
        {
        }

        public string NextPageToken { get; set; }

        public IEnumerable<T> Result { get; set; }

        public bool LastPage { get; set; }
    }
}