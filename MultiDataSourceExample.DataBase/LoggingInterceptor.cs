using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDataSourceExample.DataBase
{
    /// <summary>
    /// EF Core 的数据库操作拦截器，用于在数据库操作过程中进行日志记录和监视。
    /// </summary>
    /// <remarks>
    /// 作者：于春波
    /// 创建日期：2024年1月29日
    /// </remarks>
    public class LoggingInterceptor : DbCommandInterceptor
    {
        /// <summary>
        /// 在执行查询命令之前拦截并输出日志。
        /// </summary>
        /// <param name="command">要执行的查询命令。</param>
        /// <param name="eventData">命令事件数据。</param>
        /// <param name="result">拦截结果。</param>
        /// <returns>拦截结果。</returns>
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            var fullCommandText = command.CommandText;

            foreach (DbParameter param in command.Parameters)
            {
                var paramValue = param.Value is string ? $"'{param.Value}'" : param.Value.ToString();
                fullCommandText = fullCommandText.Replace(param.ParameterName, paramValue);
            }
            Console.WriteLine($"Executing query: {fullCommandText}");
            return base.ReaderExecuting(command, eventData, result);
        }

        /// <summary>
        /// 在执行非查询命令之前拦截并输出日志。
        /// </summary>
        /// <param name="command">要执行的非查询命令。</param>
        /// <param name="eventData">命令事件数据。</param>
        /// <param name="result">拦截结果。</param>
        /// <returns>拦截结果。</returns>
        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            var fullCommandText = command.CommandText;

            foreach (DbParameter param in command.Parameters)
            {
                var paramValue = param.Value is string ? $"'{param.Value}'" : param.Value.ToString();
                fullCommandText = fullCommandText.Replace(param.ParameterName, paramValue);
            }
            Console.WriteLine($"Executing non-query command: {fullCommandText}");
            return base.NonQueryExecuting(command, eventData, result);
        }

        /// <summary>
        /// 在执行标量查询命令之前拦截并输出日志。
        /// </summary>
        /// <param name="command">要执行的标量查询命令。</param>
        /// <param name="eventData">命令事件数据。</param>
        /// <param name="result">拦截结果。</param>
        /// <returns>拦截结果。</returns>
        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {

            var fullCommandText = command.CommandText;

            foreach (DbParameter param in command.Parameters)
            {
                var paramValue = param.Value is string ? $"'{param.Value}'" : param.Value.ToString();
                fullCommandText = fullCommandText.Replace(param.ParameterName, paramValue);
            }
            Console.WriteLine($"Executing scalar query: {fullCommandText}");
            return base.ScalarExecuting(command, eventData, result);
        }
    }
}
