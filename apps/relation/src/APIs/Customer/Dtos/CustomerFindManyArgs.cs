using Microsoft.AspNetCore.Mvc;
using Relation.APIs.Common;
using Relation.Infrastructure.Models;

namespace Relation.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
