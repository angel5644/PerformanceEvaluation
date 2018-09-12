using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public static class PerformanceSection
    {
        public static Title PerformanceTitle
        {
            get 
            {
                Title title = new Title { TitleId = 1, Name = "Performance" };

                return title;
            }
        } 

        public static Subtitle QualitySubtitle
        {
            get 
            {   
                Subtitle subtitle1 = new Subtitle {SubtitleId = 1, Name = "Quality of the Developed Products: The products meet all the requirements, specifications and standards that the client requires?", TitleId = 1};
                return subtitle1;
            }
        }

        public static Subtitle OpportunitySubtitle
        {
            get
            {
                Subtitle subtitle2 = new Subtitle { SubtitleId = 2, Name = "Opportunity in the delivery of products: All products were delivered on or before deadlines?", TitleId = 1 };
                return subtitle2;
            }
        }

        public static Description AccuracyQualityDescription1
        {
            get
            {
                Description description0 = new Description { DescriptionId = 1, DescriptionText = "1. Accuracy or Precision", SubtitleId = 1 };
                return description0;
            }
        }

        public static Description ThoroughnessQualityDescription2
        {
            get
            {
                Description description1 = new Description { DescriptionId = 2, DescriptionText = "2. Thoroughness (Content) and Neatness (Presentation)", SubtitleId = 1 };
                return description1;
            }
        }

        public static Description ReliabilityQualityDescription3
        {
            get
            {
                Description description2 = new Description { DescriptionId = 3, DescriptionText = "3. Reliability", SubtitleId = 1 };
                return description2;
            }
        }

        public static Description ResponsivenessQualityDescription4
        {
            get
            {
                Description description3 = new Description { DescriptionId = 4, DescriptionText = "4. Responsiveness to requests for service", SubtitleId = 1 };
                return description3;
            }
        }

        public static Description FollowQualityDescription5
        {
            get
            {
                Description description4 = new Description { DescriptionId = 5, DescriptionText = "5. Follow-through/Follow-up", SubtitleId = 1 };
                return description4;
            }
        }

        public static Description JudgmentQualityDescription6
        {
            get
            {
                Description description5= new Description { DescriptionId = 6, DescriptionText = "6. Judgment/Decision making", SubtitleId = 1 };
                return description5;
            }
        }

        public static Description SubtotalQualityDescription7
        {
            get
            {
                Description description6 = new Description { DescriptionId = 7, DescriptionText = "Subtotal", SubtitleId = 1 };
                return description6;
            }
        }

       /* public static List<Description> Subtitle1Descriptions 
        {
            get 
            {
                List<Description> listDescriptions = new List<Description>
                {
                    new Description{DescriptionId = 0, DescriptionText = "1. Accuracy or Precision", SubtitleId = 0},
                    new Description{DescriptionId = 1, DescriptionText = "2. Thoroughness (Content) and Neatness (Presentation)", SubtitleId = 0},
                    new Description{DescriptionId = 2, DescriptionText = "3. Reliability", SubtitleId = 0},
                    new Description{DescriptionId = 3, DescriptionText = "4. Responsiveness to requests for service", SubtitleId = 0},
                    new Description{DescriptionId = 4, DescriptionText = "5. Follow-through/Follow-up", SubtitleId = 0},
                    new Description{DescriptionId = 5, DescriptionText = "6. Judgment/Decision making", SubtitleId = 0},
                    new Description{DescriptionId = 6, DescriptionText = "Subtotal", SubtitleId = 0}
                };

                return listDescriptions;
            }   
        }*/

        public static Description PriorityOpportunityDescription8
        {
            get
            {
                Description description7 = new Description { DescriptionId = 8, DescriptionText = "7.Priority Setting", SubtitleId = 2 };
                return description7;
            }
        }

        public static Description AmountOpportunityDescription9
        {
            get
            {
                Description description8 = new Description { DescriptionId = 9, DescriptionText = "8.Amount of work completed", SubtitleId = 2 };
                return description8;
            }
        }

        public static Description WorkOpportunityDescription10
        {
            get
            {
                Description description9 = new Description { DescriptionId = 10, DescriptionText = "9.Work completed on schedule", SubtitleId = 2 };
                return description9;
            }
        }

        public static Description SubtotalOpportunityDescription11
        {
            get
            {
                Description description10 = new Description { DescriptionId = 11, DescriptionText = "Subtotal", SubtitleId = 2 };
                return description10;
            }
        }

        public static Description TotalPerformanceDescription12
        {
            get
            {
                Description description11 = new Description { DescriptionId = 12, DescriptionText = "Total Performance", SubtitleId = 2 };
                return description11;
            }
        }

        /*public static List<Description> Subtitle2Descriptions
        {
            get
            {
                List<Description> listDescriptions = new List<Description>
                {
                    new Description{DescriptionId = 7, DescriptionText = "7.Priority Setting", SubtitleId = 1},
                    new Description{DescriptionId = 8, DescriptionText = "8.Amount of work completed", SubtitleId = 1},
                    new Description{DescriptionId = 9, DescriptionText = "9.Work completed on schedule", SubtitleId = 1},
                    new Description{DescriptionId = 10, DescriptionText = "Subtotal", SubtitleId = 1},
                    new Description{DescriptionId = 11, DescriptionText = "Total  Performance", SubtitleId = 1}
                };

                return listDescriptions;
            }
        }*/
    }
}