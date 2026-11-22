using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Ideal Blood Pressure")]  Ideal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                //low bp is Diastolic < 60 or Systolic < 90
                if (Diastolic < 60 || Systolic < 90)
                    return BPCategory.Low;
                //ideal bp is Diastolic >= 60 and Diastolic <= 80 and Systolic >= 90 and Systolic <= 120
                else if (Diastolic >= 60 && Diastolic <= 80 && Systolic >= 90 && Systolic <= 120)
                    return BPCategory.Ideal;
                //pre-high bp is Diastolic > 80 or Systolic > 120 but not high
                else if (Diastolic > 80 || Systolic > 120)
                    return BPCategory.PreHigh;
                //high bp is Diastolic >= 80 and Systolic >= 130
                else
                    return BPCategory.High;
            }
        }
    }
}
