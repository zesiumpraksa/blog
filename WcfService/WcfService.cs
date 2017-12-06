using Business.Interfaces;
using Business.Services;
using Model.Models;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TestService" in both code and config file together.
    public partial class WcfService : IWcfService
    {
        IBlogService blogService;
        IAutorService authorService;

        public WcfService() { }

        public WcfService(IBlogService blogService,IAutorService authorService)
        {
            this.blogService = blogService;
            this.authorService = authorService;
        }
        

        public void DoWork()
        {
            throw new NotImplementedException();
        }
    }

}