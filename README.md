S-exp2HTML
==========

A Simple-expression to HTML translator written in Java.

REPL Example:

Input:

<code>(html(head(title))(body(p)))</code>

Output:

<code>
\<html\><br/>
\<head\><br/>
\<title\><br/>
\</title\><br/>
\</head\><br/>
\<body\><br/>
\<p\><br/>
\</p\><br/>
\</body\><br/>
\</html\><br/>
</code>

Still to come:

The ability to insert text and tag properties.

Example:

Input:

<code>(html(head(title "Title")(style "p{margin-left:20px;}"))(body(p id:'paragraph' "This is a paragraph")))</code>

Output:

<code>
\<html\><br/>
\<head\><br/>
\<title\><br/>
Title<br/>
\</title\><br/>
\<style\><br/>
p{margin-left:20px;}<br/>
\</style\><br/>
\</head\><br/>
\<body\><br/>
\<p id='paragraph'\><br/>
This is a paragraph<br/>
\</p\><br/>
\</body\><br/>
\</html\><br/>
</code>
