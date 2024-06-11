using Library.Application.ViewModel;
using MediatR;

namespace Library.Application.Queries.GetUser
{
    public class GetUsersQuery : IRequest<List<UserViewModel>>
    {
        public GetUsersQuery(string query) => Query = query;

        public string Query { get; set; }
    }
}
