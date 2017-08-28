using AlbumsSharing.Model.Entities;
using AlbumsSharing.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumsSharing.Model
{
    public class UnitOfWork : IUnitOfWork
    {
        AlbumsContext _albumsContext;
        public UnitOfWork(AlbumsContext albums )
        {
            _albumsContext = albums; 
        }

        public void Commit() => _albumsContext.SaveChanges();
        private IAlbumsRepository _albums;
        public IAlbumsRepository Albums 
            => _albums ?? (_albums = new AlbumsRepository(_albumsContext)); 
    }
}
