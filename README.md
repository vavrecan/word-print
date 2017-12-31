# Word Print

This simple utility for Windows allows you to print documents from webpages with a single click.
It works by creating new protocol handler **word-print**

We recommand that you test installation before using the utility in automated processes since file association check can sometimes hang the loading.

## Usage:
```
word-print:url=[doc/docx url]&preview=[preview before print]&printer=[printer name]
```

On webpage:
```
<a href="word-print:url=http://example.me/file.docx&preview=1">Print</a>
```
Dynamically using javascript:
```
(function(){
    var i = document.createElement('iframe');
    i.style.display = 'none';
    i.onload = function() { i.parentNode.removeChild(i); };
    i.src = 'word-print:url=http://example.me/file.docx&preview=1';
    document.body.appendChild(i);
})();
```

## Author
- [Marek Vavrecan](mailto:vavrecan@gmail.com)
- [Donate by PayPal](https://www.paypal.me/vavrecan)

## License
- [GNU General Public License, version 3](http://www.gnu.org/licenses/gpl-3.0.html)
