chrome.extension.onMessage.addListener(function(request, sender){
	if(request.line == 'content'){
		var currentTag = "";
		var currentParam = "";
		var tagHistory = new Array();
		var currentParams = new Array();
		var stringOpenerCount = 0;

		var content = document.getElementsByTagName('pre')[0].innerHTML;
		var lines = content.split("\n");
		var sContents = "";
		var output = "";
		console.log(lines);

		function readTag(input, i){
			currentTag = "";
			for(var j = i + 1; j < input.length; j++){
				if(input[j] == '(' || input[j] == ')' || input[j] == ' '){
					break;
				} else {
					currentTag += input[j];
				}
			}
			return currentTag;
		}

		function readStringParameter(input, i){
			currentParam = "";
			for(var j = i + 1; j < input.length; j++){
				if(input[j] == '"'){
					stringOpenerCount++;
					break;
				} else {
					currentParam += input[j];
				}
			}
			return currentParam;
		}

		for(var i = 0; i < lines.length; i++){
			sContents += lines[i].trim();
		}
		console.log(sContents);
		for(var i = 0; i < sContents.length; i++){
			if(sContents[i] == '('){
				tagHistory.push(readTag(sContents, i));
				tagHistory.push(readTag(sContents, i));
				for(var j = 1; j < tagHistory.length; j++){
					output += "\t";
				}
				output += "<" + tagHistory.pop() + ">\n";
			} else if (sContents[i] == ')'){
				stringOpenerCount = 0;
				for(var j = 1; j < tagHistory.length; j++){
					output += "\t";
				}
				output += "</" + tagHistory.pop() + ">\n";
			} else if (sContents[i] == '"'){
				if(stringOpenerCount == 0){
					currentParams.push(readStringParameter(sContents, i));
					for(var j = 1; j < tagHistory.length; j++){
						output += "\t";
					}
					output += currentParams.pop() + "\n";
				}
			} else {
				currentTag += sContents[i];
			}
		}
		console.log(output);
		document.body.innerHTML = output;
	}
});