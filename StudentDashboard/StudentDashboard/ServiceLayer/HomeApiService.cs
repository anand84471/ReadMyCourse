using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Base;
using StudentDashboard.Models.Category;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Review;
using StudentDashboard.Models.Search;
using StudentDashboard.Models.Student;
using StudentDashboard.Repository;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.ServiceLayer
{
    public class HomeApiService
    {
        HomeRepository _repo;
        public HomeApiService()
        {
            _repo = new HomeRepository();
        }
       

        public async Task<List<CategoryModel>> FetchAllClassroomCategories()
        {
            return await _repo.GetAllCllassroomCategoriesAsync();
        }

        public async Task<List<ClassroomShowcaseDetails>> GetAllClassroomForCategoriesAsync(SearchRequest<int> request)
        {
            return await _repo.GetAllClassroomForCategories(request);
        }
        public async Task<List<ClassroomShowcaseDetails>> GetTrendingClassroomsAsync(SearchBaseRequest request)
        {
            return await _repo.GetTrendingClassroomsAsync(request);
        }
        public async Task<List<ClassroomShowcaseDetails>> GetPopularClassroomsAsync(SearchBaseRequest request)
        {
            return await _repo.GetPopularClassroomsAsync(request);
        }
        public async Task<InstructorPublicProfileDetails> GetInstructorPublicDetailsAsync(int InstructorId)
        {
            InstructorPublicProfileDetails result = await _repo.GetInstructorPublicDetailsAsync(InstructorId);
            if (result != null)
            {
                result.classrooms = await GetAllClassroomForInstructorAsync(
                    new SearchRequest<int>()
                    {
                        Id=InstructorId,
                        m_iNoOfRowsToFetch=Constants.MAX_ITEMS_TO_BE_RETURNED
                    }
                    );
                result.reviews = await GetAllReviewsForInstructorAsync(
                   new SearchRequest<int>()
                   {
                       Id = InstructorId,
                       m_iNoOfRowsToFetch = Constants.MAX_ITEMS_TO_BE_RETURNED
                   }
                   );
            }
            return result;
        }
        public async Task<List<ClassroomShowcaseDetails>> GetAllClassroomForInstructorAsync(SearchRequest<int> request)
        {
            return await _repo.GetInstructorClassroomsAsync(request);
        }
        public async Task<List<ReviewModel>> GetAllReviewsForInstructorAsync(SearchRequest<int> request)
        {
            return await _repo.GetAllReviewsForInstructorAsync(request);
        }
        public async Task<ClassroomPublicDetails> GetClassroomPublicDetailsAsync(long ClassroomId)
        {
            ClassroomPublicDetails classroomDetails = await _repo.GetClassroomPublicDetails(ClassroomId);
            if (classroomDetails != null)
            {
               var syllabus= await GetClassroomSyllabus(ClassroomId);
                if (syllabus != null)
                {
                    classroomDetails.m_lsClassroomWeekWiseSyallabus = syllabus.m_lsClassroomWeekWiseSyallabus;
                }
            }
            return classroomDetails;
        }

        public async Task<List<ReviewModel>> GetAllClassroomReviews(SearchRequest<long> request)
        {
            List<ReviewModel> result = await _repo.GetAllClassroomReviewsAsync(request);
            return result;
        }
        public async Task<AvgReviewModel> GetAvgClassroomRating(long ClassroomId)
        {
            AvgReviewModel avgReviewModel = new AvgReviewModel();
            List<RatingNormal> lsRatingNormal = null;
            try
            {
                lsRatingNormal = await _repo.GetAvgClassroomRating(ClassroomId);
                var TotalRatings = 0;
                var AvgRatingSum = 0;
                foreach (var data in lsRatingNormal)
                {
                    TotalRatings += data.m_iNoOfRating;

                    switch (data.m_iRating)
                    {
                        case 1:
                            {
                                AvgRatingSum += 1 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage1StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 2:
                            {
                                AvgRatingSum += 2 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage2StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 3:
                            {
                                AvgRatingSum += 3 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage3StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 4:
                            {
                                AvgRatingSum += 4 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage4StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 5:
                            {
                                AvgRatingSum += 5 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage5StartRating = data.m_iNoOfRating;
                                break;
                            }
                    }
                }
                if (TotalRatings != 0)
                {
                    avgReviewModel.m_iTotalReviews = TotalRatings;
                    avgReviewModel.m_fPercentage1StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage1StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage2StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage2StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage3StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage3StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage4StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage4StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage5StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage5StartRating, TotalRatings);
                    avgReviewModel.m_fAvgRating = AvgRatingSum / TotalRatings;
                }
            }
            catch (Exception Ex)
            {
               
            }
            return avgReviewModel;
        }
        public async Task<ReviewsDetails> GetClassroomReviewAsync(long ClassroomId)
        {
            ReviewsDetails classroomReviewsResponse = new ReviewsDetails();
            try
            {
                classroomReviewsResponse.avgReviewModel = await GetAvgClassroomRating(ClassroomId);
                classroomReviewsResponse.lsReviews = await GetAllClassroomReviews(new SearchRequest<long>() { 
                    m_iNoOfRowsFetched = 0 ,
                    m_iNoOfRowsToFetch=Constants.MAX_ITEMS_TO_BE_RETURNED,Id=ClassroomId});
            }
            catch (Exception Ex)
            {
              
            }
            return classroomReviewsResponse;
        }
        public async Task<ClassroomSyllabusDetailsModal> GetClassroomSyllabus(long ClassroomId)
        {
            ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal = await _repo.GetClassroomSyllabusAsync(ClassroomId);
            if (classroomSyllabusDetailsModal != null)
            {
                classroomSyllabusDetailsModal.m_lsClassroomWeekWiseSyallabus = JsonConvert.DeserializeObject<List<ClassroomWeekWiseSyallabus>>(classroomSyllabusDetailsModal.m_strSerializedSyllabus);
            }
            return classroomSyllabusDetailsModal;
        }
        public async Task<AvgReviewModel> GetAvgInstructorRating(int InstrcutorId)
        {
            AvgReviewModel avgReviewModel = new AvgReviewModel();
            List<RatingNormal> lsRatingNormal = null;
            try
            {
                lsRatingNormal = await _repo.GetAvgInstructorRatings(InstrcutorId);
                var TotalRatings = 0;
                var AvgRatingSum = 0;
                foreach (var data in lsRatingNormal)
                {
                    TotalRatings += data.m_iNoOfRating;

                    switch (data.m_iRating)
                    {
                        case 1:
                            {
                                AvgRatingSum += 1 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage1StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 2:
                            {
                                AvgRatingSum += 2 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage2StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 3:
                            {
                                AvgRatingSum += 3 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage3StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 4:
                            {
                                AvgRatingSum += 4 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage4StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 5:
                            {
                                AvgRatingSum += 5 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage5StartRating = data.m_iNoOfRating;
                                break;
                            }
                    }
                }
                if (TotalRatings != 0)
                {
                    avgReviewModel.m_iTotalReviews = TotalRatings;
                    avgReviewModel.m_fPercentage1StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage1StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage2StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage2StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage3StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage3StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage4StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage4StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage5StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage5StartRating, TotalRatings);
                    avgReviewModel.m_fAvgRating = AvgRatingSum / TotalRatings;
                }
            }
            catch (Exception Ex)
            {

            }
            return avgReviewModel;
        }
        public async Task<ReviewsDetails> GetInstructorReviewAsync(int InstructorId)
        {
            ReviewsDetails reviews = new ReviewsDetails();
            try
            {
                reviews.avgReviewModel = await GetAvgInstructorRating(InstructorId);
                reviews.lsReviews = await GetAllReviewsForInstructorAsync(new SearchRequest<int>()
                {
                    m_iNoOfRowsFetched = 0,
                    m_iNoOfRowsToFetch = Constants.MAX_ITEMS_TO_BE_RETURNED,
                    Id = InstructorId
                });
            }
            catch (Exception Ex)
            {

            }
            return reviews;
        }
    }
  
}