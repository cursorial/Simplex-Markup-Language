chrome.extension.onMessage.addListener(function(request, sender){
	if(request.line == 'content'){
		var stringOpenerCount = 0;
		var attribCount = 0

		var content = document.getElementsByTagName('pre')[0].innerHTML;
		var lines = content.split("\n");
		var output = "";
		
		var fileContents = "";
		for(var i = 0; i < lines.length; i++){
			fileContents += lines[i].trim();
		}
		console.log(fileContents);

		var branchHistory = new Array();

		var currentName = "";
		var currentAttrib = "";
		var currentContent = "";

		function addOpenToken(i){
			currentName = "";
			for(var k = 0; k < branchHistory.length; k++){
				output += "\t";
			}
			for(var j = i + 1; j < fileContents.length; j++){
				if(fileContents[j] == '(' || fileContents[j] == ')'){
					branchHistory.push(currentName);
					output += "<" + currentName + ">\n";
					break;
				} else if (fileContents[j + 1] == ':' || fileContents[j + 1] == '"'){
					branchHistory.push(currentName);
					output += "<" + currentName;
					break;
				} else {
					currentName += fileContents[j];
				}
			}
		}

		function addBranch(i){
			if(attribCount > 0 && stringOpenerCount == 0){
				output += ">\n";
			}
			for(var k = 0; k < branchHistory.length - 3; k++){
				output += "\t";
			}
			output += "</" + branchHistory.pop() + ">\n";
			attribCount = 0;
			stringOpenerCount = 0;
		}

		function addAttribToken(i){
			currentAttrib = "";
			for(var j = i + 1; j < fileContents.length; j++){
				if(fileContents[j] == "("){
					output += " " + currentAttrib + ">";
					break;
				} else if(fileContents[j] == ' ' || fileContents[j] == ')'){
					output += " " + currentAttrib;
					break;
				} else {
					currentAttrib += fileContents[j];
				}
			}
			attribCount++;
		}

		function addContentToken(i){
			currentContent = "";
			for(var j = i + 1; j < fileContents.length; j++){
				if(fileContents[j] == '"'){
					stringOpenerCount++;
					break;
				} else {
					currentContent += fileContents[j];
				}
			}
			output += ">\n";
			for(var k = 0; k < branchHistory.length - 1; k++){
				output += "\t";
			}
			output += currentContent + "\n";
		}

		for(var i = 0; i < fileContents.length; i++){
			if(fileContents[i] == '('){
				addOpenToken(i);
			} else if (fileContents[i] == ')'){
				addBranch(i);
			} else if (fileContents[i] == '"'){
				if(stringOpenerCount == 0){
					addContentToken(i);
				}
			} else if (fileContents[i] == ':'){
				addAttribToken(i);
			}
		}

		console.log(output);
		document.write(output);
	}
});