using Courses.DAL.Interfaces;
using Courses.DAL.Repositories;
using Courses.Domain.Enum;
using Courses.Domain.Response;
using Courses.Domain.ViewModules;
using Courses.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Сourses.Domain.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Courses.Service.Implementations
{
    public class PracticalPartService : IPracticalPartService
    {
        private readonly IBaseRepository<PracticalPart> _practicalPartRep;

        public PracticalPartService(IBaseRepository<PracticalPart> practicalPartRep)
        {
            _practicalPartRep = practicalPartRep;
        }

        public async Task<IBaseResponse<PracticalPartViewModel>> CreatePracticalPart(PracticalPartViewModel practicalPartViewModel)
        {
            var baseResponse = new BaseResponse<PracticalPartViewModel>();
            try
            {
                var practicalPart = new PracticalPart {
                    Answer = practicalPartViewModel.Answer,
                    CourseId = practicalPartViewModel.CourseId,
                    Question = practicalPartViewModel.Question,
                    Id = practicalPartViewModel.Id,
                };
                await _practicalPartRep.Create(practicalPart);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PracticalPartViewModel>()
                {
                    Description = $"[CreatePracticalPart] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeletePracticalPart(int courseId, int number)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var practic = await _practicalPartRep.GetAll().FirstOrDefaultAsync(x => x.Id == number && x.CourseId == courseId);
                if (practic == null)
                {
                    baseResponse.Description = "Вопрос не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                await _practicalPartRep.Delete(practic);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePracticalPart] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }

        public async Task<IBaseResponse<PracticalPart>> Edit(int courseId, int number, PracticalPartViewModel model)
        {
            var baseResponse = new BaseResponse<PracticalPart>();

            try
            {
                var practical = await _practicalPartRep.GetAll().
                    FirstOrDefaultAsync(x => x.Id == number && x.CourseId == courseId);
                if (practical == null)
                {
                    baseResponse.StatusCode = StatusCode.CourseNotFound;
                    baseResponse.Description = "Question not found";
                    return baseResponse;
                }

                practical.Answer = model.Answer;
                practical.CourseId = model.CourseId;
                practical.Question = model.Question;

                await _practicalPartRep.Update(practical);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<PracticalPart>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }

        public async Task<IBaseResponse<PracticalPart>> GetPracticalPart(int courseId, int number)
        {
            var baseResponse = new BaseResponse<PracticalPart>();
            try
            {
                var practics = await _practicalPartRep.GetAll().
                    FirstOrDefaultAsync(x => x.Id == number && x.CourseId == courseId);

                if (practics == null)
                {
                    baseResponse.Description = "Вопрос не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = practics;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<PracticalPart>()
                {
                    Description = $"[GetPracticalPart] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<PracticalPart>>> GetPracticalParts(int courseId)
        {
            var baseResponse = new BaseResponse<IEnumerable<PracticalPart>>();
            try
            {
                var practics = await _practicalPartRep.GetAll().Where(x => x.CourseId == courseId).ToListAsync();

                if (practics.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = practics;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<PracticalPart>>()
                {
                    Description = $"[GetPracticalParts] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }

        public async Task<IBaseResponse<bool>> CheckAnswer(int courseId, int partId, string answer)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var practics = await _practicalPartRep.GetAll().
                    FirstOrDefaultAsync(x => x.Id == partId && x.CourseId == courseId);
                if (practics == null)
                {
                    baseResponse.Description = "Вопрос не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = answer == practics.Answer;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CheckAnswer] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
    }
}
