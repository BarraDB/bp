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

        // Category thresholds
        private const int LowSystolic = 90;          
        private const int LowDiastolic = 60;
        private const int IdealSystolicMax = 120;    
        private const int IdealDiastolicMax = 80;
        private const int HighSystolic = 140;        
        private const int HighDiastolic = 90;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                // low regardless of other number
                if (Systolic < LowSystolic || Diastolic < LowDiastolic)
                    return BPCategory.Low;

                var sys = ClassifySystolic(Systolic);
                var dia = ClassifyDiastolic(Diastolic);
                return sys >= dia ? sys : dia;
            }
        }
        // Anything between Ideal and High is PreHigh.
        private static BPCategory ClassifySystolic(int mmHg)
        {
            if (mmHg >= HighSystolic)
                return BPCategory.High;
            if (mmHg > IdealSystolicMax)
                return BPCategory.PreHigh;
            return BPCategory.Ideal;
        }

        private static BPCategory ClassifyDiastolic(int mmHg)
        {
            if (mmHg >= HighDiastolic)
                return BPCategory.High;
            if (mmHg > IdealDiastolicMax)
                return BPCategory.PreHigh;
            return BPCategory.Ideal;
        }
    }
}
