using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSTutorial.DataAccess.Model
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}