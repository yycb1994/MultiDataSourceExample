# MultiDataSourceExample
自己封装的一个 .Net Core Unit of Work 基于Entity Framework Core 封装的多数据源操作 (MySql版本)<br>
调用示例：<br>

```csharp
public HomeController(ILogger<HomeController> logger, IUnitOfWork<AContext> unitOfWorkA, IUnitOfWork<BContext> unitOfWorkB)
{
    unitOfWorkA.BeginTran();//A数据库开始事务<br>
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
}
```


