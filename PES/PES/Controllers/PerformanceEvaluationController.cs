using PES.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using PES.Services;
using PES.ViewModels;
using System.Web.Script.Serialization;
using OfficeOpenXml;
using System.Threading.Tasks;

namespace PES.Controllers
{

    [Authorize]
    public class PerformanceEvaluationController : Controller
    {
        // Declare services 
        private PEService _peService;
        private EmployeeService _employeeService;
        private TitleService _titleService;
        private SubtitleService _subtitleService;
        private DescriptionService _descriptionService;
        private ScoreService _scoreService;
        private CommentService _commentService;
        private SkillService _skillService;
        private LM_SkillService _lm_skillService;
        private StatusService _statusService;
        private PeriodService _periodService;
        private LocationService _locationService;

        public PerformanceEvaluationController() 
        {
            _peService = new PEService();
            _employeeService = new EmployeeService();
            _titleService = new TitleService();
            _subtitleService = new SubtitleService();
            _descriptionService = new DescriptionService();
            _scoreService = new ScoreService();
            _commentService = new CommentService();
            _skillService = new SkillService();
            _lm_skillService = new LM_SkillService();
            _statusService = new StatusService();
            _periodService = new PeriodService();
            _locationService = new LocationService();
        }

        // GET: PerformanceEvaluation
        public ActionResult Index()
        {
            return View();
        }

        public bool ReadPerformanceFile(string path, Employee user, Employee evaluator, int year, int period, int replace) 
        {
            // CREATE DE EXCEL FILE AND ACCES AT THE WORK SHEET

            FileInfo excel = new FileInfo(path);
            ExcelPackage excelFile = new ExcelPackage(excel);
            ExcelWorksheet excelSheet = excelFile.Workbook.Worksheets[1];
          
            try
            {
                // Read
                /*read excel*/

                PESComplete PESc = new PESComplete();

                #region Performance Evaluation - insert

                //PESc.Pes.Total = excelSheet.Cells[6, 9].Value;
                PESc.Pes.Total = excelSheet.GetValue<double>(6, 9);
                PESc.Pes.EmployeeId = user.EmployeeId;
                PESc.Pes.EvaluatorId = evaluator.EmployeeId;
                PESc.Pes.EvaluationPeriod = DateTime.Now.Date;
                var status = _statusService.GetStatusByDescription("Incomplete");
                PESc.Pes.StatusId = status != null ? status.StatusId : 1;
                PESc.Pes.EnglishScore = Convert.ToInt32(excelSheet.Cells[72, 7].Value);
                //PESc.Pes.PerformanceScore = excelSheet.Cells[37, 9].Value;
                PESc.Pes.PerformanceScore = excelSheet.GetValue<double>(37, 9);
                //PESc.Pes.CompeteneceScore = excelSheet.Cells[73, 9].Value;
                PESc.Pes.CompeteneceScore = excelSheet.GetValue<double>(73, 9);
                PESc.Pes.EvaluationYear = year;
                PESc.Pes.PeriodId = period;

                //Replace PE
                if(replace != 0)
                {
                    bool peDisabled = _peService.UpdateStatus(replace);
                }
                // Insert 
                bool peInserted = _peService.InsertPE(PESc.Pes);


                // Look for and get id 
                PESc.Pes = _peService.GetPerformanceEvaluationByDateEmail(user.Email, DateTime.Now.Date);
                #endregion

                #region Employee - update
                //// data from user
                //PESc.Empleado.EmployeeId = user.EmployeeId;
                //PESc.Empleado.Email = user.Email;
                //PESc.Empleado.ProfileId = user.ProfileId;
                //PESc.Empleado.ManagerId = user.ManagerId;
                ////PESc.Empleado.HireDate = user.HireDate;
                //PESc.Empleado.EndDate = user.EndDate;
                //// data from excel
                ////PESc.Empleado.FirstName = excelSheet.Cells[3, 3].Value;
                //var Name = excelSheet.GetValue<string>(3, 3).Split(' ');
                //PESc.Empleado.FirstName = Name[0];
                ////PESc.Empleado.FirstName = excelSheet.GetValue<string>(3, 3);
                ////PESc.Empleado.LastName = excelSheet.Cells[3, 3].Value;
                //PESc.Empleado.LastName = Name[2];
                ////PESc.Empleado.LastName = excelSheet.GetValue<string>(3, 3);
                ////PESc.Empleado.Position = excelSheet.Cells[4, 3].Value;
                //PESc.Empleado.Position = excelSheet.GetValue<string>(4, 3);
                ////PESc.Empleado.Customer = excelSheet.Cells[5, 3].Value;
                //PESc.Empleado.Customer = excelSheet.GetValue<string>(5, 3);
                ////PESc.Empleado.Project = excelSheet.Cells[6, 3].Value;
                //PESc.Empleado.Project = excelSheet.GetValue<string>(6, 3);

                //bool employeeInserted = _employeeService.UpdateEmployee(PESc.Empleado);
                #endregion

                #region Title - insert
                //PESc.Title1.Name = PerformanceSection.PerformanceTitle.Name;
                //PESc.Title2.Name = CompetencesSection.CompetenceTitle.Name;
                ////PESc.Title1.Name = excelSheet.Cells[19, 2].Value;
                ////PESc.Title2.Name = excelSheet.Cells[39, 2].Value;

                //bool title1Inserted = _titleService.InsertTitle(PESc.Title1);
                //bool title2Inserted = _titleService.InsertTitle(PESc.Title2);
                #endregion

                #region Subtitle - insert
                //PESc.Subtitle1.Name = PerformanceSection.QualitySubtitle.Name;
                //PESc.Subtitle1.TitleId = PerformanceSection.QualitySubtitle.TitleId;
                //PESc.Subtitle2.Name = PerformanceSection.OpportunitySubtitle.Name;
                //PESc.Subtitle2.TitleId = PerformanceSection.OpportunitySubtitle.TitleId;
                //PESc.Subtitle3.Name = CompetencesSection.SkillsSubtitle.Name;
                //PESc.Subtitle3.TitleId = CompetencesSection.SkillsSubtitle.TitleId;
                //PESc.Subtitle4.Name = CompetencesSection.InterpersonalSubtitle.Name;
                //PESc.Subtitle4.TitleId = CompetencesSection.InterpersonalSubtitle.TitleId;
                //PESc.Subtitle5.Name = CompetencesSection.GrowthSubtitle.Name;
                //PESc.Subtitle5.TitleId = CompetencesSection.GrowthSubtitle.TitleId;
                //PESc.Subtitle6.Name = CompetencesSection.PoliciesSubtitle.Name;
                //PESc.Subtitle6.TitleId = CompetencesSection.PoliciesSubtitle.TitleId;
                ////PESc.Subtitle1.Name = excelSheet.Cells[23, 2].Value;
                ////PESc.Subtitle2.Name = excelSheet.Cells[32, 2].Value;
                ////PESc.Subtitle3.Name = excelSheet.Cells[43, 2].Value;
                ////PESc.Subtitle4.Name = excelSheet.Cells[52, 2].Value;
                ////PESc.Subtitle5.Name = excelSheet.Cells[59, 2].Value;
                ////PESc.Subtitle6.Name = excelSheet.Cells[67, 2].Value;

                //bool subtitle1Inserted = _subtitleService.InsertSubtitles(PESc.Subtitle1);
                //bool subtitle2Inserted = _subtitleService.InsertSubtitles(PESc.Subtitle2);
                //bool subtitle3Inserted = _subtitleService.InsertSubtitles(PESc.Subtitle3);
                //bool subtitle4Inserted = _subtitleService.InsertSubtitles(PESc.Subtitle4);
                //bool subtitle5Inserted = _subtitleService.InsertSubtitles(PESc.Subtitle5);
                //bool subtitle6Inserted = _subtitleService.InsertSubtitles(PESc.Subtitle6);
                #endregion

                #region Description - insert
                //PESc.Description1.DescriptionText = PerformanceSection.AccuracyQualityDescription1.DescriptionText;
                //PESc.Description1.SubtitleId = PerformanceSection.AccuracyQualityDescription1.SubtitleId;
                //PESc.Description2.DescriptionText = PerformanceSection.ThoroughnessQualityDescription2.DescriptionText;
                //PESc.Description2.SubtitleId = PerformanceSection.ThoroughnessQualityDescription2.SubtitleId;
                //PESc.Description3.DescriptionText = PerformanceSection.ReliabilityQualityDescription3.DescriptionText;
                //PESc.Description3.SubtitleId = PerformanceSection.ReliabilityQualityDescription3.SubtitleId;
                //PESc.Description4.DescriptionText = PerformanceSection.ResponsivenessQualityDescription4.DescriptionText;
                //PESc.Description4.SubtitleId = PerformanceSection.ResponsivenessQualityDescription4.SubtitleId;
                //PESc.Description5.DescriptionText = PerformanceSection.FollowQualityDescription5.DescriptionText;
                //PESc.Description5.SubtitleId = PerformanceSection.FollowQualityDescription5.SubtitleId;
                //PESc.Description6.DescriptionText = PerformanceSection.JudgmentQualityDescription6.DescriptionText;
                //PESc.Description6.SubtitleId = PerformanceSection.JudgmentQualityDescription6.SubtitleId;
                //PESc.SubtotalDescQuality.DescriptionText = PerformanceSection.SubtotalQualityDescription7.DescriptionText;
                //PESc.SubtotalDescQuality.SubtitleId = PerformanceSection.SubtotalQualityDescription7.SubtitleId;

                //PESc.Description7.DescriptionText = PerformanceSection.PriorityOpportunityDescription8.DescriptionText;
                //PESc.Description7.SubtitleId = PerformanceSection.PriorityOpportunityDescription8.SubtitleId;
                //PESc.Description8.DescriptionText = PerformanceSection.AmountOpportunityDescription9.DescriptionText;
                //PESc.Description8.SubtitleId = PerformanceSection.AmountOpportunityDescription9.SubtitleId;
                //PESc.Description9.DescriptionText = PerformanceSection.WorkOpportunityDescription10.DescriptionText;
                //PESc.Description9.SubtitleId = PerformanceSection.WorkOpportunityDescription10.SubtitleId;
                //PESc.SubtotalOpportunity.DescriptionText = PerformanceSection.SubtotalOpportunityDescription11.DescriptionText;
                //PESc.SubtotalOpportunity.SubtitleId = PerformanceSection.SubtotalOpportunityDescription11.SubtitleId;
                //PESc.TotalPerformance.DescriptionText = PerformanceSection.TotalPerformanceDescription12.DescriptionText;
                //PESc.TotalPerformance.SubtitleId = PerformanceSection.TotalPerformanceDescription12.SubtitleId;

                //PESc.Description10.DescriptionText = CompetencesSection.JobSkillDescription13.DescriptionText;
                //PESc.Description10.SubtitleId = CompetencesSection.JobSkillDescription13.SubtitleId;
                //PESc.Description11.DescriptionText = CompetencesSection.AnalyzesSkillDescription14.DescriptionText;
                //PESc.Description11.SubtitleId = CompetencesSection.AnalyzesSkillDescription14.SubtitleId;
                //PESc.Description12.DescriptionText = CompetencesSection.FlexibleSkillDescription15.DescriptionText;
                //PESc.Description12.SubtitleId = CompetencesSection.FlexibleSkillDescription15.SubtitleId;
                //PESc.Description13.DescriptionText = CompetencesSection.PlanningSkillDescription16.DescriptionText;
                //PESc.Description13.SubtitleId = CompetencesSection.PlanningSkillDescription16.SubtitleId;
                //PESc.Description14.DescriptionText = CompetencesSection.CompetentSkillDescription17.DescriptionText;
                //PESc.Description14.SubtitleId = CompetencesSection.CompetentSkillDescription17.SubtitleId;
                //PESc.Description15.DescriptionText = CompetencesSection.FollowsSkillDescription18.DescriptionText;
                //PESc.Description15.SubtitleId = CompetencesSection.FollowsSkillDescription18.SubtitleId;
                //PESc.SubtotalSkills.DescriptionText = CompetencesSection.SubtotalSkillDescription19.DescriptionText;
                //PESc.SubtotalSkills.SubtitleId = CompetencesSection.SubtotalSkillDescription19.SubtitleId;

                //PESc.Description16.DescriptionText = CompetencesSection.SupervisorInterpersonalDescription20.DescriptionText;
                //PESc.Description16.SubtitleId = CompetencesSection.SupervisorInterpersonalDescription20.SubtitleId;
                //PESc.Description17.DescriptionText = CompetencesSection.OtherInterpersonalDescription21.DescriptionText;
                //PESc.Description17.SubtitleId = CompetencesSection.OtherInterpersonalDescription21.SubtitleId;
                //PESc.Description18.DescriptionText = CompetencesSection.ClientInterpersonalDescription22.DescriptionText;
                //PESc.Description18.SubtitleId = CompetencesSection.ClientInterpersonalDescription22.SubtitleId;
                //PESc.Description19.DescriptionText = CompetencesSection.CommitmentInterpersonalDescription23.DescriptionText;
                //PESc.Description19.SubtitleId = CompetencesSection.CommitmentInterpersonalDescription23.SubtitleId;
                //PESc.SubtotalInterpersonal.DescriptionText = CompetencesSection.SubtotalInterpersonalDescription24.DescriptionText;
                //PESc.SubtotalInterpersonal.SubtitleId = CompetencesSection.SubtotalInterpersonalDescription24.SubtitleId;

                //PESc.Description20.DescriptionText = CompetencesSection.ActivelyGrowthDescription25.DescriptionText;
                //PESc.Description20.SubtitleId = CompetencesSection.ActivelyGrowthDescription25.SubtitleId;
                //PESc.Description21.DescriptionText = CompetencesSection.OpenGrowthDescription26.DescriptionText;
                //PESc.Description21.SubtitleId = CompetencesSection.OpenGrowthDescription26.SubtitleId;
                //PESc.Description22.DescriptionText = CompetencesSection.InvolvementGrowthDescription27.DescriptionText;
                //PESc.Description22.SubtitleId = CompetencesSection.InvolvementGrowthDescription27.SubtitleId;
                //PESc.Description23.DescriptionText = CompetencesSection.ChallengesGrowthDescription28.DescriptionText;
                //PESc.Description23.SubtitleId = CompetencesSection.ChallengesGrowthDescription28.SubtitleId;
                //PESc.Description24.DescriptionText = CompetencesSection.SeeksGrowthDescription29.DescriptionText;
                //PESc.Description24.SubtitleId = CompetencesSection.SeeksGrowthDescription29.SubtitleId;
                //PESc.SubtotalGrowth.DescriptionText = CompetencesSection.SubtotalGrowthDescription30.DescriptionText;
                //PESc.SubtotalGrowth.SubtitleId = CompetencesSection.SubtotalGrowthDescription30.SubtitleId;

                //PESc.DescriptionPuctuality.DescriptionText = CompetencesSection.PunctualityPoliciesDescription31.DescriptionText;
                //PESc.DescriptionPuctuality.SubtitleId = CompetencesSection.PunctualityPoliciesDescription31.SubtitleId;
                //PESc.DescriptionPolicies.DescriptionText = CompetencesSection.PoliciesPoliciesDescription32.DescriptionText;
                //PESc.DescriptionPolicies.SubtitleId = CompetencesSection.PoliciesPoliciesDescription32.SubtitleId;
                //PESc.DescriptionValues.DescriptionText = CompetencesSection.ValuesPoliciesDescription33.DescriptionText;
                //PESc.DescriptionValues.SubtitleId = CompetencesSection.ValuesPoliciesDescription33.SubtitleId;
                //PESc.SubtotalPolicies.DescriptionText = CompetencesSection.SubtotalPoliciesDescription34.DescriptionText;
                //PESc.SubtotalPolicies.SubtitleId = CompetencesSection.SubtotalPoliciesDescription34.SubtitleId;
                //PESc.EnglishEvaluationDescription.DescriptionText = CompetencesSection.EnglishEvaluationDescription.DescriptionText;
                //PESc.EnglishEvaluationDescription.SubtitleId = CompetencesSection.EnglishEvaluationDescription.SubtitleId;
                //PESc.TotalCompetences.DescriptionText = CompetencesSection.TotalCompetencesDescription35.DescriptionText;
                //PESc.TotalCompetences.SubtitleId = CompetencesSection.TotalCompetencesDescription35.SubtitleId;

                ////PESc.Description1.DescriptionText = excelSheet.Cells[24, 2].Value;
                ////PESc.Description2.DescriptionText = excelSheet.Cells[25, 2].Value;
                ////PESc.Description3.DescriptionText = excelSheet.Cells[26, 2].Value;
                ////PESc.Description4.DescriptionText = excelSheet.Cells[27, 2].Value;
                ////PESc.Description5.DescriptionText = excelSheet.Cells[28, 2].Value;
                ////PESc.Description6.DescriptionText = excelSheet.Cells[29, 2].Value;
                ////PESc.Description7.DescriptionText = excelSheet.Cells[33, 2].Value;
                ////PESc.Description8.DescriptionText = excelSheet.Cells[34, 2].Value;
                ////PESc.Description9.DescriptionText = excelSheet.Cells[35, 2].Value;
                ////PESc.Description10.DescriptionText = excelSheet.Cells[44, 2].Value;
                ////PESc.Description11.DescriptionText = excelSheet.Cells[45, 2].Value;
                ////PESc.Description12.DescriptionText = excelSheet.Cells[46, 2].Value;
                ////PESc.Description13.DescriptionText = excelSheet.Cells[47, 2].Value;
                ////PESc.Description14.DescriptionText = excelSheet.Cells[48, 2].Value;
                ////PESc.Description15.DescriptionText = excelSheet.Cells[49, 2].Value;
                ////PESc.Description16.DescriptionText = excelSheet.Cells[53, 2].Value;
                ////PESc.Description17.DescriptionText = excelSheet.Cells[54, 2].Value;
                ////PESc.Description18.DescriptionText = excelSheet.Cells[55, 2].Value;
                ////PESc.Description19.DescriptionText = excelSheet.Cells[56, 2].Value;
                ////PESc.Description21.DescriptionText = excelSheet.Cells[61, 2].Value;
                ////PESc.Description22.DescriptionText = excelSheet.Cells[62, 2].Value;
                ////PESc.Description24.DescriptionText = excelSheet.Cells[64, 2].Value;
                ////PESc.DescriptionPuctuality.DescriptionText = excelSheet.Cells[68, 2].Value;
                ////PESc.DescriptionPolicies.DescriptionText = excelSheet.Cells[69, 2].Value;
                ////PESc.DescriptionValues.DescriptionText = excelSheet.Cells[70, 2].Value;
                ////PESc.subtotalQuality.DescriptionText = excelSheet.Cells[30, 2].Value;
                ////PESc.SubtotalOpportunity.DescriptionText = excelSheet.Cells[36, 2].Value;
                ////PESc.TotalPerformance.DescriptionText = excelSheet.Cells[37, 2].Value;
                ////PESc.SubtotalSkills.DescriptionText = excelSheet.Cells[50, 2].Value;
                ////PESc.SubtotalInterpersonal.DescriptionText = excelSheet.Cells[57, 2].Value;
                ////PESc.SubtotalGrowth.DescriptionText = excelSheet.Cells[65, 2].Value;
                ////PESc.SubtotalPolicies.DescriptionText = excelSheet.Cells[71, 2].Value;
                ////PESc.TotalCompetences.DescriptionText = excelSheet.Cells[73, 2].Value;

                //bool description1Inserted = _descriptionService.InsertDescription(PESc.Description1);
                //bool description2Inserted = _descriptionService.InsertDescription(PESc.Description2);
                //bool description3Inserted = _descriptionService.InsertDescription(PESc.Description3);
                //bool description4Inserted = _descriptionService.InsertDescription(PESc.Description4);
                //bool description5Inserted = _descriptionService.InsertDescription(PESc.Description5);
                //bool description6Inserted = _descriptionService.InsertDescription(PESc.Description6);
                //bool descriptionSubtotalQualityInserted = _descriptionService.InsertDescription(PESc.SubtotalDescQuality);
                //bool description7Inserted = _descriptionService.InsertDescription(PESc.Description7);
                //bool description8Inserted = _descriptionService.InsertDescription(PESc.Description8);
                //bool description9Inserted = _descriptionService.InsertDescription(PESc.Description9);
                //bool descriptionSubtotalOpportunityInserted = _descriptionService.InsertDescription(PESc.SubtotalOpportunity);
                //bool descriptionTotalPerformanceInserted = _descriptionService.InsertDescription(PESc.TotalPerformance);
                //bool description10Inserted = _descriptionService.InsertDescription(PESc.Description10);
                //bool description11Inserted = _descriptionService.InsertDescription(PESc.Description11);
                //bool description12Inserted = _descriptionService.InsertDescription(PESc.Description12);
                //bool description13Inserted = _descriptionService.InsertDescription(PESc.Description13);
                //bool description14Inserted = _descriptionService.InsertDescription(PESc.Description14);
                //bool description15Inserted = _descriptionService.InsertDescription(PESc.Description15);
                //bool descriptionSubtotalSkillsInserted = _descriptionService.InsertDescription(PESc.SubtotalSkills);
                //bool description16Inserted = _descriptionService.InsertDescription(PESc.Description16);
                //bool description17Inserted = _descriptionService.InsertDescription(PESc.Description17);
                //bool description18Inserted = _descriptionService.InsertDescription(PESc.Description18);
                //bool description19Inserted = _descriptionService.InsertDescription(PESc.Description19);
                //bool descriptionSubtotalInterpersonalInserted = _descriptionService.InsertDescription(PESc.SubtotalInterpersonal);
                //bool description20Inserted = _descriptionService.InsertDescription(PESc.Description20);
                //bool description21Inserted = _descriptionService.InsertDescription(PESc.Description21);
                //bool description22Inserted = _descriptionService.InsertDescription(PESc.Description22);
                //bool description23Inserted = _descriptionService.InsertDescription(PESc.Description23);
                //bool description24Inserted = _descriptionService.InsertDescription(PESc.Description24);
                //bool descriptionSubtotalGrowthInserted = _descriptionService.InsertDescription(PESc.SubtotalGrowth);
                //bool descriptionPuctuallityInserted = _descriptionService.InsertDescription(PESc.DescriptionPuctuality);
                //bool descriptionPoliciesInserted = _descriptionService.InsertDescription(PESc.DescriptionPolicies);
                //bool descriptionValuesInserted = _descriptionService.InsertDescription(PESc.DescriptionValues);
                //bool descriptionSubtotalPoliciesInserted = _descriptionService.InsertDescription(PESc.SubtotalPolicies);
                //bool descriptionEnglishEvalationInserted = _descriptionService.InsertDescription(PESc.EnglishEvaluationDescription);
                //bool descriptionTotalCompetencesInserted = _descriptionService.InsertDescription(PESc.TotalCompetences);
                #endregion

                #region ScoreEmployee - insert
                PESc.One.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[24, 6].Value);
                PESc.One.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.AccuracyQualityDescription1.DescriptionText).DescriptionId;
                PESc.Two.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[25, 6].Value);
                PESc.Two.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.ThoroughnessQualityDescription2.DescriptionText).DescriptionId;
                PESc.Three.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[26, 6].Value);
                PESc.Three.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.ReliabilityQualityDescription3.DescriptionText).DescriptionId;
                PESc.Four.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[27, 6].Value);
                PESc.Four.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.ResponsivenessQualityDescription4.DescriptionText).DescriptionId;
                PESc.Five.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[28, 6].Value);
                PESc.Five.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.FollowQualityDescription5.DescriptionText).DescriptionId;
                PESc.Six.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[29, 6].Value);
                PESc.Six.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.JudgmentQualityDescription6.DescriptionText).DescriptionId;
                PESc.ScoreQuality.ScoreEmployee = 0;
                PESc.ScoreQuality.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.SubtotalQualityDescription7.DescriptionText).DescriptionId;

                PESc.Seven.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[33, 6].Value);
                PESc.Seven.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.PriorityOpportunityDescription8.DescriptionText).DescriptionId;
                PESc.Eight.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[34, 6].Value);
                PESc.Eight.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.AmountOpportunityDescription9.DescriptionText).DescriptionId;
                PESc.Nine.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[35, 6].Value);
                PESc.Nine.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.WorkOpportunityDescription10.DescriptionText).DescriptionId;
                PESc.ScoreOpportunity.ScoreEmployee = 0;
                PESc.ScoreOpportunity.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.SubtotalOpportunityDescription11.DescriptionText).DescriptionId;
                PESc.ScorePerformance.ScoreEmployee = 0;
                PESc.ScorePerformance.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.TotalPerformanceDescription12.DescriptionText).DescriptionId;

                PESc.Ten.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[44, 6].Value);
                PESc.Ten.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.JobSkillDescription13.DescriptionText).DescriptionId;
                PESc.Eleven.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[45, 6].Value);
                PESc.Eleven.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.AnalyzesSkillDescription14.DescriptionText).DescriptionId;
                PESc.Twelve.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[46, 6].Value);
                PESc.Twelve.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.FlexibleSkillDescription15.DescriptionText).DescriptionId;
                PESc.Thirteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[47, 6].Value);
                PESc.Thirteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.PlanningSkillDescription16.DescriptionText).DescriptionId;
                PESc.Fourteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[48, 6].Value);
                PESc.Fourteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.CompetentSkillDescription17.DescriptionText).DescriptionId;
                PESc.Fifteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[49, 6].Value);
                PESc.Fifteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.FollowsSkillDescription18.DescriptionText).DescriptionId;
                PESc.ScoreSkills.ScoreEmployee = 0;
                PESc.ScoreSkills.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalSkillDescription19.DescriptionText).DescriptionId;

                PESc.Sixteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[53, 6].Value);
                PESc.Sixteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SupervisorInterpersonalDescription20.DescriptionText).DescriptionId;
                PESc.Seventeen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[54, 6].Value);
                PESc.Seventeen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.OtherInterpersonalDescription21.DescriptionText).DescriptionId;
                PESc.Eighteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[55, 6].Value);
                PESc.Eighteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ClientInterpersonalDescription22.DescriptionText).DescriptionId;
                PESc.Nineteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[56, 6].Value);
                PESc.Nineteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.CommitmentInterpersonalDescription23.DescriptionText).DescriptionId;
                PESc.ScoreInterpersonal.ScoreEmployee = 0;
                PESc.ScoreInterpersonal.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalInterpersonalDescription24.DescriptionText).DescriptionId;

                PESc.Twenty.ScoreEmployee = 0;
                PESc.Twenty.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ActivelyGrowthDescription25.DescriptionText).DescriptionId;
                PESc.Twentyone.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[61, 6].Value);
                PESc.Twentyone.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.OpenGrowthDescription26.DescriptionText).DescriptionId;
                PESc.Twentytwo.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[62, 6].Value);
                PESc.Twentytwo.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.InvolvementGrowthDescription27.DescriptionText).DescriptionId;
                PESc.Twentythree.ScoreEmployee = 0;
                PESc.Twentythree.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ChallengesGrowthDescription28.DescriptionText).DescriptionId;
                PESc.Twentyfour.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[64, 6].Value);
                PESc.Twentyfour.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SeeksGrowthDescription29.DescriptionText).DescriptionId;
                PESc.ScoreGrowth.ScoreEmployee = 0;
                PESc.ScoreGrowth.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalGrowthDescription30.DescriptionText).DescriptionId;

                PESc.Punctuality.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[68, 6].Value);
                PESc.Punctuality.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.PunctualityPoliciesDescription31.DescriptionText).DescriptionId;
                PESc.Policies.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[69, 6].Value);
                PESc.Policies.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.PoliciesPoliciesDescription32.DescriptionText).DescriptionId;
                PESc.Values.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[70, 6].Value);
                PESc.Values.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ValuesPoliciesDescription33.DescriptionText).DescriptionId;
                PESc.ScorePolicies.ScoreEmployee = 0;
                PESc.ScorePolicies.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalPoliciesDescription34.DescriptionText).DescriptionId;
                PESc.ScoreCompetences.ScoreEmployee = 0;
                PESc.ScoreCompetences.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.TotalCompetencesDescription36.DescriptionText).DescriptionId;
                PESc.EnglishScore.ScoreEmployee = 0;
                PESc.EnglishScore.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.EnglishEvaluationDescription.DescriptionText).DescriptionId;
                #endregion

                #region ScoreEvaluator - insert
                PESc.One.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[24, 7].Value);
                PESc.One.PEId = PESc.Pes.PEId;
                PESc.Two.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[25, 7].Value);
                PESc.Two.PEId = PESc.Pes.PEId;
                PESc.Three.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[26, 7].Value);
                PESc.Three.PEId = PESc.Pes.PEId;
                PESc.Four.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[27, 7].Value);
                PESc.Four.PEId = PESc.Pes.PEId;
                PESc.Five.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[28, 7].Value);
                PESc.Five.PEId = PESc.Pes.PEId;
                PESc.Six.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[29, 7].Value);
                PESc.Six.PEId = PESc.Pes.PEId;
                PESc.ScoreQuality.ScoreEvaluator = 0;
                PESc.ScoreQuality.PEId = PESc.Pes.PEId;

                PESc.Seven.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[33, 7].Value);
                PESc.Seven.PEId = PESc.Pes.PEId;
                PESc.Eight.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[34, 7].Value);
                PESc.Eight.PEId = PESc.Pes.PEId;
                PESc.Nine.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[35, 7].Value);
                PESc.Nine.PEId = PESc.Pes.PEId;
                PESc.ScoreOpportunity.ScoreEvaluator = 0;
                PESc.ScoreOpportunity.PEId = PESc.Pes.PEId;
                PESc.ScorePerformance.ScoreEvaluator = 0;
                PESc.ScorePerformance.PEId = PESc.Pes.PEId;

                PESc.Ten.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[44, 7].Value);
                PESc.Ten.PEId = PESc.Pes.PEId;
                PESc.Eleven.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[45, 7].Value);
                PESc.Eleven.PEId = PESc.Pes.PEId;
                PESc.Twelve.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[46, 7].Value);
                PESc.Twelve.PEId = PESc.Pes.PEId;
                PESc.Thirteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[47, 7].Value);
                PESc.Thirteen.PEId = PESc.Pes.PEId;
                PESc.Fourteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[48, 7].Value);
                PESc.Fourteen.PEId = PESc.Pes.PEId;
                PESc.Fifteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[49, 7].Value);
                PESc.Fifteen.PEId = PESc.Pes.PEId;
                PESc.ScoreSkills.ScoreEvaluator = 0;
                PESc.ScoreSkills.PEId = PESc.Pes.PEId;

                PESc.Sixteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[53, 7].Value);
                PESc.Sixteen.PEId = PESc.Pes.PEId;
                PESc.Seventeen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[54, 7].Value);
                PESc.Seventeen.PEId = PESc.Pes.PEId;
                PESc.Eighteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[55, 7].Value);
                PESc.Eighteen.PEId = PESc.Pes.PEId;
                PESc.Nineteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[56, 7].Value);
                PESc.Nineteen.PEId = PESc.Pes.PEId;
                PESc.ScoreInterpersonal.ScoreEvaluator = 0;
                PESc.ScoreInterpersonal.PEId = PESc.Pes.PEId;

                PESc.Twenty.ScoreEvaluator = 0;
                PESc.Twenty.PEId = PESc.Pes.PEId;
                PESc.Twentyone.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[61, 7].Value);
                PESc.Twentyone.PEId = PESc.Pes.PEId;
                PESc.Twentytwo.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[62, 7].Value);
                PESc.Twentytwo.PEId = PESc.Pes.PEId;
                PESc.Twentythree.ScoreEvaluator = 0;
                PESc.Twentythree.PEId = PESc.Pes.PEId;
                PESc.Twentyfour.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[64, 7].Value);
                PESc.Twentyfour.PEId = PESc.Pes.PEId;
                PESc.ScoreGrowth.ScoreEvaluator = 0;
                PESc.ScoreGrowth.PEId = PESc.Pes.PEId;

                PESc.Punctuality.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[68, 7].Value);
                PESc.Punctuality.PEId = PESc.Pes.PEId;
                PESc.Policies.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[69, 7].Value);
                PESc.Policies.PEId = PESc.Pes.PEId;
                PESc.Values.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[70, 7].Value);
                PESc.Values.PEId = PESc.Pes.PEId;
                PESc.ScorePolicies.ScoreEvaluator = 0;
                PESc.ScorePolicies.PEId = PESc.Pes.PEId;
                PESc.ScoreCompetences.ScoreEvaluator = 0;
                PESc.ScoreCompetences.PEId = PESc.Pes.PEId;
                PESc.EnglishScore.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[72, 7].Value);
                PESc.EnglishScore.PEId = PESc.Pes.PEId;
                #endregion

                #region Comments - insert
                PESc.One.Comments = excelSheet.GetValue<string>(24, 8);
                PESc.Two.Comments = excelSheet.GetValue<string>(25, 8);
                PESc.Three.Comments = excelSheet.GetValue<string>(26, 8);
                PESc.Four.Comments = excelSheet.GetValue<string>(27, 8);
                PESc.Five.Comments = excelSheet.GetValue<string>(28, 8);
                PESc.Six.Comments = excelSheet.GetValue<string>(29, 8);
                PESc.ScoreQuality.Comments = "";

                PESc.Seven.Comments = excelSheet.GetValue<string>(33, 8);
                PESc.Eight.Comments = excelSheet.GetValue<string>(34, 8);
                PESc.Nine.Comments = excelSheet.GetValue<string>(35, 8);
                PESc.ScoreOpportunity.Comments = "";
                PESc.ScorePerformance.Comments = "";

                PESc.Ten.Comments = excelSheet.GetValue<string>(44, 8);
                PESc.Eleven.Comments = excelSheet.GetValue<string>(45, 8);
                PESc.Twelve.Comments = excelSheet.GetValue<string>(46, 8);
                PESc.Thirteen.Comments = excelSheet.GetValue<string>(47, 8);
                PESc.Fourteen.Comments = excelSheet.GetValue<string>(48, 8);
                PESc.Fifteen.Comments = excelSheet.GetValue<string>(49, 8);
                PESc.ScoreSkills.Comments = "";

                PESc.Sixteen.Comments = excelSheet.GetValue<string>(53, 8);
                PESc.Seventeen.Comments = excelSheet.GetValue<string>(54, 8);
                PESc.Eighteen.Comments = excelSheet.GetValue<string>(55, 8);
                PESc.Nineteen.Comments = excelSheet.GetValue<string>(56, 8);
                PESc.ScoreInterpersonal.Comments = "";

                PESc.Twenty.Comments = "";
                PESc.Twentyone.Comments = excelSheet.GetValue<string>(61, 8);
                PESc.Twentytwo.Comments = excelSheet.GetValue<string>(62, 8);
                PESc.Twentythree.Comments = "";
                PESc.Twentyfour.Comments = excelSheet.GetValue<string>(64, 8);
                PESc.ScoreGrowth.Comments = "";

                PESc.Punctuality.Comments = excelSheet.GetValue<string>(68, 8);
                PESc.Policies.Comments = excelSheet.GetValue<string>(69, 8);
                PESc.Values.Comments = excelSheet.GetValue<string>(70, 8);
                PESc.ScorePolicies.Comments = "";
                PESc.ScoreCompetences.Comments = "";
                PESc.EnglishScore.Comments = excelSheet.GetValue<string>(72, 8);
                #endregion

                #region Calculation - insert
                PESc.One.Calculation = excelSheet.GetValue<double>(24, 9);
                PESc.Two.Calculation = excelSheet.GetValue<double>(25, 9);
                PESc.Three.Calculation = excelSheet.GetValue<double>(26, 9);
                PESc.Four.Calculation = excelSheet.GetValue<double>(27, 9);
                PESc.Five.Calculation = excelSheet.GetValue<double>(28, 9);
                PESc.Six.Calculation = excelSheet.GetValue<double>(29, 9);

                PESc.Seven.Calculation = excelSheet.GetValue<double>(33, 9);
                PESc.Eight.Calculation = excelSheet.GetValue<double>(34, 9);
                PESc.Nine.Calculation = excelSheet.GetValue<double>(35, 9);

                PESc.Ten.Calculation = excelSheet.GetValue<double>(44, 9);
                PESc.Eleven.Calculation = excelSheet.GetValue<double>(45, 9);
                PESc.Twelve.Calculation = excelSheet.GetValue<double>(46, 9);
                PESc.Thirteen.Calculation = excelSheet.GetValue<double>(47, 9);
                PESc.Fourteen.Calculation = excelSheet.GetValue<double>(48, 9);
                PESc.Fifteen.Calculation = excelSheet.GetValue<double>(49, 9);

                PESc.Sixteen.Calculation = excelSheet.GetValue<double>(53, 9);
                PESc.Seventeen.Calculation = excelSheet.GetValue<double>(54, 9);
                PESc.Eighteen.Calculation = excelSheet.GetValue<double>(55, 9);
                PESc.Nineteen.Calculation = excelSheet.GetValue<double>(56, 9);

                PESc.Twenty.Calculation = 0;
                PESc.Twentyone.Calculation = excelSheet.GetValue<double>(61, 9);
                PESc.Twentytwo.Calculation = excelSheet.GetValue<double>(62, 9);
                PESc.Twentythree.Calculation = 0;
                PESc.Twentyfour.Calculation = excelSheet.GetValue<double>(64, 9);

                PESc.Punctuality.Calculation = excelSheet.GetValue<double>(68, 9);
                PESc.Policies.Calculation = excelSheet.GetValue<double>(69, 9);
                PESc.Values.Calculation = excelSheet.GetValue<double>(70, 9);
                #endregion

                #region Totals and subtotals - insert
                PESc.ScoreQuality.Calculation = excelSheet.GetValue<double>(30, 9);
                PESc.ScoreOpportunity.Calculation = excelSheet.GetValue<double>(36, 9);
                PESc.ScorePerformance.Calculation = PESc.Pes.PerformanceScore;

                PESc.ScoreSkills.Calculation = excelSheet.GetValue<double>(50, 9);
                PESc.ScoreInterpersonal.Calculation = excelSheet.GetValue<double>(57, 9);
                PESc.ScoreGrowth.Calculation = excelSheet.GetValue<double>(65, 9);
                PESc.ScorePolicies.Calculation = excelSheet.GetValue<double>(71, 9);
                PESc.ScoreCompetences.Calculation = PESc.Pes.CompeteneceScore;
                PESc.EnglishScore.Calculation = excelSheet.GetValue<double>(72, 9);

                bool scoreOneInserted = _scoreService.InsertScore(PESc.One);
                bool scoreTwoInserted = _scoreService.InsertScore(PESc.Two);
                bool scoreThreeInserted = _scoreService.InsertScore(PESc.Three);
                bool scoreFourInserted = _scoreService.InsertScore(PESc.Four);
                bool scoreFiveInserted = _scoreService.InsertScore(PESc.Five);
                bool scoreSixInserted = _scoreService.InsertScore(PESc.Six);
                bool scoreScoreQualityInserted = _scoreService.InsertScore(PESc.ScoreQuality);

                bool scoreSevenInserted = _scoreService.InsertScore(PESc.Seven);
                bool scoreEightInserted = _scoreService.InsertScore(PESc.Eight);
                bool scoreNineInserted = _scoreService.InsertScore(PESc.Nine);
                bool scoreScoreOpportunityInserted = _scoreService.InsertScore(PESc.ScoreOpportunity);
                bool scoreScorePerformanceInserted = _scoreService.InsertScore(PESc.ScorePerformance);

                bool scoreTenInserted = _scoreService.InsertScore(PESc.Ten);
                bool scoreElevenInserted = _scoreService.InsertScore(PESc.Eleven);
                bool scoreTwelveInserted = _scoreService.InsertScore(PESc.Twelve);
                bool scoreThirteenInserted = _scoreService.InsertScore(PESc.Thirteen);
                bool scoreFourteenInserted = _scoreService.InsertScore(PESc.Fourteen);
                bool scoreFifteenInserted = _scoreService.InsertScore(PESc.Fifteen);
                bool scoreScoreSkillsInserted = _scoreService.InsertScore(PESc.ScoreSkills);

                bool scoreSixteenInserted = _scoreService.InsertScore(PESc.Sixteen);
                bool scoreSeventeenInserted = _scoreService.InsertScore(PESc.Seventeen);
                bool scoreEighteenInserted = _scoreService.InsertScore(PESc.Eighteen);
                bool scoreNineteenInserted = _scoreService.InsertScore(PESc.Nineteen);
                bool scoreScoreInterpersonalInserted = _scoreService.InsertScore(PESc.ScoreInterpersonal);

                bool scoreTwentyInserted = _scoreService.InsertScore(PESc.Twenty);
                bool scoreTwentyoneInserted = _scoreService.InsertScore(PESc.Twentyone);
                bool scoreTwentytwoInserted = _scoreService.InsertScore(PESc.Twentytwo);
                bool scoreTwentythreeInserted = _scoreService.InsertScore(PESc.Twentythree);
                bool scoreTwentyfourInserted = _scoreService.InsertScore(PESc.Twentyfour);
                bool scoreScoreGrowthInserted = _scoreService.InsertScore(PESc.ScoreGrowth);

                bool scorePunctuallityInserted = _scoreService.InsertScore(PESc.Punctuality);
                bool scorePoliciesInserted = _scoreService.InsertScore(PESc.Policies);
                bool scoreValuesInserted = _scoreService.InsertScore(PESc.Values);
                bool scoreScorePolicesInserted = _scoreService.InsertScore(PESc.ScorePolicies);
                bool scoreScoreCompetencesInserted = _scoreService.InsertScore(PESc.ScoreCompetences);
                bool scoreEnglishEvaluationInserted = _scoreService.InsertScore(PESc.EnglishScore);
                #endregion

                #region Trainning Comment - insert
                string comment1 = excelSheet.GetValue<string>(79, 2);
                PESc.comment.TrainningEmployee = (excelSheet.Cells[78, 2].Value + (string.IsNullOrEmpty(comment1) == true ? "" : ", ") + comment1);
                //PESc.comment2.TrainningEmployee = excelSheet.Cells[79, 2].Value;
                PESc.comment.TrainningEvaluator = excelSheet.GetValue<string>(80, 2);
                PESc.comment.PEId = PESc.Pes.PEId;
                #endregion

                #region Acknowledge Comment - insert
                string comment2 = excelSheet.GetValue<string>(83, 2);
                PESc.comment.AcknowledgeEvaluator = (excelSheet.Cells[82, 2].Value + (string.IsNullOrEmpty(comment2) == true ? "" : ", ") + comment2);
                //PESc.comment2.AcknowledgeEvaluator = excelSheet.Cells[83, 2].Value;
                #endregion

                #region Comments and recommendations - insert
                string comment3 = excelSheet.GetValue<string>(86, 2);
                string comment4 = excelSheet.GetValue<string>(87, 2);
                string comment5 = excelSheet.GetValue<string>(89, 2);
                string comment6 = excelSheet.GetValue<string>(90, 2);
                PESc.comment.CommRecommEmployee = (excelSheet.Cells[85, 2].Value + (string.IsNullOrEmpty(comment3) == true ? "" : ", ") + comment3 + (string.IsNullOrEmpty(comment4) == true ? "" : ", ") + comment4);
                //PESc.comment2.CommRecommEmployee = excelSheet.Cells[86, 2].Value;
                //PESc.comment3.CommRecommEmployee = excelSheet.Cells[87, 2].Value;
                PESc.comment.CommRecommEvaluator = (excelSheet.Cells[88, 2].Value + (string.IsNullOrEmpty(comment5) == true ? "" : ", ") + comment5 + (string.IsNullOrEmpty(comment6) == true ? "" : ", ") + comment6);
                //PESc.comment2.CommRecommEvaluator = excelSheet.Cells[89, 2].Value;
                //PESc.comment3.CommRecommEvaluator = excelSheet.Cells[90, 2].Value;

                bool commentInserted = _commentService.InsertComment(PESc.comment);
                #endregion

                #region Skill description - insert
                //PESc.skill1.Description = excelSheet.Cells[96, 2].Value;
                //PESc.skill2.Description = excelSheet.Cells[97, 2].Value;
                //PESc.skill3.Description = excelSheet.Cells[98, 2].Value;
                //PESc.skill4.Description = excelSheet.Cells[99, 2].Value;
                //PESc.skill5.Description = excelSheet.Cells[100, 2].Value;
                //PESc.skill6.Description = excelSheet.Cells[101, 2].Value;
                //PESc.skill7.Description = excelSheet.Cells[102, 2].Value;
                //PESc.skill8.Description = excelSheet.Cells[103, 2].Value;
                //PESc.skill9.Description = excelSheet.Cells[104, 2].Value;
                //PESc.skill10.Description = excelSheet.Cells[105, 2].Value;
                //PESc.skill11.Description = excelSheet.Cells[106, 2].Value;
                //PESc.skill12.Description = excelSheet.Cells[107, 2].Value;
                //PESc.skill13.Description = excelSheet.Cells[108, 2].Value;
                //PESc.skill14.Description = excelSheet.Cells[109, 2].Value;
                //PESc.skill15.Description = excelSheet.Cells[110, 2].Value;
                //PESc.skill16.Description = excelSheet.Cells[111, 2].Value;
                //PESc.skill17.Description = excelSheet.Cells[112, 2].Value;

                //PESc.skill1.Description = SkillsSection.SupervisesSkill1.Description;
                //PESc.skill2.Description = SkillsSection.CoordinatesSkill2.Description;
                //PESc.skill3.Description = SkillsSection.DefinesSkill3.Description;
                //PESc.skill4.Description = SkillsSection.SupportsSkill4.Description;
                //PESc.skill5.Description = SkillsSection.KeepsSkill5.Description;
                //PESc.skill6.Description = SkillsSection.GeneratesSkill6.Description;
                //PESc.skill7.Description = SkillsSection.TrainsSkill7.Description;
                //PESc.skill8.Description = SkillsSection.SupportsSkill8.Description;
                //PESc.skill9.Description = SkillsSection.EvaluatesSkill9.Description;
                //PESc.skill10.Description = SkillsSection.FacesSkill10.Description;
                //PESc.skill11.Description = SkillsSection.SupportsSkill11.Description;
                //PESc.skill12.Description = SkillsSection.HelpsSkill12.Description;
                //PESc.skill13.Description = SkillsSection.InstillsSkill13.Description;
                //PESc.skill14.Description = SkillsSection.SetsSkill14.Description;
                //PESc.skill15.Description = SkillsSection.SupportsSkill15.Description;
                //PESc.skill16.Description = SkillsSection.WelcomesSkill16.Description;
                //PESc.skill17.Description = SkillsSection.SetsSkill17.Description;

                //bool skill1Inserted = _skillService.InsertSkill(PESc.skill1);
                //bool skill2Inserted = _skillService.InsertSkill(PESc.skill2);
                //bool skill3Inserted = _skillService.InsertSkill(PESc.skill3);
                //bool skill4Inserted = _skillService.InsertSkill(PESc.skill4);
                //bool skill5Inserted = _skillService.InsertSkill(PESc.skill5);
                //bool skill6Inserted = _skillService.InsertSkill(PESc.skill6);
                //bool skill7Inserted = _skillService.InsertSkill(PESc.skill7);
                //bool skill8Inserted = _skillService.InsertSkill(PESc.skill8);
                //bool skill9Inserted = _skillService.InsertSkill(PESc.skill9);
                //bool skill10Inserted = _skillService.InsertSkill(PESc.skill10);
                //bool skill11Inserted = _skillService.InsertSkill(PESc.skill11);
                //bool skill12Inserted = _skillService.InsertSkill(PESc.skill12);
                //bool skill13Inserted = _skillService.InsertSkill(PESc.skill13);
                //bool skill14Inserted = _skillService.InsertSkill(PESc.skill14);
                //bool skill15Inserted = _skillService.InsertSkill(PESc.skill15);
                //bool skill16Inserted = _skillService.InsertSkill(PESc.skill16);
                //bool skill17Inserted = _skillService.InsertSkill(PESc.skill17);
                #endregion

                #region skill check employee - insert
                PESc.Supervises.CheckEmployee = excelSheet.GetValue<string>(96, 6);
                PESc.Supervises.SkillId = SkillsSection.SupervisesSkill1.SkillId;
                PESc.Coordinates.CheckEmployee = excelSheet.GetValue<string>(97, 6);
                PESc.Coordinates.SkillId = SkillsSection.CoordinatesSkill2.SkillId;
                PESc.Defines.CheckEmployee = excelSheet.GetValue<string>(98, 6);
                PESc.Defines.SkillId = SkillsSection.DefinesSkill3.SkillId;
                PESc.Supports.CheckEmployee = excelSheet.GetValue<string>(99, 6);
                PESc.Supports.SkillId = SkillsSection.SupportsSkill4.SkillId;
                PESc.Keeps.CheckEmployee = excelSheet.GetValue<string>(100, 6);
                PESc.Keeps.SkillId = SkillsSection.KeepsSkill5.SkillId;
                PESc.Generates.CheckEmployee = excelSheet.GetValue<string>(101, 6);
                PESc.Generates.SkillId = SkillsSection.GeneratesSkill6.SkillId;
                PESc.Trains.CheckEmployee = excelSheet.GetValue<string>(102, 6);
                PESc.Trains.SkillId = SkillsSection.TrainsSkill7.SkillId;
                PESc.SupportsExperimentation.CheckEmployee = excelSheet.GetValue<string>(103, 6);
                PESc.SupportsExperimentation.SkillId = SkillsSection.SupportsSkill8.SkillId;
                PESc.Evaluates.CheckEmployee = excelSheet.GetValue<string>(104, 6);
                PESc.Evaluates.SkillId = SkillsSection.EvaluatesSkill9.SkillId;
                PESc.Faces.CheckEmployee = excelSheet.GetValue<string>(105, 6);
                PESc.Faces.SkillId = SkillsSection.FacesSkill10.SkillId;
                PESc.SupportsResponsible.CheckEmployee = excelSheet.GetValue<string>(106, 6);
                PESc.SupportsResponsible.SkillId = SkillsSection.SupportsSkill11.SkillId;
                PESc.Helps.CheckEmployee = excelSheet.GetValue<string>(107, 6);
                PESc.Helps.SkillId = SkillsSection.HelpsSkill12.SkillId;
                PESc.Instills.CheckEmployee = excelSheet.GetValue<string>(108, 6);
                PESc.Instills.SkillId = SkillsSection.InstillsSkill13.SkillId;
                PESc.Sets.CheckEmployee = excelSheet.GetValue<string>(109, 6);
                PESc.Sets.SkillId = SkillsSection.SetsSkill14.SkillId;
                PESc.SupportsUseful.CheckEmployee = excelSheet.GetValue<string>(110, 6);
                PESc.SupportsUseful.SkillId = SkillsSection.SupportsSkill15.SkillId;
                PESc.Welcomes.CheckEmployee = excelSheet.GetValue<string>(111, 6);
                PESc.Welcomes.SkillId = SkillsSection.WelcomesSkill16.SkillId;
                PESc.SetsSpecific.CheckEmployee = excelSheet.GetValue<string>(112, 6);
                PESc.SetsSpecific.SkillId = SkillsSection.SetsSkill17.SkillId;
                #endregion

                #region skill check evaluator - insert
                PESc.Supervises.CheckEvaluator = excelSheet.GetValue<string>(96, 7);
                PESc.Supervises.PEId = PESc.Pes.PEId;
                PESc.Coordinates.CheckEvaluator = excelSheet.GetValue<string>(97, 7);
                PESc.Coordinates.PEId = PESc.Pes.PEId;
                PESc.Defines.CheckEvaluator = excelSheet.GetValue<string>(98, 7);
                PESc.Defines.PEId = PESc.Pes.PEId;
                PESc.Supports.CheckEvaluator = excelSheet.GetValue<string>(99, 7);
                PESc.Supports.PEId = PESc.Pes.PEId;
                PESc.Keeps.CheckEvaluator = excelSheet.GetValue<string>(100, 7);
                PESc.Keeps.PEId = PESc.Pes.PEId;
                PESc.Generates.CheckEvaluator = excelSheet.GetValue<string>(101, 7);
                PESc.Generates.PEId = PESc.Pes.PEId;
                PESc.Trains.CheckEvaluator = excelSheet.GetValue<string>(102, 7);
                PESc.Trains.PEId = PESc.Pes.PEId;
                PESc.SupportsExperimentation.CheckEvaluator = excelSheet.GetValue<string>(103, 7);
                PESc.SupportsExperimentation.PEId = PESc.Pes.PEId;
                PESc.Evaluates.CheckEvaluator = excelSheet.GetValue<string>(104, 7);
                PESc.Evaluates.PEId = PESc.Pes.PEId;
                PESc.Faces.CheckEvaluator = excelSheet.GetValue<string>(105, 7);
                PESc.Faces.PEId = PESc.Pes.PEId;
                PESc.SupportsResponsible.CheckEvaluator = excelSheet.GetValue<string>(106, 7);
                PESc.SupportsResponsible.PEId = PESc.Pes.PEId;
                PESc.Helps.CheckEvaluator = excelSheet.GetValue<string>(107, 7);
                PESc.Helps.PEId = PESc.Pes.PEId;
                PESc.Instills.CheckEvaluator = excelSheet.GetValue<string>(108, 7);
                PESc.Instills.PEId = PESc.Pes.PEId;
                PESc.Sets.CheckEvaluator = excelSheet.GetValue<string>(109, 7);
                PESc.Sets.PEId = PESc.Pes.PEId;
                PESc.SupportsUseful.CheckEvaluator = excelSheet.GetValue<string>(110, 7);
                PESc.SupportsUseful.PEId = PESc.Pes.PEId;
                PESc.Welcomes.CheckEvaluator = excelSheet.GetValue<string>(111, 7);
                PESc.Welcomes.PEId = PESc.Pes.PEId;
                PESc.SetsSpecific.CheckEvaluator = excelSheet.GetValue<string>(112, 7);
                PESc.SetsSpecific.PEId = PESc.Pes.PEId;

                bool SupervicesSkill = _lm_skillService.InsertLM_Skill(PESc.Supervises);
                bool CoordinatesSkill = _lm_skillService.InsertLM_Skill(PESc.Coordinates);
                bool DefinesSkill = _lm_skillService.InsertLM_Skill(PESc.Defines);
                bool SupportsSkill = _lm_skillService.InsertLM_Skill(PESc.Supports);
                bool KeepsSkill = _lm_skillService.InsertLM_Skill(PESc.Keeps);
                bool GeneratesSkill = _lm_skillService.InsertLM_Skill(PESc.Generates);
                bool TrainsSkill = _lm_skillService.InsertLM_Skill(PESc.Trains);
                bool SupportsExperimentationSkill = _lm_skillService.InsertLM_Skill(PESc.SupportsExperimentation);
                bool EvaluatesSkill = _lm_skillService.InsertLM_Skill(PESc.Evaluates);
                bool FacesSkill = _lm_skillService.InsertLM_Skill(PESc.Faces);
                bool SupportsResponsibleSkill = _lm_skillService.InsertLM_Skill(PESc.SupportsResponsible);
                bool HelpsSkill = _lm_skillService.InsertLM_Skill(PESc.Helps);
                bool InstillsSkill = _lm_skillService.InsertLM_Skill(PESc.Instills);
                bool SetsSkill = _lm_skillService.InsertLM_Skill(PESc.Sets);
                bool SupportsUsefulSkill = _lm_skillService.InsertLM_Skill(PESc.SupportsUseful);
                bool WelcomesSkill = _lm_skillService.InsertLM_Skill(PESc.Welcomes);
                bool SetsSpecificSkill = _lm_skillService.InsertLM_Skill(PESc.SetsSpecific);
                #endregion

                /*excel.Workbooks.Close();
                excel.Quit();*/
                return true;
            }
            catch (Exception ex)
            {
                //excel.Workbooks.Close();
                //excel.Quit();
                throw;
            }
            
        }

        [HttpPost]
        public ActionResult UploadFile(UploadFileViewModel uploadVM, HttpPostedFileBase fileUploaded)
        {
            string message = "";

            //Excel.Application excel = new Excel.Application();

            #region New Upload File
            // Check file was submitted             
            if (fileUploaded != null && fileUploaded.ContentLength > 0)
            {
                if (fileUploaded.FileName.EndsWith("xls") || fileUploaded.FileName.EndsWith("xlsx") || fileUploaded.FileName.EndsWith("XLSX") || fileUploaded.FileName.EndsWith("XLS")) 
                {
                    string fname = "";
                    string fullPath = "";

                    //Store file temporary
                    fname = Path.GetFileName(fileUploaded.FileName);
                    fileUploaded.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));

                    //Get full path of the file 
                    fullPath = Request.MapPath("~/App_Data/" + fname);

                    try
                    {
                        // Load file into database
                        // Get selected user
                        var user = _employeeService.GetByID(uploadVM.SelectedEmployee);
                        var year = uploadVM.SelectedYear;
                        var period = uploadVM.SelectedPeriod;
                        var replace = uploadVM.Replace;

                        if (user != null)
                        {
                            var evaluator = _employeeService.GetByID(uploadVM.SelectedEvaluator);

                            bool fileSaved = ReadPerformanceFile(fullPath, user, evaluator, year, period, replace);

                            if (!fileSaved)
                            {
                                // File not saved
                                TempData["Error"] = "There were some problems saving the file. Please try again later.";
                            }
                            else
                            {
                                // File saved successfully
                                TempData["Success"] = "File saved successfully";
                            }
                        }
                        else 
                        {
                            TempData["Error"] = "Selected resource not found. Please select a valid resource.";
                        }
                    }
                    finally
                    {
                        //Delete temporary file, always delete file already stored 
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }
                else
                {
                    TempData["Error"] = "File is not a valid excel file";
                }
            }
            else 
            {
                // No file uploaded
                TempData["Error"] = "No File Uploaded";
            }
            #endregion

            return RedirectToAction("UploadFile");
            #region old upload file
            //if (fileUploaded == null || fileUploaded.ContentLength == 0)
            //{
            //    //ViewBag.Error = "Please Select  a excel file<br>";
            //    return View("UploadFile");
            //}
            //else
            //{
            //    if (fileUploaded.FileName.EndsWith("xls") || fileUploaded.FileName.EndsWith("xlsx"))
            //    {
            //        string path = Server.MapPath("~/Content/" + fileUploaded.FileName);
            //        if (System.IO.File.Exists(path))
            //        {
            //            excel.Quit();
            //            System.IO.File.Delete(path);
            //        }

            //        //Save the file in the repository   
            //        fileUploaded.SaveAs(path);


            //        #region comment for now
            //        try
            //        {
            //            // Get selected user
            //            var user = _employeeService.GetByID(uploadVM.SelectedEmployee);

            //            if (user != null)
            //            {
            //                var evaluator = _employeeService.GetByID(user.ManagerId);

            //                bool fileSaved = ReadPerformanceFile(path, user, evaluator);

            //                if (!fileSaved)
            //                {
            //                    // File not saved
            //                    TempData["Error"] = "There were some problems saving the file. Please try again later.";
            //                }
            //                else
            //                {
            //                    // File saved successfully
            //                    TempData["Success"] = "File saved successfully";
            //                }
            //            }
            //        }
            //        finally
            //        {
            //            //Delete the file from the repository
            //            if (System.IO.File.Exists(path))
            //            {
            //                excel.Quit();
            //                System.IO.File.Delete(path);
            //            }
            //        }
            //        #endregion

            //        return RedirectToAction("UploadFile");
            //    }
            //    else
            //    {
            //        //ViewBag.Error = "File type is incorrect<br>";
            //        TempData["Error"] = "File is not a valid excel file";

            //        return RedirectToAction("UploadFile");
            //    }

            //}
            #endregion
        }
        
        // GET: PerformanceEvaluation/UploadFile
        public ActionResult UploadFile()
        {
            //Get Current user
            var currentProfile = _employeeService.GetByEmail(Session["UserEmail"].ToString());
            if (currentProfile.ProfileId == (int)ProfileUser.Director || currentProfile.ProfileId == (int)ProfileUser.Manager)
            {
                UploadFileViewModel uploadVM = new UploadFileViewModel();

                // Get current user by using the session
                Employee currentUser = new Employee();
                EmployeeService employeeService = new EmployeeService();
                currentUser = employeeService.GetByEmail((string)Session["UserEmail"]);
                uploadVM.CurrentUser = currentUser;


                // Build list of employees (to evaluate)
                #region List of Employees
                List<Employee> listEmployees = new List<Employee>();
                if (currentUser.ProfileId == (int)ProfileUser.Director)
                {
                    // Get all employees 
                    listEmployees = _employeeService.GetAll();
                }
                else if (currentUser.ProfileId == (int)ProfileUser.Manager)
                {
                    // Get employees by manager
                    listEmployees = null;
                    listEmployees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                }
                List<SelectListItem> employeesList = new List<SelectListItem>();
                foreach (var employee in listEmployees)
                {
                    var newItem = new SelectListItem()
                    {
                        Text = employee.FirstName + " " + employee.LastName + " (" + employee.Email + ")",
                        Value = (employee.EmployeeId).ToString(),
                        Selected = false
                    };
                    employeesList.Add(newItem);
                }
                #endregion

                //Build list of all employees (evaluators)
                #region List of Evaluators
                List<Employee> listAllEmployees = new List<Employee>();
                listAllEmployees = _employeeService.GetAll();

                List<SelectListItem> employeesAllList = new List<SelectListItem>();
                foreach (var evaluator in listAllEmployees)
                {
                    var newItem = new SelectListItem()
                    {
                        Text = evaluator.FirstName + " " + evaluator.LastName + " (" + evaluator.Email + ")",
                        Value = (evaluator.EmployeeId).ToString(),
                        Selected = false
                    };
                    employeesAllList.Add(newItem);
                }
                #endregion

                // Build list of periods
                #region List of Periods
                List<SelectListItem> ListPeriods = new List<SelectListItem>();
                var periods = _periodService.GetAll();

                foreach (var item in periods)
                {
                    var period = new SelectListItem
                    {
                        Text = "Period " + item.PeriodId + "(" + item.Name + ")",
                        Value = item.PeriodId.ToString(),
                        Selected = false
                    };

                    ListPeriods.Add(period);
                }


                #endregion

                // Build list of years
                #region List of years
                List<SelectListItem> listYears = new List<SelectListItem>();
                int currentYear = int.Parse(DateTime.Now.Year.ToString());
                int currentPeriod = int.Parse(DateTime.Now.Month.ToString());
                var minYear = 2014;
                var maxYear = currentYear;
                //Get currentPeriod id
                if (currentPeriod < 7)
                {
                    currentPeriod = 1;
                }
                else
                {
                    currentPeriod = 2;
                }

                for (var i = minYear; i <= maxYear; i++)
                {
                    if (currentYear == i)
                    {
                        var year = new SelectListItem()
                        {
                            Text = i.ToString(),
                            Value = i.ToString(),
                            Selected = true
                        };
                        listYears.Add(year);
                    }
                    else
                    {
                        var year = new SelectListItem()
                        {
                            Text = i.ToString(),
                            Value = i.ToString(),
                            Selected = false
                        };
                        listYears.Add(year);
                    }
                }
                #endregion

                uploadVM.ListAllEmployees = employeesAllList;
                uploadVM.ListEmployees = employeesList;
                uploadVM.PeriodList = ListPeriods;
                uploadVM.ListYears = listYears;
                //uploadVM.SelectedPeriod = int.Parse(ListPeriods.LastOrDefault(p => p.Value == currentPeriod.ToString()).Value); // last 
                uploadVM.SelectedYear = int.Parse(listYears.LastOrDefault(y => y.Value == currentYear.ToString()).Value);


                return View(uploadVM);
            }
            else if(currentProfile.ProfileId == (int)ProfileUser.Resource)
            {
                TempData["Error"] = "You are not allowed to view this page";
               return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "You are not loged in. Please try later.";
                return RedirectToAction("Login", "LoginUser");
            }
        }

        // GET: PeformanceEvaluation/SearchIformation
        [HttpGet]
        public ActionResult SearchInformation()
        {
            // Get current users by using email in Session
            // Get current user 
            Employee currentUser = new Employee();
            var userEmail = (string)Session["UserEmail"];

            currentUser = _employeeService.GetByEmail(userEmail);

            if(currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home 
                // send error
                return RedirectToAction("ChoosePeriod");
            }

            PerformanceFilesPartial model = FillPerformancePartial(currentUser);
            
            return View(model);
        }

        // GET: PerformanceEvaluation/ChoosePeriod
        [HttpGet]
        public ActionResult ChoosePeriod(string employeeEmail, int employeeID)
        {
            //Get current user
            Employee currentUser = new Employee();
            var currentuserEmail = (string)Session["UserEmail"];
            currentUser = _employeeService.GetByEmail(currentuserEmail);

            // Get user
            var user = _employeeService.GetByEmail(employeeEmail);
            var userid = _employeeService.GetByID(employeeID);
            // -- Get performance evaluation data
            // Get performance evaluation 
            List<PEs> listUserPE = _peService.GetPerformanceEvaluationByUserID(employeeID);

            //decimal totalEvaluation = _scoreService.GetScoreByPE(userPE.PEId);
            
            List<EmployeeChoosePeriodViewModel> choosePeriodVM = new List<EmployeeChoosePeriodViewModel>();

            if(listUserPE != null && listUserPE.Count > 0)
            {
                foreach(var userPE in listUserPE)
                {
                    var chooseVM = new EmployeeChoosePeriodViewModel
                    {
                        EmployeeId = userPE.EmployeeId,
                        PESId = userPE.PEId,

                        Period = userPE.EvaluationPeriod,
                        TotalEvaluation = userPE.Total,
                        TotalEnglish = userPE.EnglishScore,
                        TotalPerforformance = userPE.PerformanceScore,
                        TotalCompetences = userPE.CompeteneceScore,
                        EvaluationYear = userPE.EvaluationYear,
                        PeriodName = _periodService.GetPeriodById(userPE.PeriodId)
                    };

                    choosePeriodVM.Add(chooseVM);
                }
            }

            ViewBag.UserName = user.FirstName + " " + user.LastName;
            ViewBag.UserEmail = employeeEmail;
            ViewBag.UserID = employeeID;
            ViewBag.CurrentUserProfile = currentUser.ProfileId;
            return View(choosePeriodVM);
        }

        //GET: PerformanceEvaluation/PEVisualization
        public ActionResult PEVisualization(int peID)
        {
            List<PerformanceSectionHelper> listPerformancHelper = new List<PerformanceSectionHelper>();
            _peService = new PEService();

            // Get performance evaluation 
            var pe = _peService.GetPerformanceEvaluationById(peID);
            var employee = _employeeService.GetByID(pe.EmployeeId);
            var evaluator = _employeeService.GetByID(pe.EvaluatorId);
            
            listPerformancHelper = _peService.GetPerformanceEvaluationByIDPE(peID);
            
            // Complete view PEVisualization
            PEVisualizationViewModel model = new PEVisualizationViewModel();

            model.PerformanceMain = new PerformanceMainPartial();
            model.PerformanceMain.Employee = employee;
            model.PerformanceMain.Evaluator = evaluator;
            model.PerformanceMain.TotalEvaluation = pe.Total;

            // Initialize sections
            model.PerformanceSections = new PerformanceSectionsPartial();
            model.PerformanceSections.Sections = listPerformancHelper;

            // Initializae comments
            model.PerformanceComments = new PerformanceCommentsPartial();
            model.PerformanceComments.Comments = _commentService.GetCommentByPE(peID);

            // Initializae skills
            model.PerformanceSkills = new PerformanceSkilsPartial();
            model.PerformanceSkills.Skills = _lm_skillService.GetSkillsWithNameByPEId(peID);

            #region 
            /*
            #region Subtitles
            // Peformance Quality Subtitle 
            peComplete.Subtitle1 = PerformanceSection.QualitySubtitle;
            // Performance Opportuniyy Subtitle 
            peComplete.Subtitle2 = PerformanceSection.OpportunitySubtitle;
            #endregion

            #region Descriptions
            // Performance Quality Descriptions
            peComplete.Description1 = PerformanceSection.AccuracyQualityDescription1;
            peComplete.Description2 = PerformanceSection.ThoroughnessQualityDescription2;
            peComplete.Description3 = PerformanceSection.ReliabilityQualityDescription3;
            peComplete.Description4 = PerformanceSection.ResponsivenessQualityDescription4;
            peComplete.Description5 = PerformanceSection.FollowQualityDescription5;
            peComplete.Description6 = PerformanceSection.JudgmentQualityDescription6;
            peComplete.SubtotalDescQuality = PerformanceSection.SubtotalQualityDescription7;
            
            //Performance Opportunity Description
            peComplete.Description7 = PerformanceSection.PriorityOpportunityDescription8;
            peComplete.Description8 = PerformanceSection.AmountOpportunityDescription9;
            peComplete.Description9 = PerformanceSection.WorkOpportunityDescription10;
            peComplete.SubtotalOpportunity = PerformanceSection.SubtotalOpportunityDescription11;
            #endregion

            #region Performance Scores
            // Quality Scores
            
            //Accuracy
            peComplete.One = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.AccuracyQualityDescription1.DescriptionId);
            //Thoroughness
            peComplete.Two = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.ThoroughnessQualityDescription2.DescriptionId);
            //Reliability
            peComplete.Three = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.ReliabilityQualityDescription3.DescriptionId);
            //Responsiveness
            peComplete.Four = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.ResponsivenessQualityDescription4.DescriptionId);
            //Follow
            peComplete.Five = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.FollowQualityDescription5.DescriptionId);
            //Judgment/Decision
            peComplete.six = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.JudgmentQualityDescription6.DescriptionId);
            // Quality Subtotal
            peComplete.SubtotalDescQuality = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.SubtotalQualityDescription7.DescriptionId);

            #endregion
            */
            #endregion

            // Get score of 1. Accuracy or Precision
            // var score = _scoreService.GetPEScoreByPEIdDescId(pe.PEId, PerformanceSection.);

            return View(model);
        }

        // GET: PerformanceEvaluation/SearchInfoRank
        public ActionResult SearchInfoRank()
        {
            return View();
        }


        public ActionResult UpdateRank(List<PerformanceRankHelper> listPerformances)
        {
            int countUpdated = 0;

            // Update each performance record
            foreach (var peformance in listPerformances)
            {
                var updated = _peService.UpdateRank(peformance.PerformanceId, peformance.RankValue);
                if (updated)
                {
                    countUpdated++;
                }
            }

            var userEmail = (string)Session["UserEmail"];

            Employee currentUser = _employeeService.GetByEmail(userEmail);

            PerformanceFilesPartial partial = FillPerformancePartial(currentUser);
            partial.CountRankUpdated = countUpdated;

            return PartialView("_PerformanceFilesPartial", partial);
        }

        public PerformanceFilesPartial FillPerformancePartial(Employee currentUser) 
        {
            currentUser = _employeeService.GetByEmail(currentUser.Email);

            List<Employee> employees = new List<Employee>();
            if (currentUser.ProfileId == (int)ProfileUser.Director)
            {
                // Get all
                employees = _employeeService.GetAll();
            }
            else if (currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                // Get by manager 
                employees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                employees = employees.Where(x => x.EmployeeId != currentUser.EmployeeId).ToList();
            }

            List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
            var location = new Location();
            foreach (var employee in employees)
            {
                location = _locationService.GetPeriodById(employee.LocationId);
                var employeeVM = new EmployeeManagerViewModel();
                employeeVM.Employee = employee;
                employeeVM.Manager = _employeeService.GetByID(employee.ManagerId);
                employeeVM.Location = location;

                var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);
                var currentEvaluation = getCurrentPeriod();
                currentEvaluation.Split(',');
                if (listPE != null && listPE.Count > 0)
                {
                    var lastPE = listPE.OrderByDescending(pe => pe.PeriodId == (int)currentEvaluation[0] && pe.EvaluationYear == (int)currentEvaluation[1]).LastOrDefault();

                    employeeVM.LastPe = lastPE;
                }

                listEmployeeVM.Add(employeeVM);

            }

            var periods = _peService.GetAllPeriods();
            var years = _peService.GetAllYears();
            var ListPeriods = new List<SelectListItem>();
            var ListYears = new List<SelectListItem>();
            foreach (var item in periods)
            {
                var newPeriod = new SelectListItem
                {
                    Text = "Period " + item.PeriodId + "(" + item.Name + ")",
                    Value = item.PeriodId.ToString(),
                    Selected = false
                };
                ListPeriods.Add(newPeriod);
            }
            foreach(var item in years)
            {
                var newYear = new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString(),
                    Selected = false
                };
                ListYears.Add(newYear);
            }

            PerformanceFilesPartial partial = new PerformanceFilesPartial(listEmployeeVM, currentUser);

            partial.ListPeriods = ListPeriods;
            partial.ListYear = ListYears;

            return partial;
        }

        private string getCurrentPeriod()
        {
            var currentPeriod = (int)DateTime.Now.Month;
            var currentYear = (int)DateTime.Now.Year;

            if(currentPeriod < 7)
            {
                currentPeriod = 1;
            }
            else
            {
                currentPeriod = 2;
            }

            return currentPeriod.ToString() + "," + currentYear.ToString();
            
        }
        public JsonResult VerifyPE(int employee, int evaluator, int period, int year)
        {
            var success = false;
            //Get evaluation conscidence Id
            var idPe = _peService.VerifyPE(employee, evaluator, period, year);
            if(idPe != 0)
            {
                success = true;
            }
            //Return startus of searcha and Id PE
            return Json(new { exist = success, idPe = idPe},JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployeesByFilter(int FilterId, int OptionId)
        {
            //Get current user by email
            var currentUser = _employeeService.GetByEmail(Session["UserEmail"].ToString());
            if (FilterId == 1)
            {
                List<Employee> employees = new List<Employee>();
                if (currentUser.ProfileId == (int)ProfileUser.Director)
                {
                    // Get all employees
                    employees = _employeeService.GetAll();
                    employees = filterByLocation(employees, OptionId);
                }
                else if (currentUser.ProfileId == (int)ProfileUser.Manager)
                {
                    // Get employees by manager 
                    employees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                    employees = filterByLocation(employees, OptionId);
                }

                List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
                var location = new Location();
                foreach (var employee in employees)
                {
                    //Get employee location
                    location = _locationService.GetPeriodById(employee.LocationId);
                    var employeeVM = new EmployeeManagerViewModel();
                    employeeVM.Employee = employee;
                    employeeVM.Manager = _employeeService.GetByID(employee.ManagerId);
                    employeeVM.Location = location;

                    var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);
                    var currentEvaluation = getCurrentPeriod();
                    currentEvaluation.Split(',');

                    if (listPE != null && listPE.Count > 0)
                    {
                        //Filter PEs to get the last One
                        var lastPE = listPE.OrderByDescending(pe => pe.PeriodId == (int)currentEvaluation[0] && pe.EvaluationYear == (int)currentEvaluation[1]).LastOrDefault();

                        employeeVM.LastPe = lastPE;
                    }

                    listEmployeeVM.Add(employeeVM);
                }

                PerformanceFilesPartial partial = new PerformanceFilesPartial(listEmployeeVM, currentUser);

                return PartialView("_PerformanceFilesPartial", partial);
            }
            else if(FilterId == 2)
            {
                List<Employee> employees = new List<Employee>();
                if (currentUser.ProfileId == (int)ProfileUser.Director)
                {
                    // Get all and filter them
                    //employees = _employeeService.GetAll();
                    //filterByManager(employees, OptionId);

                    //Get emplyees by manager
                    employees = _employeeService.GetEmployeeByManager(OptionId);

                    //filter Manager from list
                    employees = employees.Where(x => x.ManagerId == OptionId).ToList();
                }
                else if (currentUser.ProfileId == (int)ProfileUser.Manager)
                {
                    // Get by manager 
                    employees = _employeeService.GetEmployeeByManager(OptionId);
                    //filter Manager from list
                    employees = employees.Where(x => x.ManagerId == OptionId).ToList();
                }

                List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
                var location = new Location();
                foreach (var employee in employees)
                {
                    //Get employee ocation
                    location = _locationService.GetPeriodById(employee.LocationId);
                    var employeeVM = new EmployeeManagerViewModel();
                    employeeVM.Employee = employee;
                    employeeVM.Manager = _employeeService.GetByID(employee.ManagerId);
                    employeeVM.Location = location;
                    //Get all employees PEs
                    var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);
                    var currentEvaluation = getCurrentPeriod();
                    currentEvaluation.Split(',');

                    if (listPE != null && listPE.Count > 0)
                    {
                        //Filter PEs to get the last One
                        var lastPE = listPE.OrderByDescending(pe => pe.PeriodId == (int)currentEvaluation[0] && pe.EvaluationYear == (int)currentEvaluation[1]).LastOrDefault();

                        employeeVM.LastPe = lastPE;
                    }

                    listEmployeeVM.Add(employeeVM);
                }

                PerformanceFilesPartial partial = new PerformanceFilesPartial(listEmployeeVM, currentUser);
                return PartialView("_PerformanceFilesPartial", partial);
            }
            else if(FilterId == 3)//get report by directors
            {
                List<Employee> employees = new List<Employee>();
                if (currentUser.ProfileId == (int)ProfileUser.Director)
                {
                    // Get all
                    //employees = _employeeService.GetAll();
                    //filterByDirector(employees, OptionId);

                    //Get by director
                    employees = _employeeService.GetEmployeesByDirector(OptionId);
                    employees = employees.Where(x => x.EmployeeId != OptionId).ToList();
                }
                else if (currentUser.ProfileId == (int)ProfileUser.Manager)
                {
                    // Get by manager 
                    employees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                    employees = employees.Where(x => x.EmployeeId != OptionId).ToList();
                }

                List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
                var location = new Location();
                foreach (var employee in employees)
                {
                    //Get employee Lcation
                    location = _locationService.GetPeriodById(employee.LocationId);
                    var employeeVM = new EmployeeManagerViewModel();
                    employeeVM.Employee = employee;
                    employeeVM.Manager = _employeeService.GetByID(employee.ManagerId);
                    employeeVM.Location = location;

                    var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);
                    var currentEvaluation = getCurrentPeriod();
                    currentEvaluation.Split(',');

                    if (listPE != null && listPE.Count > 0)
                    {
                        //Filter PEs to get the last One
                        var lastPE = listPE.OrderByDescending(pe => pe.PeriodId == (int)currentEvaluation[0] && pe.EvaluationYear == (int)currentEvaluation[1]).LastOrDefault();

                        employeeVM.LastPe = lastPE;
                    }

                    listEmployeeVM.Add(employeeVM);
                }

                PerformanceFilesPartial partial = new PerformanceFilesPartial(listEmployeeVM, currentUser);
                return PartialView("_PerformanceFilesPartial", partial);
            }
            else //Get general report of all company employees
            {
                currentUser = _employeeService.GetByEmail(currentUser.Email);

                List<Employee> employees = new List<Employee>();
                if (currentUser.ProfileId == (int)ProfileUser.Director)
                {
                    // Get all
                    employees = _employeeService.GetAll();
                    employees = filterByLocation(employees, OptionId);
                }
                else if (currentUser.ProfileId == (int)ProfileUser.Manager)
                {
                    // Get by manager 
                    employees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                    employees = employees.Where(x => x.EmployeeId != currentUser.EmployeeId).ToList();
                }

                List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
                var location = new Location();
                foreach (var employee in employees)
                {
                    //Get employees location
                    location = _locationService.GetPeriodById(employee.LocationId);
                    var employeeVM = new EmployeeManagerViewModel();
                    employeeVM.Employee = employee;
                    employeeVM.Manager = _employeeService.GetByID(employee.ManagerId);
                    employeeVM.Location = location;
                    //Get all employee PEs
                    var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);
                    var currentEvaluation = getCurrentPeriod();
                    currentEvaluation.Split(',');

                    if (listPE != null && listPE.Count > 0)
                    {
                        //Filter PEs to get the last One
                        var lastPE = listPE.OrderByDescending(pe => pe.PeriodId == (int)currentEvaluation[0] && pe.EvaluationYear == (int)currentEvaluation[1]).LastOrDefault();

                        employeeVM.LastPe = lastPE;
                    }

                    listEmployeeVM.Add(employeeVM);
                }

                PerformanceFilesPartial partial = new PerformanceFilesPartial(listEmployeeVM, currentUser);


                return PartialView("_PerformanceFilesPartial", partial);
            }
        }

        private List<Employee> filterByLocation(List<Employee> employees, int idLocation)
        {
            //Get user by selected location
            employees = employees.Where(x => x.LocationId == idLocation).ToList();
            return employees;
        }

        [HttpGet]
        public ActionResult ReportrsHistory()
        {
            // Get current users by using email in Session
            // Get current user 
            Employee currentUser = new Employee();
            var userEmail = (string)Session["UserEmail"];

            currentUser = _employeeService.GetByEmail(userEmail);

            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home 
                // send error
                return RedirectToAction("ChoosePeriod");
            }

            PerformanceFilesPartial model = FillPerformancePartial(currentUser);

            return View(model);
        }

        public async Task<ActionResult> GetHistoricalReport(int period, int year)
        {
            //Get curent user by email
            var currentUser = _employeeService.GetByEmail(Session["UserEmail"].ToString());
            
            currentUser = _employeeService.GetByEmail(currentUser.Email);
            List<Employee> employees = new List<Employee>();
            if (currentUser.ProfileId == (int)ProfileUser.Director)
            {
                // Get all employees
                employees = _employeeService.GetAll();
            }
            else if (currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                // Get employees by manager 
                employees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                employees = employees.Where(x => x.EmployeeId != currentUser.EmployeeId).ToList();
            }

            List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
            var location = new Location();
            foreach (var employee in employees)
            {
                //Get employees location
                location = _locationService.GetPeriodById(employee.LocationId);
                var employeeVM = new EmployeeManagerViewModel();
                employeeVM.Employee = employee;
                employeeVM.Manager = _employeeService.GetByID(employee.ManagerId);
                employeeVM.Location = location;

                //Get employee evaluations
                var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);

                if (listPE != null && listPE.Count > 0)
                {
                    //Filter evalautions to get the correct One by year and period
                    var selectedPe = listPE.OrderByDescending(pe => pe.PeriodId == period && pe.EvaluationYear == year).FirstOrDefault();
                    if(selectedPe.EvaluationYear != year || selectedPe.PeriodId != period)
                    {
                        selectedPe = null;
                    }
                    employeeVM.LastPe = selectedPe;
                }

                listEmployeeVM.Add(employeeVM);
            }
            var periods = _peService.GetAllPeriods();
            var years = _peService.GetAllYears();
            var listPeriods = new List<SelectListItem>();
            var listYears = new List<SelectListItem>();
            foreach (var item in periods)
            {
                //Set onto list registered periods on data base
                var newPeriod = new SelectListItem
                {
                    Text = "Period " + item.PeriodId + "(" + item.Name + ")",
                    Value = item.PeriodId.ToString(),
                    Selected = false
                };
                listPeriods.Add(newPeriod);
            }
            foreach (var item in years)
            {
                //Set into list years with at leat with a register
                var newYear = new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString(),
                    Selected = false
                };
                listYears.Add(newYear);
            }

            PerformanceFilesPartial partial = new PerformanceFilesPartial(listEmployeeVM, currentUser);

            partial.ListPeriods = listPeriods;
            partial.ListYear = listYears;
            
            return PartialView("_PerformanceFilesPeriodsPartial", partial);
        }
    }
}