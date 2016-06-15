(function () {
    var div = document.createElement("div");
    div.classList.add("row", "center", "middle", "full");
    var p = document.createElement("p");

    p.innerText = "This is a subheading created by a dynamically-loaded JS file";
    
    div.appendChild(p);
    document.body.appendChild(div);
}());