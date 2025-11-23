namespace Domain.Enums
{
    public enum WorkStatus
    {
        Created,        // запись создана
        FileUploaded,   // известен fileId
        AnalysisPending,
        AnalysisInProgress,
        Analyzed,
        AnalysisFailed
    }
}