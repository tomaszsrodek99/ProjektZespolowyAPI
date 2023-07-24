using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Dtos;
using ProjektAPI.Models;

namespace ProjektAPI.Contracts
{
    public interface IGoalRepository : IGenericRepository<Goal>
    {
    }
}
