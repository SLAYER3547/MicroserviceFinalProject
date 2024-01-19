//using M.Domain.Entities.Concrete;


using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.ExampleCQRS.Application.Commands.Books
{
    public record AddOrEditBookCommand(Book model) : IRequest<Book>;

}
