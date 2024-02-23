using Microsoft.AspNetCore.Mvc;
using MultiDataSourceExample.DataBase;
using MultiDataSourceExample.Entity;
using MultiDataSourceExample.Repository;
using MultiDataSourceExample.Web.Models;
using System.Diagnostics;

namespace MultiDataSourceExample.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork<AContext> unitOfWorkA, IUnitOfWork<BContext> unitOfWorkB)
        {
            unitOfWorkA.BeginTran();//A数据库开始事务
            unitOfWorkA.Repository<EntityA>().RemoveRangeByPks(new object[] { 6, 7 });//A数据库批量删除
            unitOfWorkA.Repository<EntityA>().Add(new EntityA() { Id = 6, Name = "nnnn", CreatedAt = DateTime.Now });
            unitOfWorkA.Repository<EntityA>().Add(new EntityA() { Id = 7, Name = "nnnn7", CreatedAt = DateTime.Now });
            unitOfWorkA.Commit();

            unitOfWorkB.BeginTran(); //B数据库开始事务
            unitOfWorkB.Repository<EntityB>().RemoveRangeByPks(new object[] { 16, 27 }); //B数据库批量删除
            unitOfWorkB.Repository<EntityB>().Add(new EntityB() { Id = 16, Name = "nnnn", CreatedAt = DateTime.Now });
            unitOfWorkB.Repository<EntityB>().Add(new EntityB() { Id = 27, Name = "nnnn7", CreatedAt = DateTime.Now });
            unitOfWorkB.Commit();

            var adata = unitOfWorkA.Repository<EntityA>().QueryList(c => c.Id > 1);//A数据库查询结果
            var bdata = unitOfWorkB.Repository<EntityB>().QueryList(c => c.Id > 1);//B数据库查询结果
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
