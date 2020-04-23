using cw3.Models;
using System.Collections.Generic;


namespace cw3.Services
{
    public interface IDBService
    {
        public IEnumerable<Student> GetStudents();
    }
}
