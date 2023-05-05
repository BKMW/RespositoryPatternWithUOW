using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : class;

        //IBaseRepository<Author> Authors { get; }
        //IBooksRepository Books { get; }

        int Complete();
    }
}