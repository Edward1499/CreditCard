using Application.Queries;
using Application.Responses;
using Core.Enumerations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Handlers
{
    public class GetBankListQueryHandler : IRequestHandler<GetBankListQuery, List<BankResponse>>
    {
        public async Task<List<BankResponse>> Handle(GetBankListQuery request, CancellationToken cancellationToken)
        {
            var list = new List<BankResponse>();
            foreach (var name in Enum.GetNames(typeof(Banks)))
            {
                list.Add(new BankResponse
                {
                    Key = (int)Enum.Parse(typeof(Banks), name),
                    Value = name
                });
            }

            return list;
        }
    }
}
