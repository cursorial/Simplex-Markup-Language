Simplex Markup Language
==========

A Simple-expression to HTML translator.

Example:

Input:

<code>(html(head(title "Title")(style "p{margin-left:20px;}"))(body(script :src='script.js')(p "This is a paragraph")))</code>

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
\<script src='script.js'\>
\</script\><br/>
\<p\><br/>
This is a paragraph<br/>
\</p\><br/>
\</body\><br/>
\</html\><br/>
</code>

Wishlist:

*The ability to add attributes to tags

*Extensions in Chrome and Firefox that have s-exp standalone. No HTML required. I don't even know if that's possible.

*Plugins for popular text editors to allow s-exp to HTML, a la emmet.io
