using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        //public IBaseRepository<Author> Authors { get; private set; }
        //public IBooksRepository Books { get; private set; }

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }
       
        public IBaseRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IBaseRepository<T>;
            }

            IBaseRepository<T> repo = new BaseRepository<T>(_context);
            Repositories.Add(typeof(T), repo);
            return repo;

        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            //Authors = new BaseRepository<Author>(_context);
            //Books = new BooksRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}