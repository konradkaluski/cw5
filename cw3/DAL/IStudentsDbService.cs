using cw3.DTOs.Requests;
using cw3.DTOs.Responses;

namespace cw3.DAL
{
    public interface IStudentsDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);


    }
}
