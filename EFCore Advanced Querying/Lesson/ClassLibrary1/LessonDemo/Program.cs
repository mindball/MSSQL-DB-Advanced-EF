using LessonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace LessonDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var mContext = new MusicXContext();

            //Executing Native SQL Queries
            //NativeSqlQuery.ExecuteSqlQuery(mContext);

            //Native SQL Queries with Parameters
            //NativeSqlQuery.ExecuteSqlQueryWithParameter(mContext);

            //Object State 
            //ObjectStateTracking.ChangeTracker(mContext);
            //ObjectStateTracking.ObjectCondition(mContext);
            //ObjectStateTracking.ChangeStateObject(mContext);

            //BulkOperation.SomeBulkOperation(mContext);

            //TypesOfLoading.ExplicitType(mContext);

            //ConcurrencyCheck.ConcurrencyWithoutAttribute(mContext);
            //ConcurrencyCheck.ConcurrencyWithAttribute(mContext);
            ConcurrencyCheck.ConcurrencyWithAttributeDifferentLogic(mContext);

        }
    }
}
