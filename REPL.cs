using System;
using System.Collections;

public class REPL{
	Stack tagHistory = new Stack();
	Stack currentParams = new Stack();
	string currentTag;
	string currentParam;
	char[] input;
	int stringOpenerCount = 0;

	public REPL(){

	}

	public void Start(){
		Console.Write(">>> ");
		while(true){
			input = Console.ReadLine().ToCharArray();
			for(int i = 0; i < input.Length; i++){
				if(input[i] == '('){
					tagHistory.Push(ReadTag(input, i));
					for(int j = 1; j < tagHistory.Count; j++){
						Console.Write("\t");
					}
					Console.WriteLine("<" + tagHistory.Peek() + ">");
				} else if(input[i] == ')'){
					stringOpenerCount = 0;
					for(int j = 1; j < tagHistory.Count; j++){
						Console.Write("\t");
					}
					Console.WriteLine("</" + tagHistory.Pop() + ">"); 
				} else if(input[i] == '"'){
					if(stringOpenerCount == 0){
						currentParams.Push(ReadStringParameter(input, i));
						for(int j = 1; j < tagHistory.Count + 1; j++){
							Console.Write("\t");
						}
						Console.WriteLine(currentParams.Pop());
					}
				} else {
					currentTag += input[i];
				}
			}
			Console.Write(">>> ");
		}
	}

	public string ReadTag(char[] input, int i){
		currentTag = "";
		for(int j = i + 1; j < input.Length; j++){
			if(input[j] == '(' || input[j] == ')' || input[j] == ' '){
				break;
			} else {
				currentTag += input[j];
			}
		}
		return currentTag;
	}

	public string ReadStringParameter(char[] input, int i){
		currentParam = "";
		for(int j = i + 1; j < input.Length; j++){
			if(input[j] == '"'){
				stringOpenerCount++;
				break;
			} else {
				currentParam += input[j];
			}
		}
		return currentParam;
	}

	public static void Main(){
		REPL repl = new REPL();
		repl.Start();
	}
}
