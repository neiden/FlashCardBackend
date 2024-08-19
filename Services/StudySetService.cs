using Microsoft.AspNetCore.Mvc;

namespace Services;

public class StudySetService
{

    private readonly IRepository _repo;

    public StudySetService(IRepository repository)
    {
        _repo = repository;
    }

    [HttpPost]
    public async Task<StudySet> CreateStudySet(StudySet studySet)
    {
        var result = await _repo.CreateStudySet(studySet);
        return result;
    }

    [HttpGet]
    public async Task<List<StudySet>> GetAllStudySetsById(int userId)
    {
        var result = await _repo.GetAllStudySetsById(userId);
        return result;
    }

    [HttpPut]
    public async Task<StudySet> UpdateStudySet(StudySet studySet)
    {
        var result = await _repo.UpdateStudySet(studySet);
        return result;
    }

    [HttpDelete]
    public async Task DeleteStudySet(int id)
    {
        await _repo.DeleteStudySet(id);
    }
}