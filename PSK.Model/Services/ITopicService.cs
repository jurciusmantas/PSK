﻿using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Services
{
    public interface ITopicService
    {
        ServerResult<List<Topic>> GetTopics();
    }
}