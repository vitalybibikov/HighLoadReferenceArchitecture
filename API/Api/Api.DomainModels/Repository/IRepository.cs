using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.DomainModels.Repository
{
    public interface IRepository<T>
    {
        Task CreateAsync(T c);

        Task UpdateAsync(T c);
    }
}
