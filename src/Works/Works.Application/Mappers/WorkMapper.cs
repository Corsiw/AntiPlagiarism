using Domain.Entities;
using Domain.Enums;
using Works.Application.UseCases.AddWork;
using Works.Application.UseCases.GetWorkById;
using Works.Application.UseCases.ListWorks;

namespace Works.Application.Mappers
{
    public class WorkMapper : IWorkMapper
    {
        public Work MapAddWorkRequestToEntity(AddWorkRequest request)
        {
            return new Work
            {
                WorkId = Guid.NewGuid(),
                StudentId = request.StudentId,
                AssignmentId = request.AssignmentId,
                SubmissionTime = DateTime.UtcNow,
                Status = WorkStatus.Created
            };
        }

        public AddWorkResponse MapEntityToAddWorkResponse(Work work)
        {
            return new AddWorkResponse(
                work.WorkId,
                work.StudentId,
                work.AssignmentId,
                work.SubmissionTime,
                work.Status.ToString()
            );
        }

        public ListWorksResponseItem MapEntityToListWorksResponseItem(Work work)
        {
            return new ListWorksResponseItem(
                work.WorkId,
                work.StudentId,
                work.AssignmentId,
                work.SubmissionTime,
                work.Status.ToString(),
                work.ReportId,
                work.PlagiarismFlag
            );
        }

        public GetWorkByIdResponse MapEntityToGetWorkByIdResponse(Work work)
        {
            return new GetWorkByIdResponse(
                work.WorkId,
                work.StudentId,
                work.AssignmentId,
                work.SubmissionTime,
                work.FileId,
                work.Status.ToString(),
                work.ReportId,
                work.PlagiarismFlag
            );
        }
    }
}