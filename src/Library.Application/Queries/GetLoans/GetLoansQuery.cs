using Library.Application.ViewModel;
using MediatR;

namespace Library.Application.Queries.GetLoans
{
    public class GetLoansQuery : IRequest<List<LoanViewModel>> 
    {
        public GetLoansQuery() { }
    }
}
