using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Compiler{
	StreamReader reader;
	StreamWriter writer;
	string fileContents;

	int stringOpenerCount = 0;
	int attribCount = 0;

	Stack<string> branchHistory = new Stack<string>();

	string currentName;
	string currentAttrib;
	string currentContent;

	public void AddOpenToken(int i){
		currentName = "";
		for(int k = 0; k < branchHistory.Count; k++){
			writer.Write("\t");		
		}
		for(int j = i + 1; j < fileContents.Length; j++){
			if(fileContents[j] == '(' || fileContents[j] == ')'){
				branchHistory.Push(currentName);
				writer.Write("<" + currentName + ">\n");
				break;
			} else if (fileContents[j + 1] == ':' || fileContents[j + 1] == '"'){
				branchHistory.Push(currentName);
				writer.Write("<" + currentName);
				break;
			} else {
				currentName += fileContents[j];
			}
		}
	}
	public void AddBranch(int i){
		if(attribCount > 0 && stringOpenerCount == 0){
			writer.Write(">\n");
		}
		for(int k = 0; k < branchHistory.Count - 1; k++){
			writer.Write("\t");
		}
		writer.Write("</" + branchHistory.Pop() + ">\n");
		attribCount = 0;
		stringOpenerCount = 0;
	}
	public void AddAttribToken(int i){
		currentAttrib = "";
		for(int j = i + 1; j < fileContents.Length; j++){
			if(fileContents[j] == ' ' || fileContents[j] == '(' || fileContents[j] == ')'){
				break;
			} else {
				currentAttrib += fileContents[j];
			}
		}
		writer.Write(" " + currentAttrib);
		attribCount++;
	}
	public void AddContentToken(int i){
		currentContent = "";
		for(int j = i + 1; j < fileContents.Length; j++){
			if(fileContents[j] == '"'){
				stringOpenerCount++;
				break;
			} else {
				currentContent += fileContents[j];
			}
		}
		writer.Write(">\n");
		for(int k = 0; k < branchHistory.Count; k++){
			writer.Write("\t");
		}
		writer.Write(currentContent + "\n");
	}

	public Compiler(string inputLocation, string outputLocation){
		reader = new StreamReader(inputLocation);
		writer = new StreamWriter(outputLocation);
		while(reader.Peek() != -1){
			fileContents += reader.ReadLine().Trim();
		}
		for(int i = 0; i < fileContents.Length; i++){
			if(fileContents[i] == '('){
				AddOpenToken(i);
			} else if(fileContents[i] == ')'){
				AddBranch(i);
			} else if(fileContents[i] == ':'){
				AddAttribToken(i);
			} else if(fileContents[i] == '"'){
				if(stringOpenerCount == 0){
					AddContentToken(i);
				}
			}
		}
		writer.Close();
	}
	public static void Main(string[] args){
		string inputLocation = "";
		string outputLocation = "";
		for(int i = 0; i < args.Length; i++){
			if(String.Compare(args[i], "-i", true) == 0){
				inputLocation = args[i + 1];
			} else if(String.Compare(args[i], "-o", true) == 0){
				outputLocation = args[i + 1]; 
			} else if(String.Compare(args[i], "-h", true) == 0) {
				ShowHelp();
			}
		}
		new Compiler(inputLocation, outputLocation);
	}
	public static void ShowHelp(){
		Console.WriteLine("-i Input file selector (required)\n-o Output file selector (required)\n-h Display this page");
	}
}