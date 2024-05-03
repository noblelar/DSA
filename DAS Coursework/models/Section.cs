using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS_Coursework.models
{
    internal class Section
    {
        private Guid id;
        private string startingStation;
        private string endingStation;
        private float distance;
        private float time_ui;
        private float time_amPeak;
        private float inter_peak;
        private float delay = 0;

       
        public Section(string startingStation, string endingStation, float distance,
                       float time_ui, float time_amPeak, float inter_peak, float delay = 0)
        {
            this.id = Guid.NewGuid();
            this.startingStation = startingStation;
            this.endingStation = endingStation;
            this.distance = distance;
            this.time_ui = time_ui;
            this.time_amPeak = time_amPeak;
            this.inter_peak = inter_peak;
            this.delay = delay;
        }

        public float GetDelay() => delay;
        public void SetDelay(float value) => delay = value;


    }
}
