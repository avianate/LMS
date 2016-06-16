(function (XHR) {
    var container = document.querySelector(".ajax-container");

    var div = document.createElement("div");
    div.classList.add("row", "center", "middle", "full");

    var p = document.createElement("p");
    p.innerText = "This is a subheading created by a dynamically-loaded JS file";
    
    div.appendChild(p);
    container.appendChild(div);

}(XHR));