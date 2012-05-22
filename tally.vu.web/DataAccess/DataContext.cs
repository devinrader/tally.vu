using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using tallyvu.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace tallyvu.DataAccess
{
    public partial class DataContext : DbContext
    {
        public DbSet<Poll> Polls { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceError("Property: {0} Error: {1}",
                                         validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw;
            }
        }
    }
}