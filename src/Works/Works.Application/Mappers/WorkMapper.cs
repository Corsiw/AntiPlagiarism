using Domain.Entities;
using Works.Application.DTO.Analysis;
using Works.Application.UseCases.AddWork;
using Works.Application.UseCases.GetWorkById;
using Works.Application.UseCases.ListWorks;

namespace Works.Application.Mappers
{
    public class WorkMapper : IWorkMapper
    {
        public Work MapAddWorkRequestToEntity(AddWorkRequest request)
        {
            return new Work(request.StudentId, request.AssignmentId);
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
                work.FileId,
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

        public AnalyzeWorkRequestDto MapEntityToAnalyzeWorkRequest(Work work)
        {
            return new AnalyzeWorkRequestDto(
                work.WorkId,
                work.FileId ?? throw new KeyNotFoundException("File not attached"),
                work.StudentId,
                work.AssignmentId,
                work.SubmissionTime
            );
        }
    }
}