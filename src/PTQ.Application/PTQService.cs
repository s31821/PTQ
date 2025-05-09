using PTQ.Models;
using PTQ.Repositories;

namespace PTQ.Application;

public class PTQService
{
    private QuizRepository _quizRepository;
    private PotatoTeacherRepository _potatoTeacherRepository;
    public PTQService(string connectionString)
    {
        _quizRepository = new QuizRepository(connectionString);
        _potatoTeacherRepository = new PotatoTeacherRepository(connectionString);
    }

    public IEnumerable<Quiz> GetQuizzes()
    {
        return _quizRepository.GetQuizzes();
    }

    public Quiz GetQuizById(int id)
    {
        var quiz = _quizRepository.GetQuizById(id);
        if (quiz == null)
        {
            throw new Exception("Quiz Not Found");
        }
        return quiz;
    }
}