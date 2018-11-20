/* Copyright (C) 1998-2018 by Northwoods Software Corporation. All Rights Reserved. */

// When adding samples or extensions, modify this file, samples/all.html, and samples/indexList.js
// along with adding a 400x400 screenshot in assets/images/screenshots.

// Load necessary scripts:
if (window.require) {
  // declare required libraries and ensure Bootstrap's dependency on jQuery
  require.config({
    paths: {
      "highlight": "/wp-content/themes/weddingvendor/gojs/assets/js/highlight",
      "jquery": "/wp-content/themes/weddingvendor/gojs/assets/js/jquery.min", // 1.11.3
      "bootstrap": "/wp-content/themes/weddingvendor/gojs/assets/js/bootstrap.min"
    },
    shim: {
      "bootstrap": ["jquery"]
    }
  });
  require(["highlight", "jquery", "bootstrap"], function() {});
} else {
  function goLoadSrc(filenames) {
    var scripts = document.getElementsByTagName("script");
    var script = null;
    for (var i = 0; i < scripts.length; i++) {
      if (scripts[i].src.indexOf("goSamples") > 0) {
        script = scripts[i];
        break;
      }
    }
    for (var i = 0; i < arguments.length; i++) {
      var filename = arguments[i];
      if (!filename) continue;
      var selt = document.createElement("script");
      selt.async = false;
      selt.defer = false;
      selt.src = "/wp-content/themes/weddingvendor/gojs/assets/js/" + filename;
      script.parentNode.insertBefore(selt, script.nextSibling);
      script = selt;
    }
  }
  goLoadSrc("highlight.js", (window.jQuery ? "" : "jquery.min.js"), "bootstrap.min.js");
}

var head = document.getElementsByTagName("head")[0];


link = document.createElement("link");
link.type = "text/css";
link.rel = "stylesheet";
link.href = "/wp-content/themes/weddingvendor/gojs/assets/css/highlight.css";
head.appendChild(link);

function goSamples() {
  // determine if it's an extension
  var isExtension = (location.pathname.split('/').slice(-2)[0].indexOf("extensions") >= 0);
  var isTS = (location.pathname.split('/').slice(-2)[0].indexOf("TS") > 0);

  // save the body for goViewSource() before we modify it
  window.bodyHTML = document.body.innerHTML;
  window.bodyHTML = window.bodyHTML.replace(/</g, "&lt;");
  window.bodyHTML = window.bodyHTML.replace(/>/g, "&gt;");

  // look for links to API documentation and convert them
  _traverseDOM(document);

  

}

// Traverse the whole document and replace <a>TYPENAME</a> with:
//    <a href="/wp-content/themes/weddingvendor/gojs/api/symbols/TYPENAME.html">TYPENAME</a>
// and <a>TYPENAME.MEMBERNAME</a> with:
//    <a href="/wp-content/themes/weddingvendor/gojs/api/symbols/TYPENAME.html#MEMBERNAME">TYPENAME.MEMBERNAME</a>
function _traverseDOM(node) {
  if (node.nodeType === 1 && node.nodeName === "A" && !node.getAttribute("href")) {
    var text = node.innerHTML.split(".");
    if (text.length === 1) {
      node.setAttribute("href", "/wp-content/themes/weddingvendor/gojs/api/symbols/" + text[0] + ".html");
      node.setAttribute("target", "api");
    } else if (text.length === 2) {
      node.setAttribute("href", "/wp-content/themes/weddingvendor/gojs/api/symbols/" + text[0] + ".html" + "#" + text[1]);
      node.setAttribute("target", "api");
    } else {
      alert("Unknown API reference: " + node.innerHTML);
    }
  }
  for (var i = 0; i < node.childNodes.length; i++) {
    _traverseDOM(node.childNodes[i]);
  }
}

function goViewSource() {
  // show the code:
  var script = document.getElementById("code");
  if (!script) {
    var scripts = document.getElementsByTagName("script");
    script = scripts[scripts.length - 1];
  }
  var sp1 = document.createElement("pre");
  sp1.setAttribute("class", "javascript");
  sp1.innerHTML = script.innerHTML;
  var samplediv = document.getElementById("sample") || document.body;
  samplediv.appendChild(sp1);

  // show the body:
  var sp2 = document.createElement("pre");
  sp2.innerHTML = window.bodyHTML;
  samplediv.appendChild(sp2);

  window.hdr.children[0].style.display = "none"; // hide the "View Source" link

  // apply formatting
  hljs.highlightBlock(sp1);
  hljs.highlightBlock(sp2);
  window.scrollBy(0,100);
}
