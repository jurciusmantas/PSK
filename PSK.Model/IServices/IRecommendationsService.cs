﻿using PSK.Model.DTO;
using System.Collections.Generic;

namespace PSK.Model.IServices
{
    public interface IRecommendationsService
    {
        ServerResult<List<Recommendation>> GetRecommendations();
        ServerResult<List<Recommendation>> GetReceivedRecommendations(int userId);
        ServerResult<List<Recommendation>> GetCreatedRecommendations(int creatorId);
        ServerResult<Recommendation> GetRecommendation(int id);
        ServerResult CreateRecommendation(Recommendation recommendation);
        ServerResult DeleteRecommendation(int id);
        ServerResult UpdateRecommendation(Recommendation rec);
    }
}
