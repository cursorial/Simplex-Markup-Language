using System;
using System.Collections;

public class REPL{
	Stack tagHistory = new Stack();
	string currentTag;
	char[] input;

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
					for(int j = 1; j < tagHistory.Count; j++){
						Console.Write("\t");
					}
					Console.WriteLine("</" + tagHistory.Pop() + ">"); 
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
			if(input[j] == '(' || input[j] == ')'){
				break;
			} else {
				currentTag += input[j];
			}
		}
		return currentTag;
	}

	public static void Main(){
		REPL repl = new REPL();
		repl.Start();
	}
}
