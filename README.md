Simplex Markup Language
==========

A Simple-expression to HTML translator.

Example:

Input:

    (html
        (head
            (title "Title")
            (style "p { margin-left:20px; }")
        )
        (body
            (script :src='script.js')
            (p "This is a paragraph")
        )
    )

Output:

    <html>
        <head>
            <title>
                Title
            </title>
            <style> p { margin-left: 20px; } </style>
            <script src='script.js'></script>
        </head>
        <body>
            <p>This is a paragraph</p>
        </body>
    </html>

Wishlist:

*The ability to add attributes to tags

*Extensions in Chrome and Firefox that have s-exp standalone. No HTML required. I don't even know if that's possible.

*Plugins for popular text editors to allow s-exp to HTML, a la emmet.io
