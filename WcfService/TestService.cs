using Business.Interfaces;
using Business.Services;
using Model.Models;
using System;
using System.Collections.Generic;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TestService" in both code and config file together.
    public partial class TestService : ITestService, IBlogWcfService
    {
        public void DoWork()
        {
        }

        IBlogService blogService = new BlogService();

        public TestService() { }

        public TestService(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public List<Blog> GetAllBlogs()
        {
            return blogService.GetAllBlogs();
        }

        public Blog GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }

   
}