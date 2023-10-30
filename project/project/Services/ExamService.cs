using OfficeOpenXml;
using project.DTO;
using project.Models;
using project.Repositories;
using project.Repositories.IRepositories;
using project.Services.IServices;

namespace project.Services
{
    public class ExamService
    {
        private ExamRepository repository;

        private ExamService()
        {
            repository = new ExamRepository();
        }

        private static readonly ExamService _singleton = new ExamService();
        public static ExamService GetSingleton()
        {
            return _singleton;
        }

        public Dictionary<string, List<ExamDTO>> GetExams()
        {
            return repository.GetExams();
        }

        public List<Exam> GetExamsDistinct()
        {
            return repository.GetExamsDistinct();
        }

        public List<Exam> GetExam(int id)
        {
            return repository.GetExam(id);
        }
        public List<ExamDTO> GetExamNo()
        {
            return repository.GetExamsNo();
        }

        public ExamDTO GetExam(int id, int paperNo)
        {
            return repository.GetExam(id, paperNo);
        }

        public void Update(ExamDTO exam)
        {
            repository.Update(exam);
        }

        public void Add(ExamDTO exam)
        {
            repository.Add(exam);
        }

        public void Delete(int id, int paperNo)
        {
            repository.Delete(id, paperNo);
        }

        public IQueryable<Exam> GetAllExams()
        {
            return repository.GetAllExams();
        }

        private ExcelPackage CreateDoc(string title, string subject, string keyword)
        {
            var p = new ExcelPackage();
            p.Workbook.Properties.Title = title;
            p.Workbook.Properties.Author = "Application Name";
            p.Workbook.Properties.Subject = subject;
            p.Workbook.Properties.Keywords = keyword;
            return p;
        }

        public ExcelPackage getApplicantsStatistics(List<ExamResultStudent> exportData)
        {
            ExcelPackage p = CreateDoc("Applicant Statistics", "Applicant statistics", "All Applicants");
            var worksheet = p.Workbook.Worksheets.Add("Applicant Statistics");

            //Add Report Header
            worksheet.Cells[1, 1].Value = "Auto Scoring PE PRO192";
            worksheet.Cells[1, 1, 1, 3].Merge = true;

           
            //First add the headers
            worksheet.Cells[2, 1].Value = "PaperNo";
            worksheet.Cells[2, 2].Value = "StudentId";
            worksheet.Cells[2, 3].Value = "Mark";
            worksheet.Cells[2, 4].Value = "GradeNote";

            //Add values
            var numberformat = "#,##0";
            var dataCellStyleName = "TableNumber";
            var numStyle = p.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;

            for (int i = 0; i < exportData.Count; i++)
            {
                worksheet.Cells[i + 3, 1].Value = exportData[i].PaperNo;
                worksheet.Cells[i + 3, 2].Value = exportData[i].StudentId;
                worksheet.Cells[i + 3, 3].Value = exportData[i].Mark;
                worksheet.Cells[i + 3, 4].Value = exportData[i].GradeNote;
            }
            // Add to table / Add summary row
            var rowEnd = exportData.Count + 2;
            var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 2, fromCol: 1, toRow: rowEnd, toColumn: 3), "Applicants");
            tbl.ShowHeader = true;
            tbl.ShowTotal = true;
            tbl.Columns[2].DataCellStyleName = dataCellStyleName;
            worksheet.Cells[rowEnd, 3].Style.Numberformat.Format = numberformat;

            // AutoFitColumns
            worksheet.Cells[2, 1, rowEnd, 3].AutoFitColumns();
            return p;
        }
    }
}
