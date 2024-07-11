using Library.Application.ViewModel;
using MediatR;

namespace Library.Application.Queries.GetLoans
{
    public class GetLoansQuery : IRequest<IReadOnlyCollection<LoanViewModel>> 
    {
        public GetLoansQuery(int take, int skip) 
        {
            Take = take;
            Skip = skip;
        }
        public int Take { get; private set; } = 5;
        public int Skip { get; private set; } = 0;
    }
}
