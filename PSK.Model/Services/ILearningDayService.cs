using PSK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Services
{
    interface ILearningDayService
    {
        ServerResult addNewLearningDay(DayArgs args);
        ServerResult deleteLearningDay(int id);
    }
}
