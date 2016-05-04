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
	
	private Question[] questions = new Question[5];
	private int currentQuestionIndex;
	private int[] questionNumbersChoosen = new int[4];
	private int questionFinished;
	
	public GameObject[] triviaPanels;
	public GameObject finalResultsPanel;
	public Text resultsText;
	private int numberOfCorrectAnswers;
	
	// Use this for initialization
	void Start () {
		SetQuestions();
		currentQuestion = questions[currentQuestionIndex];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SetQuestions(){
	
		for(int i = 0; i < questionNumbersChoosen.Length; i++){
			questionNumbersChoosen[i] = 0;
		}
		questions[0] = new Question("What language are we currently using to code in Unity?", new string[]{"Java", "Objective-C", "Python", "C#", "Ruby"}, 3);
		questions[1] = new Question("What is the correct way to end a C# statement?", new string[]{".", "$", ";", ":", "!"}, 2);
		questions[2] = new Question("Which of the following keyword is used for including the namespaces in the program in C#?", new string[]{"import", "using", "load", "include", "read"}, 1);
		questions[3] = new Question("What is the correct way to add 1 to the variable int i?", new string[]{"i++", "i+", "+1", "i+1", "i=+1"}, 0);
		questions[4] = new Question("C# is a kind of ___ programming language?", new string[]{"variable-oriented", "markup", "class-oriented", "hypertext", "object-oriented"}, 4);
				
		PickQuestions();
		AssignQuestion(questionNumbersChoosen[0]);
	}
	
	void AssignQuestion(int qNum){
		currentQuestion = questions[qNum];
		questionText.text = currentQuestion.questionText;
		for (int i = 0; i < answerButtons.Length; i++){
			answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
		}
		print("correct index: " + currentQuestion.correctAnswerIndex);
	}
	
	public void CheckAnswer(int btnNum){
		if(btnNum != currentQuestion.correctAnswerIndex){
			print("incorrect");
		}else{
			print("correct");
			numberOfCorrectAnswers++;
		}
		
		if (questionFinished < questionNumbersChoosen.Length -1){
			MoveToNextQuestion();
			questionFinished++;
		}else{
			foreach(GameObject p in triviaPanels){
				p.SetActive(false);
			}
			finalResultsPanel.SetActive(true);
			DisplayResults();
		}
	}
	
	void PickQuestions(){
		for(int i = 0; i < questionNumbersChoosen.Length; i++){
			int questionNum = Random.Range(0, questions.Length);
			if(NumberNotContained(questionNumbersChoosen, questionNum)){
				questionNumbersChoosen[i] = questionNum;
				print("Questions Choosen are: " + questionNumbersChoosen[i]);
			}else{
				i--;
			}
		}
		currentQuestionIndex = Random.Range(0, questions.Length);
		print(currentQuestionIndex);
	}
	
	bool NumberNotContained(int[] numbers, int num){
		for(int i = 0; i < numbers.Length; i++){
			if(num == numbers[i]){
				return false;
			}
		}
		return true;
	}
	
	public void MoveToNextQuestion(){
		AssignQuestion(questionNumbersChoosen[questionNumbersChoosen.Length -1 -questionFinished]);
	}
	
	void DisplayResults(){
		switch(numberOfCorrectAnswers){
			case 4:
				resultsText.text = "Perfect!";
				break;
			case 3:
				resultsText.text = "Well done!";
				break;
			case 2:
				resultsText.text = "Better luck next time!";
				break;
			case 1:
				resultsText.text = "Okay...";
				break;
			case 0:
				resultsText.text = "Well, let's study together.";
				break;
			default:
				print("invalid number of correct answers");
				break;
		}
	}
}