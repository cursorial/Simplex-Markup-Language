import java.util.Scanner;
import java.util.Stack;
import java.util.ArrayList;

public class REPL{
	Stack<String> tagHistory = new Stack<String>();
	Scanner input = new Scanner(System.in);
	char[] in;
	String currentTag = "";

	public REPL(){
		System.out.print(">>> ");
		while(input.hasNextLine()){
			in = input.nextLine().toCharArray();
			for(int i = 0; i < in.length; i++){
				if(in[i] == '('){
					tagHistory.push(readTag(in, i));
					for(int j = 1; j < tagHistory.size(); j++){
						System.out.print("\t");
					}
					System.out.print("<" + tagHistory.peek() + ">\n");
				} else if(in[i] == ')'){
					for(int j = 1; j < tagHistory.size(); j++){
						System.out.print("\t");
					}
					System.out.print("</" + tagHistory.pop() + ">\n");
				} else {
					currentTag += in[i];
				}
			}
			System.out.print("\n>>> ");
		}
	}
	public String readTag(char[] in, int i){
		currentTag = "";
		for(int j = i + 1; j < in.length; j++){
			if(in[j] == '(' || in[j] == ')'){
				break;
			} else {
				currentTag += in[j];
			}
		}
		return currentTag;
	}
	public static void main(String[] args){
		REPL repl = new REPL();
	}
}
