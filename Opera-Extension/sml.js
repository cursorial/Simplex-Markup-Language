function compile(){
	chrome.tabs.query({currentWindow: true, active: true}, function(tab) {       
		chrome.tabs.sendMessage(tab[0].id, {line: 'content'});
	});
}

chrome.tabs.onUpdated.addListener(compile);