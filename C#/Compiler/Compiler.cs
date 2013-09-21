using System;
using System.Collections;
using System.IO;

public class Compiler{
	public string fileContents = "";
	Stack tagHistory = new Stack();
	Stack currentParams = new Stack();
	string currentTag;
	string currentParam;
	char[] input;
	int stringOpenerCount = 0;
	StreamReader reader;
	StreamWriter writer;

	public void Start(string inputLocation, string outputLocation){
		reader = new StreamReader(inputLocation);
		writer = new StreamWriter(outputLocation);
		while(reader.Peek() != -1){
			fileContents += reader.ReadLine().Trim();
		}
		input = fileContents.ToCharArray();
		for(int i = 0; i < input.Length; i++){
			if(input[i] == '('){
				tagHistory.Push(ReadTag(input, i));
				for(int j = 1; j < tagHistory.Count; j++){
					writer.Write("\t");
				}
				writer.WriteLine("<" + tagHistory.Peek() + ">");
			} else if(input[i] == ')'){
				stringOpenerCount = 0;
				for(int j = 1; j < tagHistory.Count; j++){
					writer.Write("\t");
				}
				writer.WriteLine("</" + tagHistory.Pop() + ">"); 
			} else if(input[i] == '"'){
				if(stringOpenerCount == 0){
					currentParams.Push(ReadStringParameter(input, i));
					for(int j = 1; j < tagHistory.Count + 1; j++){
						writer.Write("\t");
					}
					writer.WriteLine(currentParams.Pop());
				}
			} else {
				currentTag += input[i];
			}
		}
		writer.Close();
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

	public static void Main(string[] args){
		string inputLocation = "", outputLocation = "";
		for(int i = 0; i < args.Length; i++){
			if(String.Compare(args[i], "-h", false) == 0){
				Console.WriteLine("Usage:\n-i : input file location\n-o : output file location\n-h : display this page");
			} else if(String.Compare(args[i], "-o", false) == 0){
				outputLocation = args[i + 1];
				i++;
			} else if(String.Compare(args[i], "-i", false) == 0){
				inputLocation = args[i + 1];
				i++;
			} else {
				Console.WriteLine("Usage:\n-i : input file location\n-o : output file location\n-h : display this page");
			}
		}

		Compiler s2hc = new Compiler();
		s2hc.Start(inputLocation, outputLocation);
	}
}
