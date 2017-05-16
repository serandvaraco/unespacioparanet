using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacRepositoriesContainer.Models
{
    public class uowRep : IuowRep
    {
        private IContextModel _context;
        public uowRep(IContextModel context)
        {
            _context = context;
        }

        private ProjectRepository _project;

        public ProjectRepository Project
        {
            get { return _project ?? new ProjectRepository(_context); }
        }

    }
}