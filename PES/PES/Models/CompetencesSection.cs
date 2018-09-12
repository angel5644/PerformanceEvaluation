using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public static class CompetencesSection
    {
        public static Title CompetenceTitle
        {
            get 
            {
                Title title = new Title { TitleId = 2, Name = "Competences" };
                return title;
            }
        }
        
        public static Subtitle SkillsSubtitle
        {
            get
            {
                Subtitle subtitle1 = new Subtitle { SubtitleId = 3, TitleId = 2, Name = "Skills and Knowledge" };
                return subtitle1;
            }
        }

        public static Subtitle InterpersonalSubtitle
        {
            get
            {
                Subtitle subtitle2 = new Subtitle { SubtitleId = 4, TitleId = 2, Name = "INTERPERSONAL SKILLS: Effectiveness of the team member's interaction with others and as a team participant" };
                return subtitle2;
            }
        }

        public static Subtitle GrowthSubtitle
        {
            get
            {
                Subtitle subtitle3 = new Subtitle { SubtitleId = 5, TitleId = 2, Name = "Growth and development: Learns new concepts and techniques, investigates and explores new work processes and/or new tools." };
                return subtitle3;
            }
        }

        public static Subtitle PoliciesSubtitle
        {
            get
            {
                Subtitle subtitle4 = new Subtitle { SubtitleId = 6, TitleId = 2, Name = "Policies Compliance: (non-disclosure, dress code )" };
                return subtitle4;
            }
        }

        public static Description JobSkillDescription13
        {
            get
            {
                Description description12 = new Description { DescriptionId = 13, SubtitleId = 3, DescriptionText = "10. Job Knowledge" };
                return description12;
            }
        }

        public static Description AnalyzesSkillDescription14
        {
            get
            {
                Description description13 = new Description { DescriptionId = 14, SubtitleId = 3, DescriptionText = "11. Analyzes Problems" };
                return description13;
            }
        }

        public static Description FlexibleSkillDescription15
        {
            get
            {
                Description description14 = new Description { DescriptionId = 15, SubtitleId = 3, DescriptionText = "12. Flexible / Adaptable" };
                return description14;
            }
        }

        public static Description PlanningSkillDescription16
        {
            get
            {
                Description description15 = new Description { DescriptionId = 16, SubtitleId = 3, DescriptionText = "13. Planning and Organization" };
                return description15;
            }
        }

        public static Description CompetentSkillDescription17
        {
            get
            {
                Description description16 = new Description { DescriptionId = 17, SubtitleId = 3, DescriptionText = "14. Competent/proper usage of work tools" };
                return description16;
            }
        }

        public static Description FollowsSkillDescription18
        {
            get
            {
                Description description17 = new Description { DescriptionId = 18, SubtitleId = 3, DescriptionText = "15. Follows proper procedures, standards and requirements" };
                return description17;
            }
        }

        public static Description SubtotalSkillDescription19
        {
            get
            {
                Description description18 = new Description { DescriptionId = 19, SubtitleId = 3, DescriptionText = "Subtotal" };
                return description18;
            }
        }
        //public static List<Description> Subtitle1Descriptions
        //{
        //    get
        //    {
        //        List<Description> listDescription = new List<Description>
        //        {
        //            new Description{DescriptionId= 13, SubtitleId = 3, DescriptionText = "10. Job Knowledge" },
        //            new Description{DescriptionId= 14, SubtitleId = 3, DescriptionText = "11. Analyzes Problems" },
        //            new Description{DescriptionId= 15, SubtitleId = 3, DescriptionText = "12. Flexible / Adaptable" },
        //            new Description{DescriptionId= 16, SubtitleId = 3, DescriptionText = "13. Planning and Organization" },
        //            new Description{DescriptionId= 17, SubtitleId = 3, DescriptionText = "14. Competent/proper usage of work tools" },
        //            new Description{DescriptionId= 18, SubtitleId = 3, DescriptionText = "15. Follows proper procedures, standards and requirements" },
        //            new Description{DescriptionId= 19, SubtitleId = 3, DescriptionText = "Subtotal" }
        //        };

        //        return listDescription;
        //    }
        //}

        public static Description SupervisorInterpersonalDescription20
        {
            get
            {
                Description description19 = new Description { DescriptionId = 20, SubtitleId = 4, DescriptionText = "16. With Supervisors" };
                return description19;
            }
        }

        public static Description OtherInterpersonalDescription21
        {
            get
            {
                Description description20 = new Description { DescriptionId = 21, SubtitleId = 4, DescriptionText = "17. With other team members / across teams" };
                return description20;
            }
        }

        public static Description ClientInterpersonalDescription22
        {
            get
            {
                Description description21 = new Description { DescriptionId = 22, SubtitleId = 4, DescriptionText = "18. With client(s)" };
                return description21;
            }
        }

        public static Description CommitmentInterpersonalDescription23
        {
            get
            {
                Description description22 = new Description { DescriptionId = 23, SubtitleId = 4, DescriptionText = "19. Commitment to Team Success" };
                return description22;
            }
        }

        public static Description SubtotalInterpersonalDescription24
        {
            get
            {
                Description description23 = new Description { DescriptionId = 24, SubtitleId = 4, DescriptionText = "Subtotal" };
                return description23;
            }
        }
        
        //public static List<Description> Subtitle2Descriptions
        //{
        //    get
        //    {
        //        List<Description> listDescription = new List<Description>
        //        {
        //            new Description{DescriptionId= 20, SubtitleId = 4, DescriptionText = "16. With Supervisors" },
        //            new Description{DescriptionId= 21, SubtitleId = 4, DescriptionText = "17. With other team members / across teams" },
        //            new Description{DescriptionId= 22, SubtitleId = 4, DescriptionText = "18. With client(s)" },
        //            new Description{DescriptionId= 23, SubtitleId = 4, DescriptionText = "19. Commitment to Team Success" },
        //            new Description{DescriptionId= 24, SubtitleId = 4, DescriptionText = "Subtotal" },
        //        };

        //        return listDescription;
        //    }
        //}

        public static Description ActivelyGrowthDescription25
        {
            get
            {
                Description description24 = new Description { DescriptionId = 25, SubtitleId = 5, DescriptionText = "20. Actively seeks ways to streamline processes" };
                return description24;
            }
        }

        public static Description OpenGrowthDescription26
        {
            get
            {
                Description description25 = new Description { DescriptionId = 26, SubtitleId = 5, DescriptionText = "21. Open to new ideas and approaches" };
                return description25;
            }
        }

        public static Description InvolvementGrowthDescription27
        {
            get
            {
                Description description26 = new Description { DescriptionId = 27, SubtitleId = 5, DescriptionText = "22. Involvement/commitment in activities for work/company improvement" };
                return description26;
            }
        }

        public static Description ChallengesGrowthDescription28
        {
            get
            {
                Description description27 = new Description { DescriptionId = 28, SubtitleId = 5, DescriptionText = "23. Challenges Status Quo processes in appropriate ways" };
                return description27;
            }
        }

        public static Description SeeksGrowthDescription29
        {
            get
            {
                Description description28 = new Description { DescriptionId = 29, SubtitleId = 5, DescriptionText = "24. Seeks additional training and development" };
                return description28;
            }
        }

        public static Description SubtotalGrowthDescription30
        {
            get
            {
                Description description29 = new Description { DescriptionId = 30, SubtitleId = 5, DescriptionText = "Subtotal" };
                return description29;
            }
        }

        //public static List<Description> Subtitle3Descriptions
        //{
        //    get
        //    {
        //        List<Description> listDescription = new List<Description>
        //        {
        //            new Description{DescriptionId= 25, SubtitleId = 5, DescriptionText = "20. Actively seeks ways to streamline processes" },
        //            new Description{DescriptionId= 26, SubtitleId = 5, DescriptionText = "21. Open to new ideas and approaches" },
        //            new Description{DescriptionId= 27, SubtitleId = 5, DescriptionText = "22. Involvement/commitment in activities for work/company improvement" },
        //            new Description{DescriptionId= 28, SubtitleId = 5, DescriptionText = "23. Challenges Status Quo processes in appropriate ways" },
        //            new Description{DescriptionId= 29, SubtitleId = 5, DescriptionText = "24. Seeks additional training and development" },
        //            new Description{DescriptionId= 30, SubtitleId = 5, DescriptionText = "Subtotal" }
        //        };

        //        return listDescription;
        //    }
        //}

        public static Description PunctualityPoliciesDescription31
        {
            get
            {
                Description description30 = new Description { DescriptionId = 31, SubtitleId = 6, DescriptionText = "Punctuality: Fulfillment of the company's established schedules for attendance, meetings, etc (within the company and with clients). " };
                return description30;
            }
        }

        public static Description PoliciesPoliciesDescription32
        {
            get
            {
                Description description31 = new Description { DescriptionId = 32, SubtitleId = 6, DescriptionText = "Policies Compliance: (non-disclosure, dress code )" };
                return description31;
            }
        }

        public static Description ValuesPoliciesDescription33
        {
            get
            {
                Description description32 = new Description { DescriptionId = 33, SubtitleId = 6, DescriptionText = "Values: Acts according to the company values " };
                return description32;
            }
        }

        public static Description SubtotalPoliciesDescription34
        {
            get
            {
                Description description33 = new Description { DescriptionId = 34, SubtitleId = 6, DescriptionText = "Subtotal" };
                return description33;
            }
        }

        public static Description EnglishEvaluationDescription
        {
            get
            {
                Description description34 = new Description { DescriptionId = 35, SubtitleId = 6, DescriptionText = "ENGLISH EVALUATION result" };
                return description34;
            }
        }

        public static Description TotalCompetencesDescription36
        {
            get
            {
                Description description35 = new Description { DescriptionId = 36, SubtitleId = 6, DescriptionText = "Total  Competences" };
                return description35;
            }
        }

        //public static List<Description> Subtitle4Descriptions
        //{
        //    get
        //    {
        //        List<Description> listDescription = new List<Description>
        //        {
        //            new Description{DescriptionId= 31, SubtitleId = 6, DescriptionText = "Punctuality: Fulfillment of the company's established schedules for attendance, meetings, etc (within the company and with clients). " },
        //            new Description{DescriptionId= 32, SubtitleId = 6, DescriptionText = "Policies Compliance: (non-disclosure, dress code )" },
        //            new Description{DescriptionId= 33, SubtitleId = 6, DescriptionText = "Values: Acts according to the company values " },
        //            new Description{DescriptionId= 34, SubtitleId = 6, DescriptionText = "Subtotal" },
        //            new Description{DescriptionId= 35, SubtitleId = 6, DescriptionText = "Total  Competences" }
        //        };

        //        return listDescription;
        //    }
        //}
    }
}