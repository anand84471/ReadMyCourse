using StudentDashboard.HttpRequest.Student;
using StudentDashboard.Models.Session;
using StudentDashboard.Repository;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.ServiceLayer
{
    public class StudentSessionService
    {
        StudentSessionRepo _repo;
        public StudentSessionService()
        {
            _repo = new StudentSessionRepo();
        }
        public async Task<string> CreateSessionAsync(StudentSession session)
        {
            session.ExpiryTime = DateTime.Now.AddYears(Constants.SESSION_EXPIRY_TIME_IN_YEAR);
            session.Token = JwtStudentSessionToken.GenerateJSONWebToken(session);
            await _repo.InsertStudentSessionAsync(session);
            return session.Token;
        }
        public async Task<bool> LogoutSessionAsync(StudentSession session)
        {
            return  await _repo.LogoutSessionAsync(session);
        }
        public async  Task<bool> ValidateSessionAsync(string Token)
        {
            var session = await _repo.GetStudentSessionDetailsAsync(Token);
            return session != null && session.IsLoggedOut == false && session.GetIsExpired() == false;
        }
    }
}