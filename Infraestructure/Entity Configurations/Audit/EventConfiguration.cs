using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{

    public class EventConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
         : this("dbo")
        {
        }
        public EventConfiguration(string schema)
        {
            ToTable("Event", schema);
            HasKey(x => x.EventId);
            Property(x => x.EventId).HasColumnName(@"EventId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity).HasColumnOrder(0);
            Property(x => x.InsertedDate).HasColumnName(@"InsertedDate").IsOptional();
            Property(x => x.LastUpdatedDate).HasColumnName(@"LastUpdatedDate").IsOptional();
            Property(x => x.Data).HasColumnName(@"Data").IsOptional();
        }
    }
}
