using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }

        private DateTime _adddate = DateTime.Now;
        public DateTime AddDate 
        {
            get
            {
                return _adddate;
            }
            set
            {
                _adddate = value;

            }
        }

        private bool _isdeleted = false;
        public bool IsDeleted
        {
            get
            {
                return _isdeleted;
            }
            set
            {
                _isdeleted = value;
            }
        }
    }
}
