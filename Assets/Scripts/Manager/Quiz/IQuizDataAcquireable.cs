namespace Scripts.Manager.Quiz {

    //랜덤 수학문제식 대비한 인터페이스(실질사용은 X)
    public interface IQuizDataAcquireable {
        /// <summary>
        /// 문제
        /// </summary>
        string ProblemText { get; }
        /// <summary>
        /// 정답
        /// </summary>
        string GetCorrectAnswer { get; }
        /// <summary>
        /// 오답
        /// </summary>
        /// <param name="index">번호</param>
        /// <returns></returns>
        string GetWrongAnswer(int index);
    }

}