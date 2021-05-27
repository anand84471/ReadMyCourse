using StudentDashboard.Models.Base;
using StudentDashboard.Models.Category;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Review;
using StudentDashboard.Models.Search;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.Repository
{
    public class HomeRepository
    {
        CPDataService.CpDataServiceClient _dbClient;
        StringBuilder m_strLogMessage = new StringBuilder();
        public HomeRepository()
        {
            _dbClient = new CPDataService.CpDataServiceClient();
        }
        //public async Task<ClassroomJoinDetailsModal> GetClassroomPublicDetails(long ClassroomId)
        //{
        //    ClassroomJoinDetailsModal objClassRoomModal = new ClassroomJoinDetailsModal();
        //    try
        //    {
        //        DataSet ds = await objCPDataService.GetClassRoomDetailsForStudentAsync(ClassroomId);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
        //             dataRow => new ClassroomJoinDetailsModal(
        //                  dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
        //                 dataRow.Field<int>("NO_OF_ASSIGNMENTS"),
        //                 dataRow.Field<int>("NO_OF_MEETINGS"),
        //                  dataRow.Field<int>("NO_OF_TESTS"),
        //                  dataRow.Field<int>("NO_OF_ATTACHENTS"),
        //                  dataRow.Field<string>("CLASSROOM_NAME"),
        //                  dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
        //                  dataRow.Field<DateTime>("ACTIVATION_DATETIME"),
        //                   dataRow.Field<int>("CLASSROOM_CHARGE_IN_PAISE"),
        //                   dataRow.Field<string>("BACK_GROUND_IMAGE_PATH"),
        //                   dataRow.Field<string>("CLASSROOM_SYLLABUS"),
        //                   dataRow.Field<string>("CLASSROOM_SCHEDULE_OBJ"),
        //                   dataRow.Field<DateTime?>("CLASS_START_DATE"),
        //                   dataRow.Field<int>("NO_OF_DEMO_CLASSES"),
        //                   dataRow.Field<DateTime?>("REGISTRATION_CLOSE_DATE")
        //                 )).ToList()[0];
        //        }
        //        objClassRoomModal.m_llClassroomId = ClassroomId;
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objClassRoomModal;
        //}
        public async Task<List<CategoryModel>> GetAllCllassroomCategoriesAsync()
        {
            List<CategoryModel> result = null;
            try
            {
                DataSet ds = await _dbClient.GetAllClassroomCategoriesAsync();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CategoryModel()
                     {
                         CategoryId = dataRow.Field<int>("CATEGORY_ID"),
                         CategoryName= dataRow.Field<string>("CATEGORY_NAME"),
                         CategoryImageUrl=dataRow.Field<string>("CATEGORIES_IMG_URL"),
                         NoOfCourses= dataRow.Field<int>("NO_OF_COURSES")
                     }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomShowcaseDetails>> GetAllClassroomForCategories(SearchRequest<int> request)
        {
            List<ClassroomShowcaseDetails> result = null;
            try
            {
                DataSet ds = await _dbClient.GetAllClassroomForCategoriesAsync(request.Id, request.m_iNoOfRowsFetched,
                    request.m_iNoOfRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomShowcaseDetails()
                     {
                         m_strClassroomName = dataRow.Field<string>("CLASSROOM_NAME"),
                         m_strClassroomDescription= dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         ClassroomImage = dataRow.Field<string>("CLASSROOM_THUMBNAIL_PATH"),
                         AvgRating = dataRow.Field<int?>("AVG_RATING"),
                         NoOfStudentsJoined = dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         m_llClassroomId = dataRow.Field<long>("CLASSROOM_ID")
                     }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomShowcaseDetails>> GetPopularClassroomsAsync(SearchBaseRequest request)
        {
            List<ClassroomShowcaseDetails> result = null;
            try
            {
                DataSet ds = await _dbClient.GetPopularClassroomsAsync( request.m_iNoOfRowsFetched,
                    request.m_iNoOfRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomShowcaseDetails()
                     {
                         m_strClassroomName = dataRow.Field<string>("CLASSROOM_NAME"),
                         m_strClassroomDescription = dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         ClassroomImage = dataRow.Field<string>("CLASSROOM_THUMBNAIL_PATH"),
                         AvgRating = dataRow.Field<int?>("AVG_RATING"),
                         NoOfStudentsJoined = dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         m_llClassroomId = dataRow.Field<long>("CLASSROOM_ID")
                     }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomShowcaseDetails>> GetTrendingClassroomsAsync(SearchBaseRequest request)
        {
            List<ClassroomShowcaseDetails> result = null;
            try
            {
                DataSet ds = await _dbClient.GetTredndingClassroomsAsync(request.m_iNoOfRowsFetched,
                    request.m_iNoOfRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomShowcaseDetails()
                     {
                         m_strClassroomName = dataRow.Field<string>("CLASSROOM_NAME"),
                         m_strClassroomDescription = dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         ClassroomImage = dataRow.Field<string>("CLASSROOM_THUMBNAIL_PATH"),
                         AvgRating = dataRow.Field<int?>("AVG_RATING"),
                         NoOfStudentsJoined = dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         m_llClassroomId = dataRow.Field<long>("CLASSROOM_ID")
                     }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<InstructorPublicProfileDetails> GetInstructorPublicDetailsAsync(int InstructorId)
        {
            InstructorPublicProfileDetails result = null;
            try
            {
                DataSet ds = await _dbClient.GetInstructorPublicDetailsAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new InstructorPublicProfileDetails()
                     {
                         m_iInstructorId = dataRow.Field<int>("ID"),
                         m_strName = dataRow.Field<string>("NAME"),
                         m_strProfileUrlMedium = dataRow.Field<string>("PROFILE_URL"),
                         Bio = dataRow.Field<string>("BIO"),
                         NoOfCourses = dataRow.Field<int>("NO_OF_COURSES"),
                         NoOfStudentsJoined = dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         NoOfFollowers = dataRow.Field<int>("NO_OF_FOLLOWERS"),
                         AvgRating = dataRow.Field<double?>("AVG_RATING"),
                     }).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomShowcaseDetails>> GetInstructorClassroomsAsync(SearchRequest<int> request)
        {
            List<ClassroomShowcaseDetails> result = null;
            try
            {
                DataSet ds = await _dbClient.GetAllClassroomsForInstructorsAsync(request.Id, request.m_iNoOfRowsFetched,
                    request.m_iNoOfRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomShowcaseDetails()
                     {
                         m_strClassroomName = dataRow.Field<string>("CLASSROOM_NAME"),
                         m_strClassroomDescription = dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         ClassroomImage = dataRow.Field<string>("CLASSROOM_THUMBNAIL_PATH"),
                         AvgRating = dataRow.Field<double?>("AVG_RATING"),
                         NoOfStudentsJoined = dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         m_llClassroomId = dataRow.Field<long>("CLASSROOM_ID")
                     }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ReviewModel>> GetAllReviewsForInstructorAsync(SearchRequest<int> request)
        {
            List<ReviewModel> result = null;
            try
            {
                DataSet ds = await _dbClient.GetAllReviewsOfInstructorAsync(request.Id, request.m_iNoOfRowsFetched,
                    request.m_iNoOfRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ReviewModel()
                     {
                         m_strName= dataRow.Field<string>("STUDENT_NAME"),
                         m_strProfileUrl = dataRow.Field<string>("PROFILE_URL"),
                         m_llUserId = dataRow.Field<long>("STUDENT_ID"),
                         m_strReviewDate = dataRow.Field<DateTime>("CLASSROOM_CONTENT_RATING_DATETIME").ToString("d MMM yyyy"),
                         m_iNoOfRatings = dataRow.Field<int>("CLASSROOM_CONTENT_RATING"),
                         m_strFeedback = dataRow.Field<string>("CLASSROOM_FEEDBACK")
                     }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<ClassroomPublicDetails> GetClassroomPublicDetails(long ClassroomId)
        {
            ClassroomPublicDetails result = null;
            try
            {
                DataSet ds = await _dbClient.GetClassroomsPublicDetailsAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomPublicDetails()
                     {
                         m_llClassroomId = dataRow.Field<long>("CLASSROOM_ID"),
                         m_strClassroomName = dataRow.Field<string>("CLASSROOM_NAME"),
                         m_strClassroomDescription = dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         ClassroomJoiningInPaise = dataRow.Field<int>("CLASSROOM_CHARGE_IN_PAISE"),
                         Instructor =new InstructorPublicProfileDetails(){
                             m_strName= dataRow.Field<string>("INSTRUCTOR_NAME"),
                             m_iInstructorId= dataRow.Field<int>("INSTRUCTOR_ID"),
                             m_strProfileUrlSmall= dataRow.Field<string>("PROFILE_THUMBNAIL_PATH"),
                             NoOfFollowers= dataRow.Field<int>("NO_OF_FOLLOWERS"),
                             NoOfCourses = dataRow.Field<int>("NO_OF_COURSES"),
                             AvgRating = dataRow.Field<double?>("AVG_RATING"),
                             NoOfStudentsJoined = dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         },
                         NoOfStudentsJoined = dataRow.Field<int>("NO_OF_STUDENTS_JOINED_CLASS"),
                         AvgRating = dataRow.Field<double?>("AVG_CLASSROOM_RATING"),
                     }).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<List<ReviewModel>> GetAllClassroomReviewsAsync(SearchRequest<long> request)
        {
            List<ReviewModel> lsReviews = new List<ReviewModel>();
            try
            {
                DataSet ds = await _dbClient.GetAllClassroomReviewsAsync(request.Id,
                    request.m_iNoOfRowsToFetch, request.m_iNoOfRowsFetched);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsReviews = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ReviewModel(
                         dataRow.Field<int>("NO_OF_RATINGS"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<string>("FEEDBACK_MESSAGE"),
                         dataRow.Field<DateTime?>("FEEDBACK_DATE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsReviews;
        }
        public async Task<List<RatingNormal>> GetAvgClassroomRating(long ClassroomId)
        {
            List<RatingNormal> lsRatings = new List<RatingNormal>();
            try
            {
                DataSet ds = await _dbClient.GetAvgRatingForClassroomAsync(ClassroomId);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsRatings = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new RatingNormal(
                         dataRow.Field<int>("CLASSROOM_CONTENT_RATING"),
                         dataRow.Field<int>("NO_OF_RATINGS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsRatings;
        }
        public async Task<ClassroomSyllabusDetailsModal> GetClassroomSyllabusAsync(long ClassroomId)
        {
            ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal = null;
            try
            {
                DataSet ds = await _dbClient.GetClassroomSyllabusAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    classroomSyllabusDetailsModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomSyllabusDetailsModal(
                         dataRow.Field<string>("CLASSROOM_SYLLABUS")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomSyllabus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomSyllabusDetailsModal;
        }
        public async Task<List<RatingNormal>> GetAvgInstructorRatings(int InstructorId)
        {
            List<RatingNormal> lsRatings = new List<RatingNormal>();
            try
            {
                DataSet ds = await _dbClient.GetAvgInstructorRatingAsync(InstructorId);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsRatings = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new RatingNormal(
                         dataRow.Field<int>("CLASSROOM_CONTENT_RATING"),
                         dataRow.Field<int>("NO_OF_RATINGS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsRatings;
        }
    }
}