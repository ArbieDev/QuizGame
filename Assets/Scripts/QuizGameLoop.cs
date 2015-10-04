using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuizGameLoop : MonoBehaviour {
	
	public struct Question{
		public string questionText;
		public string[] answers;
		public int correctAnswerIndex;
		
		public Question(string qText, string[] ans, int corIndex){
			this.questionText = qText;
			this.answers = ans;
			this.correctAnswerIndex = corIndex;
		}
	}
	
	private Question currentQuestion;
	public Button[] answerButtons;
	public Text questionText;
	
	private Question[] questions = new Question[4];
	private int currentQuestionIndex;
	
	// Use this for initialization
	void Start () {
		SetQuestions();
		PickQuestion();
		currentQuestion = questions[currentQuestionIndex];
		AssignQuestion();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SetQuestions(){
		questions[0] = new Question("What language are we currently using to code in Unity?", new string[]{"Java", "Objective-C", "Python", "C#", "Ruby"}, 3);
		questions[1] = new Question("What is the correct way to end a C# statement?", new string[]{".", "$", ";", ":", "!"}, 2);
		questions[2] = new Question("What is the correct way to add 1 to the variable int i?", new string[]{"i++", "i+", "+1", "i+1", "i=+1"}, 0);
		questions[3] = new Question("C# is a kind of ___ programming language?", new string[]{"variable-oriented", "markup", "class-oriented", "hypertext", "object-oriented"}, 0);
	}
	
	void AssignQuestion(){
		questionText.text = currentQuestion.questionText;
		for (int i = 0; i < answerButtons.Length; i+=1){
			answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
		}
	}
	
	public void CheckAnswer(int btnNum){
		if(btnNum == currentQuestion.correctAnswerIndex){
			print("correct");
		}else{
			print("incorrect");
		}
	}
	
	void PickQuestion(){
		currentQuestionIndex = Random.Range(0, questions.Length);
		print(currentQuestionIndex);
	}
}
